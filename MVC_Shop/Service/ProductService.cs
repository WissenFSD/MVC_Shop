using Microsoft.Identity.Client;
using MVC_Shop.Models.DTO;
using MVC_Shop.Repository;

namespace MVC_Shop.Service
{
	public interface IProductService
	{
		public List<ProductDTO> GetProductBySubCategoryId(int subCategoryId);
	}
	public class ProductService:IProductService
	{
        private ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
			_productRepository= productRepository;

		}
		public List<ProductDTO> GetProductBySubCategoryId(int subCategoryId)
		{

			return _productRepository.GetProductBySubCategoryId(subCategoryId);
		}
    }
}
