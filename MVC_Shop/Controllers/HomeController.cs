using Microsoft.AspNetCore.Mvc;
using MVC_Shop.Models;
using MVC_Shop.Models.ViewModel;
using MVC_Shop.Service;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MVC_Shop.Controllers
{
	public class HomeController : Controller
	{
		private IProductCategoryService _productSubCategoryService;
		private IProductService _productService;
		public HomeController(IProductCategoryService productSubCategoryService, IProductService productService)
		{
			_productSubCategoryService = productSubCategoryService;
			_productService = productService;
		}

		public IActionResult Index()
		{
			PscViewModel model = new PscViewModel();
			model.PSCModel = _productSubCategoryService.GetAll();
			return View(model);
		}
		public IActionResult ProductFilter(int categoryId)
		{

			PscViewModel model = new PscViewModel();
			model.PSCModel = _productSubCategoryService.GetAll();
			model.Products = _productService.GetProductBySubCategoryId(categoryId);


			return View("Index", model);
		}

		public IActionResult Sepet()
		{

			return View();
		}

		[HttpPost]
		public IActionResult AddSepet(int id)
		{
			try
			{
				if (HttpContext.Session!= null)
				{
					// ikinci kez session'i kullan�yorsak
					var sepetList = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("sepet"));
					sepetList.Add(id);

					var jsonSepet = JsonConvert.SerializeObject(sepetList);
					HttpContext.Session.SetString("sepet", jsonSepet);
				}
				else
				{
					// �lk kez kullan�yorsak
					List<int> sepets = new List<int> { id };
					var jsonSepet = JsonConvert.SerializeObject(sepets);
					HttpContext.Session.SetString("sepet", jsonSepet);

				}
				return Json(true);

			}
			catch {

				return Json(false);
			}
		
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
