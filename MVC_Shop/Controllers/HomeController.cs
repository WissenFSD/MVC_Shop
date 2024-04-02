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
		private IBasketService _basketService;
		public HomeController(IProductCategoryService productSubCategoryService,
			IProductService productService,
			IBasketService basketService)
		{
			_productSubCategoryService = productSubCategoryService;
			_productService = productService;
			_basketService = basketService;
		}

		public IActionResult Index()
		{
			PscViewModel model = new PscViewModel();
			model.PSCModel = _productSubCategoryService.GetAll();

			//Redirect olduðunda sepetteki ürünleri görmek için sayfa açýlýrkende session deðerini alalým
			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("sepet")))
			{
				string json = HttpContext.Session.GetString("sepet");
				var sessionObject = JsonConvert.DeserializeObject<List<int>>(json);

				//Session içerisindeki ürün adedini bulup, view modele mapledik. 
				model.SessionCount = sessionObject.Count;
			}


			return View(model);
		}
		public IActionResult ProductFilter(int categoryId)
		{

			PscViewModel model = new PscViewModel();
			model.PSCModel = _productSubCategoryService.GetAll();
			model.Products = _productService.GetProductBySubCategoryId(categoryId);


			//Sayfa refresh olduðunda, sepetteki ürün miktarý sýfýrlanýyor. Düzenleyelim 

			//Sepette ürün varsa ürün sayýsýný bulalým

			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("sepet")))
			{
				string json = HttpContext.Session.GetString("sepet");
				var sessionObject = JsonConvert.DeserializeObject<List<int>>(json);

				//Session içerisindeki ürün adedini bulup, view modele mapledik. 
				model.SessionCount = sessionObject.Count;
			}


			return View("Index", model);
		}

		public IActionResult Sepet()
		{


			// Önce sepette olan ürünlerin id deðerlerini yakalayalým
			if (HttpContext.Session.Keys.Count() > 0)
			{

				var jsonBasket = HttpContext.Session.GetString("sepet");

				List<int> basketIds = JsonConvert.DeserializeObject<List<int>>(jsonBasket);
				var baskets = _basketService.GetProductById(basketIds);
				return View(baskets);
			}
			return RedirectToAction("Index");
			
		}

		[HttpPost]
		public IActionResult AddSepet(int id)
		{
			try
			{
				if (HttpContext.Session != null && HttpContext.Session.Keys.Count() > 0)
				{
					// ikinci kez session'i kullanýyorsak
					var sepetList = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("sepet"));
					sepetList.Add(id);

					var jsonSepet = JsonConvert.SerializeObject(sepetList);
					HttpContext.Session.SetString("sepet", jsonSepet);
				}
				else
				{
					// Ýlk kez kullanýyorsak
					List<int> sepets = new List<int> { id };
					var jsonSepet = JsonConvert.SerializeObject(sepets);
					HttpContext.Session.SetString("sepet", jsonSepet);

				}
				return Json(true);

			}
			catch
			{

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
