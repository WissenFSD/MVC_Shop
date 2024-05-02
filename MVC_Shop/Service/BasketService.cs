using MVC_Shop.Models.DTO;
using MVC_Shop.Repository;

namespace MVC_Shop.Service
{

	public interface IBasketService
	{
		public List<GroupBasket> GetProductById(List<int> ids);
		public decimal GetTotalPrice(List<int> productIds);
	}
	public class BasketService : IBasketService
	{
		private IBasketRepository _basketRepository;
        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
		public decimal GetTotalPrice(List<int> productIds)
		{
			var result = _basketRepository.GetTotalPrice(productIds);
			return result;
		}

		public List<GroupBasket> GetProductById(List<int> ids)
		{
			var result = _basketRepository.GetProductById(ids);
			return result;
		}
	}
}
