using MVC_Shop.DMO;
using MVC_Shop.Models.DTO;

namespace MVC_Shop.Repository
{
	public interface IProductRepository
	{

	}
	public class ProductRepository
	{
		private AdventureWorks2019Context _context;
        public ProductRepository(AdventureWorks2019Context context)
        {
            _context= context;
        }
		public List<ProductDTO> GetProductBySubCategoryId(int subCategoryId)
		{

		}

    }
}
