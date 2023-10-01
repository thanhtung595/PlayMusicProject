using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayMusicProject.EntityData;
using PlayMusicProject.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PlayMusicProject.Areas.Shopping.Controllers
{
    [Area("Shopping")]
    public class AdminEditProductController : Controller
    {
        private readonly ILogger<AdminEditProductController> _logger;
        private readonly AppDbContext _dbContext;
        public string _UserNameCookis;
        private readonly IWebHostEnvironment _enviroment;
        public AdminEditProductController(ILogger<AdminEditProductController> logger, AppDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _dbContext = dbContext;
            _enviroment = hostEnvironment;
        }

        [Authorize]
        [Area("Shopping")]
        public IActionResult CardAddProduct(int pageSize, int pageNumber, int CategoryId, int idSapXepKiem, string seachName)
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

            var card = from c in _dbContext.AddCartEntity
                       join us in _dbContext.UserEntity
                       on c.IdUser equals us.IdUser
                       select new AddCart()
                       {
                           IdAddCart = c.IdAddCart,
                           IdUser = c.IdUser,
                           NameUser = us.UserName,
                           CountAddCart = c.CountAddCart,
                           IdProductShop = c.IdProductShop,
                           SumPrice = c.SumPrice,
                           ImageProductShop = c.ImageProductShop,
                       };

            List<AddCart> addCart = card.ToList();
            return View(addCart);
        }

        [Authorize]
        [Area("Shopping")]
        public IActionResult PayAdmin()
        {
            int idUserCart = 0;
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
                    idUserCart = us.IdUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }

            var pay = from p in _dbContext.PayEntity
                      select new Pay()
                      {
                          IdPay = p.IdPay,
                          IdUser = p.IdUser,
                          IdProductString = p.IdProductString,
                          CountProductString = p.CountProductString,
                          CountCart = p.CountCart,
                          TotalPay = p.TotalPay,
                          ActionPay = p.ActionPay,
                      };
            var vm = new PlayMusicProjectMode
            {
                Pay = pay.ToList(),
            };
            return View(vm);
        }

        [Authorize]
        [Area("Shopping")]
        public IActionResult CheckBillAdmin(int id, int idPay, int ActionPay)
        {
            int idUserCart = 0;
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
                    idUserCart = us.IdUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }
            var pay = from p in _dbContext.PayEntity
                      where p.IdPay == id
                      select new Pay()
                      {
                          IdPay = p.IdPay,
                          IdUser = p.IdUser,
                          IdProductString = p.IdProductString,
                          CountProductString = p.CountProductString,
                          CountCart = p.CountCart,
                          TotalPay = p.TotalPay,
                          ActionPay = p.ActionPay,
                      };
            var vm = new PlayMusicProjectMode
            {
                Pay = pay.ToList(),
            };

            if(idPay > 0)
            {
                var editPay = _dbContext.PayEntity.Find(idPay);
                editPay.ActionPay = ActionPay;
                _dbContext.PayEntity.Update(editPay);
                _dbContext.SaveChanges();
                return Redirect("/Shopping/AdminEditProduct/PayAdmin");
            }

            return View(vm);
        }

		[Area("Shopping")]
		public IActionResult DeleteBillAdmin(int id)
        {
            var del = _dbContext.PayEntity.Find(id);
            _dbContext.PayEntity.Remove(del);
            _dbContext.SaveChanges();
            return Redirect("/Shopping/AdminEditProduct/PayAdmin");
        }

        [Authorize]
        [Area("Shopping")]
        public IActionResult ProductShop(int pageSize, int pageNumber, int CategoryId, int idSapXepKiem, string seachName) 
        {
            int idUserCart = 0;
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
                    idUserCart = us.IdUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }

            var product = from p in _dbContext.ProductShopEntity
                          select new ProductShop()
                          {
                              IdProductShop = p.IdProductShop,
                              NameProductShop = p.NameProductShop,
                              IdBrandShop = p.IdBrandShop,
                              IdItemCategoryShop = p.IdItemCategoryShop,
                              NewProductShop = p.NewProductShop,
                              PriceProductShop = p.PriceProductShop,
                              ImageProductShop = p.ImageProductShop,
                          };
            if (pageSize == 0)
                pageSize = 5;

            if (pageNumber == 0)
                pageNumber = 1;
            var toatlCount = product.Count();

            var pageCount = (int)Math.Ceiling((double)toatlCount / pageSize);
            var skip = pageNumber * pageSize - pageSize;
            var vm = new PlayMusicProjectMode()
            {
                TotalCount = toatlCount,

                ProductShop = product.OrderBy(x => x.IdProductShop).Skip(skip).Take(pageSize).ToList(),
                PageCount = pageCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(vm); 
        }

    }
}
