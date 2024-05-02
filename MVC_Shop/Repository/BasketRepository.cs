using Microsoft.EntityFrameworkCore;
using MVC_Shop.DMO;
using MVC_Shop.Models.DTO;

namespace MVC_Shop.Repository
{
	public interface IBasketRepository
	{

		public List<GroupBasket> GetProductById(List<int> ids);
		public decimal GetTotalPrice(List<int> productIds);
		public bool SetStock(List<int> ids);
	}
	public class BasketRepository : IBasketRepository
	{
		private AdventureWorks2019Context _context;
		public BasketRepository(AdventureWorks2019Context context)
		{
			_context = context;
		}
		public decimal GetTotalPrice(List<int> productIds)
		{
			decimal totalPrice = _context.Products.Where(s => productIds.Contains(s.ProductId)).Select(k => k.ListPrice * productIds.Count).FirstOrDefault();
			return totalPrice;
		}
		public BasketDTO GetProductById(int id)
		{
			var result = _context.Products.Include(m => m.ProductProductPhotos).ThenInclude(k => k.ProductPhoto).Where(s => s.ProductId == id).Select(k => new BasketDTO()
			{
				Id = k.ProductId,
				Name = k.Name,
				Price = k.ListPrice,
				Photo = k.ProductProductPhotos.Where(s => s.ProductId == s.ProductId).Select(k => k.ProductPhoto.LargePhoto).SingleOrDefault()
			}).FirstOrDefault();
			return result;
		}
		public List<GroupBasket> GetProductById(List<int> ids)
		{
			//var result = _context.Products.Include(m => m.ProductProductPhotos).ThenInclude(k => k.ProductPhoto).Where(s => ids.Any(k=>k==s.ProductId)).Select(k => new BasketDTO()
			//{
			//	Id = k.ProductId,
			//	Name = k.Name,
			//	Price = k.ListPrice,
			//	Photo = k.ProductProductPhotos.Where(s => s.ProductId == s.ProductId).Select(k => k.ProductPhoto.LargePhoto).SingleOrDefault()
			//}).ToList();

			//gru
			var groupResult = ids.GroupBy(s => s).Select(y => new GroupBasket
			{

				ProductId = y.Key,
				Quantity = y.Count(),
				Product = GetProductById(y.Key)

			}).ToList();

			return groupResult;

		}

		public bool SetStock(List<int> ids)
		{
			var product = _context.Products.Where(s => ids.Contains(s.ProductId));
			foreach (var item in product)
			{
				item.SafetyStockLevel = (short)(item.SafetyStockLevel - 1);
			}

			// kaydedelimtry{

			try
			{
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
				
			}
			

		}
	}
}
