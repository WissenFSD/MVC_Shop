using MVC_Shop.DMO;

namespace MVC_Shop.Repository
{
	public interface IProductRepository
	{

	}
	public class ProductRepository:IProductRepository
	{
		private AdventureWorks2019Context _context;
        public ProductRepository(AdventureWorks2019Context context)
        {
            _context = context;
        }
    }
}
