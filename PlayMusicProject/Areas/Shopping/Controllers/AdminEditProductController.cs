using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayMusicProject.EntityData;
using PlayMusicProject.Models;

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
    }
}
