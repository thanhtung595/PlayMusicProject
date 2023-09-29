using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using PlayMusicProject.EntityData;
using PlayMusicProject.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;

namespace PlayMusicProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;
        public string _UserNameCookis;
        public string nameMusic;
        private readonly IWebHostEnvironment _enviroment;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, IWebHostEnvironment enviroment)
        {
            _logger = logger;
            _dbContext = dbContext;
            _enviroment = enviroment;
        }

        public IActionResult Index(int id, string nameMusicSearch)
        {
            nameMusic = nameMusicSearch;
            if(nameMusicSearch != null)
            {
                TempData["MusicNameSc"] = nameMusicSearch;
            }
            else
            {
                TempData["MusicNameSc"] = " ";
            }
            if (id == 0)
            {
                id = 0;
            }
            var musicList = from music in _dbContext.MusicEntity
                        join cate in _dbContext.CateGoryEntity
                        on music.IdCategory equals cate.IdCategory
                        join atist in _dbContext.ArtistsEntity
                        on music.IdArtists equals atist.IdArtists
                        join lyric in _dbContext.LyricMusicEntity
                        on music.IdLyricMusic equals lyric.IdLyricMusic
                        where music.IdMusic == id
                        select new Music()
                        {
                            IdMusic = music.IdMusic,
                            NameMusic = music.NameMusic,
                            DescribeMusic = music.DescribeMusic,
                            IdArtists = atist.IdArtists,
                            ImageMusic = music.ImageMusic,
                            AudioMusic = music.AudioMusic,
                            NameArtists = atist.NameArtists,
                            ImageArtists = atist.ImageArtists,
                            IdCategory = cate.IdCategory,
                            IdLyricMusic = music.IdLyricMusic,
                            LyreicText = lyric.LyricMusic,
                            TopMusic = music.TopMusic,
                            DateTimeCreate = music.DateTimeCreate
                        };
            var baner = from b in _dbContext.BannerEntity
                        select new Banner()
                        {
                            idBanner = b.idBanner,
                            imageBanner = b.imageBanner,
                        };
            var vm = new PlayMusicProjectMode
            {
                Music = musicList.ToList(),
                Banner = baner.ToList(),
            };

            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            if(_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                 };
                foreach (var us in AcountUser)
                {
                        TempData["IdUser"] = us.IdUser;
                        TempData["UserName"] = us.UserName;
                        TempData["AccountUser"] = us.AccountUser;
                        TempData["AccountPass"] = us.AccountPass;
                        TempData["UserImage"] = us.UserImage;
                        TempData["CheckAdmin"] = us.IsAdmin;
                }
            }
            return View(vm);
        }

        public IActionResult Index_GetMusic()
        {
            string name = TempData["MusicNameSc"].ToString();
            if (String.IsNullOrEmpty(name))
            {
                var query = from music in _dbContext.MusicEntity
                            join cate in _dbContext.CateGoryEntity
                            on music.IdCategory equals cate.IdCategory
                            join atist in _dbContext.ArtistsEntity
                            on music.IdArtists equals atist.IdArtists
                            where music.IsDelete == false
                            select new Music()
                            {
                                IdMusic = music.IdMusic,
                                NameMusic = music.NameMusic,
                                DescribeMusic = music.DescribeMusic,
                                IdArtists = atist.IdArtists,
                                ImageMusic = music.ImageMusic,
                                NameArtists = atist.NameArtists,
                                IdCategory = cate.IdCategory,
                                IdLyricMusic = music.IdLyricMusic,
                                TopMusic = music.TopMusic,
                                DateTimeCreate = music.DateTimeCreate
                            };
                return Ok(query);
            }
            else
            {
                var query = from music in _dbContext.MusicEntity
                            join cate in _dbContext.CateGoryEntity
                            on music.IdCategory equals cate.IdCategory
                            join atist in _dbContext.ArtistsEntity
                            on music.IdArtists equals atist.IdArtists
                            where music.IsDelete == false && (string.IsNullOrEmpty(name) || music.NameMusic.ToLower().Contains(name.ToLower()))
                            select new Music()
                            {
                                IdMusic = music.IdMusic,
                                NameMusic = music.NameMusic,
                                DescribeMusic = music.DescribeMusic,
                                IdArtists = atist.IdArtists,
                                ImageMusic = music.ImageMusic,
                                NameArtists = atist.NameArtists,
                                IdCategory = cate.IdCategory,
                                IdLyricMusic = music.IdLyricMusic,
                                TopMusic = music.TopMusic,
                                DateTimeCreate = music.DateTimeCreate
                            };
                return Ok(query);
            }
        }
        
        public IActionResult Index_Featured_Artists()
        {
            var query = from ats in _dbContext.ArtistsEntity
                        select new Artists()
                        {
                            IdArtists = ats.IdArtists,
                            NameArtists = ats.NameArtists,
                            ImageArtists = ats.ImageArtists
                        };
            return Ok(query);
        }

        public IActionResult Index_New_Releases()
        {
            var query = from music in _dbContext.MusicEntity
                        join atist in _dbContext.ArtistsEntity
                        on music.IdArtists equals atist.IdArtists
                        where music.IsDelete == false && music.IsNew == true
                        select new Music()
                        {
                            IdMusic = music.IdMusic,
                            NameMusic = music.NameMusic,
                            IdArtists = atist.IdArtists,
                            NameArtists = atist.NameArtists,
                            ImageMusic = music.ImageMusic,
                            IsNew = music.IsNew
                        };
            return Ok(query);
        }

        [Authorize]
        public IActionResult Album()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                 };
                foreach (var us in AcountUser)
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }
            return View();
        }

        [Authorize]
        public IActionResult Artists()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                 };
                foreach (var us in AcountUser)
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }
            return View();
        }

        [Authorize]
        public IActionResult Genres() 
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                 };
                foreach (var us in AcountUser)
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User useInfo)
        {
            
            var query = from us in _dbContext.UserEntity
                        where us.AccountUser == useInfo.AccountUser && us.AccountPass == useInfo.AccountPass
                        select new ListUser()
                        {
                            UserName = us.UserName,
                            IdUser = us.IdUser,
                            AccountUser = us.AccountUser,
                            AccountPass = us.AccountPass,
                            IsAdmin = us.IsAdmin,
                            IsBan = us.IsBan
                        };

            var user = query.FirstOrDefault(x => (x.AccountUser == useInfo.AccountUser)
            && x.AccountPass == useInfo.AccountPass);
            if (user == null)
            {
                string message = "Tài khoản hoặc mật khẩu không chính xác";
                TempData["erorMessage"] = message;
                return Redirect("/Home/Login");
            }
            foreach (var item in query)
            {
                if (item.IsBan == true)
                {
                    string message = "Tài khoản đã bị khóa do hành vi xấu";
                    TempData["erorMessage"] = message;
                    return Redirect("/Home/Login");
                }
            }
            var clanims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.AccountUser),

                new Claim(ClaimTypes.NameIdentifier, user.AccountUser),
            };

            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                { 
                    string s = cclams.Value;
                } 
            }
            var identy = new ClaimsIdentity(clanims, CookieAuthenticationDefaults.AuthenticationScheme);

            var princical = new ClaimsPrincipal(identy);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princical);

            foreach (var item in query)
            {
                if(item.IsAdmin == true)
                {
                    return Redirect("/Admin/Home/Index");
                }
            }
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }
            return Redirect("/Home/Index");
        }

        public IActionResult Register_Here()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index_Register_Here(User user, string AccountPassConfirm)
        {
            var checkAcount = from us in _dbContext.UserEntity
                              select new User()
                              {
                                  AccountUser = us.AccountUser
                              };
            foreach (var us in checkAcount)
            {
                if (user.AccountUser.ToLower() == us.AccountUser.ToLower())
                {
                    string eresAcont = "Tài khoản đã được đăng kí";
                    TempData["eresAcont"] = eresAcont;
                    return Redirect("/Home/Register_Here");
                }
            }
            if (user.AccountPass != AccountPassConfirm)
            {
                string eresAcont = "Mật khẩu không giống nhau";
                TempData["eresAcont"] = eresAcont;
                return Redirect("/Home/Register_Here");
            }
            var newUser = new pmoUserEntity()
            {
                UserName = user.UserName,
                AccountUser = user.AccountUser,
                AccountPass = user.AccountPass,
                IsAdmin = false,
                IsBan = false,
                SDTUser = user.SDTUser,
                UserImage = "imageUserdefaul.png",
                TimeCreate = DateTime.Now
            };
            _dbContext.UserEntity.Add(newUser);
            _dbContext.SaveChanges();
            string message = "Đăng ký thành công đăng nhập tại đây";
            TempData["RegisterSuccess"] = message;
            return Redirect("/Home/Login");
        }

        public IActionResult ProFile()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                     SDTUser = us.SDTUser,
                                 };
                foreach (var us in AcountUser)
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                    TempData["PhoneNumber"] = us.SDTUser;
                }
            }

            var user = from us in _dbContext.UserEntity
                       where us.AccountUser == _UserNameCookis
                       select new User()
                       {
                           IdUser = us.IdUser,
                           UserName = us.UserName,
                           AccountUser = us.AccountUser,
                           AccountPass = us.AccountPass,
                           UserImage = us.UserImage,
                           IsAdmin = us.IsAdmin,
                           IsBan = us.IsBan,
                           SDTUser = us.SDTUser
                       };

            List<User> users = user.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult EditUserItem([FromBody] User user)
        {
            var entityUser = _dbContext.UserEntity.Find(user.IdUser);
            entityUser.UserName = user.UserName;
            entityUser.AccountPass = user.AccountPass;
            entityUser.AccountPass = user.AccountPass;
            if (user.IsAdminString.ToLower() == "true".ToLower() || user.IsAdminString == "1")
            {
                entityUser.IsAdmin = true;
            }
            else
            {
                entityUser.IsAdmin = false;
            }

            if (user.IsBanString.ToLower() == "true".ToLower() || user.IsBanString == "1")
            {
                entityUser.IsBan = true;
            }
            else
            {
                entityUser.IsBan = false;
            }
            entityUser.TimeCreate = DateTime.Now;
            entityUser.UserImage = user.UserImage;
            _dbContext.UserEntity.Update(entityUser);
            var status = _dbContext.SaveChanges();
            return Ok(status);
        }


        [HttpPost]
        public IActionResult EditImageProFile(IFormFile imageUpload)
        {
            if (imageUpload == null)
                return Json(new FileUpload()
                {
                    Status = "error",
                    Message = "File không tồn tại"
                });
            var fullPath = Path.Combine(_enviroment.WebRootPath, "images/imageUser", imageUpload.FileName); // upload là foder
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                imageUpload.CopyTo(fileStream);
            }
            return Json(new FileUpload()
            {
                FileName = imageUpload.FileName.ToString(),
                FilePath = Path.Combine("/images/imageUser", imageUpload.FileName),
                Status = "success",
                Message = "Upload file thành công!"
            });
        }

        public class FileUpload
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}