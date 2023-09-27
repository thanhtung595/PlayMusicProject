using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayMusicProject.EntityData;
using PlayMusicProject.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PlayMusicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;
        public string _UserNameCookis;
        private readonly IWebHostEnvironment _enviroment;
        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _dbContext = dbContext;
            _enviroment = hostEnvironment;
        }

        [Authorize]
        [Area("Admin")]
        public IActionResult Index(int pageSize, int pageNumber, int CategoryId, int idSapXepKiem, string seachName)
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            var AcountUser = from user in _dbContext.UserEntity
                             where user.AccountUser == _UserNameCookis
                             select new User()
                             {
                                 IdUser = user.IdUser,
                                 UserName = user.UserName,
                                 AccountUser = user.AccountUser,
                                 AccountPass = user.AccountPass,
                                 UserImage = user.UserImage,
                                 IsAdmin = user.IsAdmin,
                             };

            foreach (var us in AcountUser)
            {
                if (us.IsAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                }
            }

            var query = from user in _dbContext.UserEntity
                        where (string.IsNullOrEmpty(seachName) || user.UserName.ToLower().Contains(seachName.ToLower()))

                        select new User()
                        {
                            IdUser = user.IdUser,
                            UserName = user.UserName,
                            AccountUser = user.AccountUser,
                            AccountPass = user.AccountPass,
                            UserImage = user.UserImage,
                            IsAdmin = user.IsAdmin,
                            IsBan = user.IsBan,
                            TimeCreate = user.TimeCreate
                        };

            if (pageSize == 0)
                pageSize = 5;

            if (pageNumber == 0)
                pageNumber = 1;

            var toatlCount = query.Count();

            var pageCount = (int)Math.Ceiling((double)toatlCount / pageSize);
            var skip = pageNumber * pageSize - pageSize;
            //var take = query.Count();
            if (idSapXepKiem == 0 || idSapXepKiem == 1)
            {
                var vm = new PlayMusicProjectMode()
                {
                    TotalCount = toatlCount,

                    User = query.OrderBy(x => x.IdUser).Skip(skip).Take(pageSize).ToList(),
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                };

                return View(vm);
            }
            else if (idSapXepKiem == 2)
            {
                var vm = new PlayMusicProjectMode()
                {
                    TotalCount = toatlCount,

                    User = query.OrderBy(x => x.AccountUser).Skip(skip).Take(pageSize).ToList(),
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                };

                return View(vm);

            }
            else if (idSapXepKiem == 3)
            {
                var vm = new PlayMusicProjectMode()
                {
                    TotalCount = toatlCount,
                    User = query.OrderBy(x => x.UserName).Skip(skip).Take(pageSize).ToList(),
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                };

                return View(vm);

            }
            else if (idSapXepKiem == 4)
            {
                var vm = new PlayMusicProjectMode()
                {
                    TotalCount = toatlCount,

                    User = query.OrderBy(x => x.IsAdmin).Skip(skip).Take(pageSize).ToList(),
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                };

                return View(vm);

            }
            else if (idSapXepKiem == 5)
            {
                var vm = new PlayMusicProjectMode()
                {
                    TotalCount = toatlCount,

                    User = query.OrderBy(x => x.IsBan).Skip(skip).Take(pageSize).ToList(),
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                };

                return View(vm);

            }
            return View();
        }
        [Authorize]
        [Area("Admin")]
        public IActionResult NewUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            var AcountUser = from user in _dbContext.UserEntity
                             where user.AccountUser == _UserNameCookis
                             select new User()
                             {
                                 IdUser = user.IdUser,
                                 UserName = user.UserName,
                                 AccountUser = user.AccountUser,
                                 AccountPass = user.AccountPass,
                                 UserImage = user.UserImage,
                                 IsAdmin = user.IsAdmin,
                             };

            foreach (var us in AcountUser)
            {
                if (us.IsAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                }
            }
            return View();
        }

        [Authorize]
        [Area("Admin")]
        public IActionResult EditUser(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            var AcountUser = from user in _dbContext.UserEntity
                             where user.AccountUser == _UserNameCookis
                             select new User()
                             {
                                 IdUser = user.IdUser,
                                 UserName = user.UserName,
                                 AccountUser = user.AccountUser,
                                 AccountPass = user.AccountPass,
                                 UserImage = user.UserImage,
                                 IsAdmin = user.IsAdmin,
                             };

            foreach (var us in AcountUser)
            {
                if (us.IsAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                }
            }

            var query = from us in _dbContext.UserEntity
                        where us.IdUser == id
                        select new User()
                        {
                            IdUser = us.IdUser,
                            UserName = us.UserName,
                            AccountUser = us.AccountUser,
                            AccountPass = us.AccountPass,
                            IsAdmin = us.IsAdmin,
                            IsBan = us.IsBan,
                            TimeCreate = us.TimeCreate,
                            UserImage = us.UserImage
                        };
            List<User> users = query.ToList();
            return View(users);
        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult EditUserItem([FromBody] User user)
        {
            var entityUser = _dbContext.UserEntity.Find(user.IdUser);
            entityUser.UserName = user.UserName;
            entityUser.AccountPass = user.AccountPass;
            entityUser.AccountPass = user.AccountPass;
            if(user.IsAdminString.ToLower() == "true".ToLower() || user.IsAdminString == "1")
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

        [Authorize]
        [Area("Admin")]
        public IActionResult DeleteUser(int id)
        {
            var entity = _dbContext.UserEntity.Find(id);
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return Redirect("/Admin/Home/");
        }

        [Authorize]
        [Area("Admin")]
        public IActionResult Music(int pageSize, int pageNumber, int CategoryId, int idSapXepKiem, string seachName)
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            var AcountUser = from user in _dbContext.UserEntity
                             where user.AccountUser == _UserNameCookis
                             select new User()
                             {
                                 IdUser = user.IdUser,
                                 UserName = user.UserName,
                                 AccountUser = user.AccountUser,
                                 AccountPass = user.AccountPass,
                                 UserImage = user.UserImage,
                                 IsAdmin = user.IsAdmin,
                             };

            foreach (var us in AcountUser)
            {
                if (us.IsAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                }
            }

            var query = from music in _dbContext.MusicEntity
                        join cate in _dbContext.CateGoryEntity
                        on music.IdCategory equals cate.IdCategory
                        join ats in _dbContext.ArtistsEntity
                        on music.IdArtists equals ats.IdArtists
                        join lyric in _dbContext.LyricMusicEntity
                        on music.IdLyricMusic equals lyric.IdLyricMusic
                        where (string.IsNullOrEmpty(seachName) || music.NameMusic.ToLower().Contains(seachName.ToLower()))
                        select new Music()
                        {
                            IdMusic = music.IdMusic,
                            NameMusic = music.NameMusic,
                            IdLyricMusic = lyric.IdLyricMusic,
                            IdCategory = cate.IdCategory,
                            NameCategory = cate.NameCategory,
                            NameArtists = ats.NameArtists,
                            TopMusic = music.TopMusic,
                            IsNew = music.IsNew,
                            DateTimeCreate = music.DateTimeCreate
                        };

            if (pageSize == 0)
                pageSize = 5;

            if (pageNumber == 0)
                pageNumber = 1;

            var toatlCount = query.Count();

            var pageCount = (int)Math.Ceiling((double)toatlCount / pageSize);
            var skip = pageNumber * pageSize - pageSize;
            //var take = query.Count();
            //if (idSapXepKiem == 0 || idSapXepKiem == 1)
            //{
                var vm = new PlayMusicProjectMode()
                {
                    TotalCount = toatlCount,
                    Music = query.OrderBy(x => x.IdMusic).Skip(skip).Take(pageSize).ToList(),
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                };

            //    return View(vm);
            //}
            return View(vm);
        }

        [Authorize]
        [Area("Admin")]
        [HttpPost]
        public IActionResult NewMusicItem([FromBody] Music music)
        {
            var checkMusic = from m in _dbContext.MusicEntity
                             select new Music()
                             {
                                 NameMusic = m.NameMusic,
                                 AudioMusic = m.AudioMusic
                             };

            foreach (var item in checkMusic)
            {
                if(item.NameMusic.ToLower() == music.NameMusic.ToLower())
                {
                    return Ok(2);
                }
            }

            foreach (var item in checkMusic)
            {
                if (item.AudioMusic.ToLower() == music.AudioMusic.ToLower())
                {
                    return Ok(3);
                }
            }

            var ms = new pmoMusicEntity{
                NameMusic = music.NameMusic,
                DescribeMusic = music.DescribeMusic,
                IdLyricMusic = music.IdLyricMusic,
                ImageMusic = music.ImageMusic,
                AudioMusic = music.AudioMusic,
                IdArtists = music.IdArtists,
                IdCategory = music.IdCategory,
                TopMusic = 1,
                DateTimeCreate = DateTime.Now,
                IsDelete = false,
                IsNew = false
            };
            _dbContext.MusicEntity.Add(ms);
            var status = _dbContext.SaveChanges();
            return Ok(status);
        }

        [Authorize]
        [Area("Admin")]
        public IActionResult NewMusic()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            var AcountUser = from user in _dbContext.UserEntity
                             where user.AccountUser == _UserNameCookis
                             select new User()
                             {
                                 IdUser = user.IdUser,
                                 UserName = user.UserName,
                                 AccountUser = user.AccountUser,
                                 AccountPass = user.AccountPass,
                                 UserImage = user.UserImage,
                                 IsAdmin = user.IsAdmin,
                             };

            foreach (var us in AcountUser)
            {
                if (us.IsAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                }
            }
            var category = from cate in _dbContext.CateGoryEntity
                           select new CateGory()
                           {
                               IdCategory = cate.IdCategory,
                               NameCategory = cate.NameCategory
                           };
            var artits = from ats in _dbContext.ArtistsEntity
                         select new Artists()
                         {
                             IdArtists = ats.IdArtists,
                             NameArtists = ats.NameArtists
                         };
            var lyric = from ls in _dbContext.LyricMusicEntity
                        select new LyricMusic()
                        {
                            IdLyricMusic = ls.IdLyricMusic,
                            LyricMusics = ls.LyricMusic
                        };
            var vm = new PlayMusicProjectMode()
            {
                CateGory = category.ToList(),
                Artists = artits.ToList(),
                LyricMusic = lyric.ToList()
            };
            return View(vm);
        }
        [Authorize]
        [Area("Admin")]

        public IActionResult EditMusic(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            var AcountUser = from user in _dbContext.UserEntity
                             where user.AccountUser == _UserNameCookis
                             select new User()
                             {
                                 IdUser = user.IdUser,
                                 UserName = user.UserName,
                                 AccountUser = user.AccountUser,
                                 AccountPass = user.AccountPass,
                                 UserImage = user.UserImage,
                                 IsAdmin = user.IsAdmin,
                             };

            foreach (var us in AcountUser)
            {
                if (us.IsAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                }
            }

            var query = from m in _dbContext.MusicEntity
                        where m.IdMusic == id
                        select new Music()
                        {
                            IdMusic = m.IdMusic,
                            NameMusic = m.NameMusic,
                            DescribeMusic = m.DescribeMusic,
                            IdLyricMusic = m.IdLyricMusic,
                            ImageMusic = m.ImageMusic,
                            AudioMusic = m.AudioMusic,
                            IdCategory = m.IdCategory,
                            IdArtists = m.IdArtists,
                            TopMusic = m.TopMusic,
                            IsDelete = m.IsDelete,
                            IsNew = m.IsNew
                        };

            var vm = new PlayMusicProjectMode
            {
                Music = query.ToList(),
            };
            return View(vm);
        }

        [Authorize]
        [Area("Admin")]
        [HttpPost]
        public IActionResult EditMusicItem([FromBody] Music music)
        {
            var ms = _dbContext.MusicEntity.Find(music.IdMusic);
            ms.NameMusic = music.NameMusic;
            _dbContext.MusicEntity.Update(ms);
            var status = _dbContext.SaveChanges();
            return Ok(status);
        }

        [Authorize]
        [Area("Admin")]
        public IActionResult DeleteMusic(int id)
        {
            var entity = _dbContext.MusicEntity.Find(id);
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return Redirect("/Admin/Home/Music");
        }


        [Authorize]
        [Area("Admin")]
        public IActionResult Artists(int pageSize, int pageNumber, int CategoryId, int idSapXepKiem, string seachName)
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            var AcountUser = from user in _dbContext.UserEntity
                             where user.AccountUser == _UserNameCookis
                             select new User()
                             {
                                 IdUser = user.IdUser,
                                 UserName = user.UserName,
                                 AccountUser = user.AccountUser,
                                 AccountPass = user.AccountPass,
                                 UserImage = user.UserImage,
                                 IsAdmin = user.IsAdmin,
                             };

            foreach (var us in AcountUser)
            {
                if (us.IsAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                }
            }

            var query = from str in _dbContext.ArtistsEntity
                        where (string.IsNullOrEmpty(seachName) || str.NameArtists.ToLower().Contains(seachName.ToLower()))
                        select new Artists()
                        {
                            IdArtists = str.IdArtists,
                            NameArtists = str.NameArtists,
                            ImageArtists = str.ImageArtists,
                        };

            if (pageSize == 0)
                pageSize = 5;

            if (pageNumber == 0)
                pageNumber = 1;

            var toatlCount = query.Count();

            var pageCount = (int)Math.Ceiling((double)toatlCount / pageSize);
            var skip = pageNumber * pageSize - pageSize;

            var vm = new PlayMusicProjectMode()
            {
                TotalCount = toatlCount,
                Artists = query.OrderBy(x => x.IdArtists).Skip(skip).Take(pageSize).ToList(),
                PageCount = pageCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(vm);
        }

        [Authorize]
        [Area("Admin")]
        public IActionResult EditBaner()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            var AcountUser = from user in _dbContext.UserEntity
                             where user.AccountUser == _UserNameCookis
                             select new User()
                             {
                                 IdUser = user.IdUser,
                                 UserName = user.UserName,
                                 AccountUser = user.AccountUser,
                                 AccountPass = user.AccountPass,
                                 UserImage = user.UserImage,
                                 IsAdmin = user.IsAdmin,
                             };

            foreach (var us in AcountUser)
            {
                if (us.IsAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                }
            }

            var baner = from b in _dbContext.BannerEntity
                        select new Banner()
                        {
                            idBanner = b.idBanner,
                            imageBanner = b.imageBanner
                        };
            List<Banner> listBaner = baner.ToList();
            return View(listBaner);
        }

        [Authorize]
        [Area("Admin")]
        [HttpPost]
        public IActionResult EditBanerItem([FromBody]Banner banner)
        {
            var edit = _dbContext.BannerEntity.Find(banner.idBanner);
            edit.imageBanner = banner.imageBanner;
            _dbContext.BannerEntity.Update(edit);
            var status =  _dbContext.SaveChanges();
            return Ok(status);
        }

        [HttpPost]
        public IActionResult UploadImageBaner(IFormFile imageUpload)
        {
            if (imageUpload == null)
                return Json(new FileUpload()
                {
                    Status = "error",
                    Message = "File không tồn tại"
                });
            var fullPath = Path.Combine(_enviroment.WebRootPath, "images/banner", imageUpload.FileName); // upload là foder
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                imageUpload.CopyTo(fileStream);
            }
            return Json(new FileUpload()
            {
                FileName = imageUpload.FileName.ToString(),
                FilePath = Path.Combine("/images/banner", imageUpload.FileName),
                Status = "success",
                Message = "Upload file thành công!"
            });
        }

        [HttpPost]
        public IActionResult UploadImageUser(IFormFile imageUpload)
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

        [HttpPost]
        public IActionResult UploadImageMusic(IFormFile imageUpload)
        {
            if (imageUpload == null)
                return Json(new FileUpload()
                {
                    Status = "error",
                    Message = "File không tồn tại"
                });
            var fullPath = Path.Combine(_enviroment.WebRootPath, "images/ImageMusic", imageUpload.FileName); // upload là foder
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                imageUpload.CopyTo(fileStream);
            }
            return Json(new FileUpload()
            {
                FileName = imageUpload.FileName.ToString(),
                FilePath = Path.Combine("/images/ImageMusic", imageUpload.FileName),
                Status = "success",
                Message = "Upload file thành công!"
            });
        }

        [HttpPost]
        public IActionResult UploadImageAudio(IFormFile audioNewUpload)
        {
            if (audioNewUpload == null)
                return Json(new FileUpload()
                {
                    Status = "error",
                    Message = "File không tồn tại"
                });
            var fullPath = Path.Combine(_enviroment.WebRootPath, "audio", audioNewUpload.FileName); // upload là foder
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                audioNewUpload.CopyTo(fileStream);
            }
            return Json(new FileUpload()
            {
                FileName = audioNewUpload.FileName.ToString(),
                FilePath = Path.Combine("audio", audioNewUpload.FileName),
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
    }
}
