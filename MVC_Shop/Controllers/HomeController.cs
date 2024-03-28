using Microsoft.AspNetCore.Mvc;
using MVC_Shop.Models;
using MVC_Shop.Models.ViewModel;
using MVC_Shop.Service;
using System.Diagnostics;

namespace MVC_Shop.Controllers
{
	public class HomeController : Controller
	{
		private IProductCategoryService _productSubCategoryService;
		public HomeController(IProductCategoryService  productSubCategoryService)
		{
			_productSubCategoryService = productSubCategoryService;
		}

		public IActionResult Index()
		{
			PscViewModel model = new PscViewModel();
			model.PSCModel = _productSubCategoryService.GetAll();
			return View(model);
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
