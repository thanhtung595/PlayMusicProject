using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayMusicProject.EntityData;
using PlayMusicProject.Models;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PlayMusicProject.Areas.Shopping.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ILogger<ShoppingController> _logger;
        private readonly AppDbContext _dbContext;
        public string _UserNameCookis;
        public string nameMusic;
        private readonly IWebHostEnvironment _enviroment;
        public ShoppingController(ILogger<ShoppingController> logger, AppDbContext dbContext, IWebHostEnvironment enviroment)
        {
            _logger = logger;
            _dbContext = dbContext;
            _enviroment = enviroment;
        }


        [Area("Shopping")]
        public IActionResult ShoppingHome()
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
		[Area("Shopping")]
        public IActionResult ProductShop(int pageSize, int pageNumber, int categoryIdItem, int idBrands, string seachName, string quantity)
        {
            if(quantity != "")
            {
				TempData["Quantity"] = quantity;
			}

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

            var brands = from b in _dbContext.BrandShopEntity
                         select new BrandShop()
                         {
                             IdBrandShop = b.IdBrandShop,
                             NameBrandShop = b.NameBrandShop,
                         };

            var cateShop = from cate in _dbContext.CategoryShopEntity
                           select new CategoryShop()
                           {
                               IdCategoryShop = cate.IdCategoryShop,
                               NameCategoryShop = cate.NameCategoryShop,
                           };

            var cateItemShop = from cateItem in _dbContext.ItemCategoryShopEntity
                               select new ItemCategoryShop()
                               {
                                   IdItemCategoryShop = cateItem.IdItemCategoryShop,
                                   NameItemCategoryShop = cateItem.NameItemCategoryShop,
                                   IdCategoryShop = cateItem.IdCategoryShop,
                               };

            if(seachName != null)
            {
			    ViewBag.ValueSeachName = seachName.ToString();
            }
			var proShop = from pr in _dbContext.ProductShopEntity
                          where(String.IsNullOrEmpty(seachName) || pr.NameProductShop.ToLower().Contains(seachName.ToLower()))
						  select new ProductShop()
						  {
							  IdProductShop = pr.IdProductShop,
							  NameProductShop = pr.NameProductShop,
							  IdBrandShop = pr.IdBrandShop,
							  IdItemCategoryShop = pr.IdItemCategoryShop,
							  NewProductShop = pr.NewProductShop,
							  PriceProductShop = pr.PriceProductShop,
							  ImageProductShop = pr.ImageProductShop
						  };

			if (categoryIdItem != 0)
            {
				proShop = from pr in _dbContext.ProductShopEntity
							  where pr.IdItemCategoryShop == categoryIdItem
							  select new ProductShop()
							  {
								  IdProductShop = pr.IdProductShop,
								  NameProductShop = pr.NameProductShop,
								  IdBrandShop = pr.IdBrandShop,
								  IdItemCategoryShop = pr.IdItemCategoryShop,
								  NewProductShop = pr.NewProductShop,
								  PriceProductShop = pr.PriceProductShop,
								  ImageProductShop = pr.ImageProductShop
							  };
			}
			if (idBrands != 0)
			{
				proShop = from pr in _dbContext.ProductShopEntity
						  where pr.IdBrandShop == idBrands
						  select new ProductShop()
						  {
							  IdProductShop = pr.IdProductShop,
							  NameProductShop = pr.NameProductShop,
							  IdBrandShop = pr.IdBrandShop,
							  IdItemCategoryShop = pr.IdItemCategoryShop,
							  NewProductShop = pr.NewProductShop,
							  PriceProductShop = pr.PriceProductShop,
							  ImageProductShop = pr.ImageProductShop
						  };
			}

			if (pageSize == 0)
				pageSize = 6;

			if (pageNumber == 0)
				pageNumber = 1;

			var toatlCount = proShop.Count();

            if(toatlCount == 0)
            {
                ViewBag.MesseNullCount = "Mặt hàng này đã hết....";
            }

			var pageCount = (int)Math.Ceiling((double)toatlCount / pageSize);
			var skip = pageNumber * pageSize - pageSize;
			
				var vm = new PlayMusicProjectMode()
				{
					TotalCount = toatlCount,
					BrandShop = brands.ToList(),
					CategoryShop = cateShop.ToList(),
					ItemCategoryShop = cateItemShop.ToList(),
					ProductShop = proShop.OrderBy(x => x.IdProductShop).Skip(skip).Take(pageSize).ToList(),
					PageCount = pageCount,
					PageNumber = pageNumber,
					PageSize = pageSize,
				};

				return View(vm);
		}

		[Authorize]
		[Area("Shopping")]
		public IActionResult Cart(int id)
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

			if (id != 0)
            {
				var checkNullCart = from c in _dbContext.AddCartEntity
									where c.IdUser == idUserCart
									select new AddCart()
									{
										IdAddCart = c.IdAddCart,
										IdProductShop = c.IdProductShop,
										IdUser = c.IdUser,
										CountAddCart = c.CountAddCart,
										SumPrice = c.SumPrice,
										NameProductShop = c.NameProductShop,
										ImageProductShop = c.ImageProductShop,
										PriceProductShop = c.PriceProduct,
									};
				List<AddCart> listNullCart = checkNullCart.ToList();

				foreach (var item in listNullCart)
				{
					if (item.IdProductShop.ToString() == id.ToString())
					{
						return Redirect("/Shopping/Shopping/Cart");
					}
				}
					var checkProduct = from checkPr in _dbContext.ProductShopEntity
									   where checkPr.IdProductShop == id
									   select new ProductShop()
									   {
										   IdProductShop = checkPr.IdProductShop,
										   NameProductShop = checkPr.NameProductShop,
										   IdBrandShop = checkPr.IdBrandShop,
										   IdItemCategoryShop = checkPr.IdItemCategoryShop,
										   NewProductShop = checkPr.NewProductShop,
										   PriceProductShop = checkPr.PriceProductShop,
										   ImageProductShop = checkPr.ImageProductShop
									   };

					List<ProductShop> productShops = checkProduct.ToList();
					
					var addCart = new pmoAddCartEntity
					{
						IdUser = idUserCart,
						CountAddCart = 1,
						IdProductShop = productShops[0].IdProductShop,
						SumPrice = productShops[0].PriceProductShop,
						PriceProduct = productShops[0].PriceProductShop,
						NameProductShop = productShops[0].NameProductShop,
						ImageProductShop = productShops[0].ImageProductShop,
					};
					_dbContext.AddCartEntity.Add(addCart);
					_dbContext.SaveChanges();
					return Redirect("/Shopping/Shopping/Cart");
            }
			var cart = from c in _dbContext.AddCartEntity
					   where c.IdUser == idUserCart
					   select new AddCart()
					   {
						   IdAddCart = c.IdAddCart,
						   IdUser = c.IdUser,
						   CountAddCart = c.CountAddCart,
						   SumPrice = c.SumPrice,
						   NameProductShop = c.NameProductShop,
						   ImageProductShop = c.ImageProductShop,
						   PriceProductShop = c.PriceProduct,
					   };
			decimal sumtotal = 0;
			foreach (var item in cart.ToList())
			{
				sumtotal += item.SumPrice;
			}
			ViewBag.TongSlProduct = cart.Count();
			ViewBag.TongTotal = sumtotal;
			var vm = new PlayMusicProjectMode
            {
                AddCart = cart.ToList(),
            };
			return View(vm);
        }

		[Authorize]
		[Area("Shopping")]
		[HttpPost]
        public IActionResult EditCartAdd([FromBody] AddCart addCart)
        {
            var cartItem = from c in _dbContext.AddCartEntity
                           where c.IdAddCart == addCart.IdAddCart
                           select new AddCart()
                           {
                               PriceProductShop = c.PriceProduct,
                           };
            List<AddCart> addCarts = cartItem.ToList();
            var entity = _dbContext.AddCartEntity.Find(addCart.IdAddCart);
            entity.CountAddCart = addCart.CountAddCart;
            entity.SumPrice = addCarts[0].PriceProductShop * addCart.CountAddCart;
            _dbContext.AddCartEntity.Update(entity);
			var status = _dbContext.SaveChanges();
			return Ok(status);
		}

		[Authorize]
		[Area("Shopping")]
		public IActionResult DeleteCardUser(int id)
		{
			var entity = _dbContext.AddCartEntity.Find(id);
			_dbContext.Remove(entity);
			_dbContext.SaveChanges();
			return Redirect("/Shopping/Shopping/Cart");
		}

        [Authorize]
        [Area("Shopping")]
        public IActionResult DeleteCardAdminUser(int id)
        {
            var entity = _dbContext.AddCartEntity.Find(id);
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return Redirect("/Shopping/AdminEditProduct/CardAddProduct");
        }

        [Authorize]
        [Area("Shopping")]
        public IActionResult Pay()
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
					  where p.IdUser == idUserCart
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
		public IActionResult AddPay()
		{
			int idUserCart = 0;
			int countProduct = 0;
			decimal sumPriceProduct = 0;
			string IdProductString = "";
			string CountProductString = "";
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

			var cardUser = from c in _dbContext.AddCartEntity
						   where c.IdUser == idUserCart
						   select new AddCart()
						   {
							   IdAddCart = c.IdUser,
							   IdUser = idUserCart,
							   IdProductShop = c.IdProductShop,
							   CountAddCart = c.CountAddCart,
							   NameProductShop = c.NameProductShop,
							   SumPrice = c.SumPrice,
							   ImageProductShop = c.ImageProductShop,
						   };
			foreach (var item in cardUser.ToList())
			{
				countProduct += item.CountAddCart;
				IdProductString = IdProductString+" "+ item.IdProductShop.ToString();
				CountProductString = CountProductString + " "+ item.CountAddCart.ToString();
				sumPriceProduct += item.SumPrice;
			}

			var newPay = new pmoPayEntity
			{
				IdUser = idUserCart,
				CountCart = countProduct,
				TotalPay = sumPriceProduct,
				IdProductString = IdProductString,
				CountProductString = CountProductString,
				ActionPay = 0,
			};

			_dbContext.PayEntity.Add(newPay);
			_dbContext.SaveChanges();

			return Redirect("/Shopping/Shopping/Pay");
		}

		[Area("Shopping")]
		public IActionResult DeleteBillUser(int id)
		{
			var del = _dbContext.PayEntity.Find(id);
			_dbContext.PayEntity.Remove(del);
			_dbContext.SaveChanges();
			return Redirect("/Shopping/Shopping/Pay");
		}

	}
}
