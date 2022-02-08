using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BulkyBook.Utility;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");

            return View(productList);
        }
        public IActionResult Details(int ProductId)
        {
            ShoppingCart cartobj = new()
            {
                Count = 1,
                ProductId = ProductId,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == ProductId, includeProperties: "Category,CoverType")
            };
            
            return View(cartobj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]// only authorized user can access the post action method
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            //retrieving current user info
            var clasimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = clasimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCart.ApplicationUserId = claim.Value;
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);
            if(cartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u =>u.ApplicationUserId == claim.Value).ToList().Count);

            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
                _unitOfWork.Save();
            }
 
            

            return RedirectToAction(nameof(Index));
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}