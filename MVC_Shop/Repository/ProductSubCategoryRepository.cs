using MVC_Shop.DMO;

namespace MVC_Shop.Repository
{

	public interface IProductSubCategoryRepository
	{

	}
	public class ProductSubCategoryRepository:IProductSubCategoryRepository
	{
		private AdventureWorks2019Context _context;
        public ProductSubCategoryRepository(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // tüm alt kategorileri dönen bir metot yapalım
        public List<ProductSubcategory> GetAll()
		{
			_context.ProductSubcategories
		}

	}
}
