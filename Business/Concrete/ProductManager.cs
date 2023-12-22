using Business.Abstract;
using DataAccessLayer.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		private IProductDal _productDal;

		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}

		public Product GetById(int productId)
		{
			return _productDal.Get(p => p.ProductId == productId);
		}

		public List<Product>GetList()
		{
			return _productDal.GetList().ToList();
		}

		public List<Product> GetListByCategory(int categoryId)
		{
			return _productDal.GetList(p => p.CategoryID == categoryId).ToList();
		}

		public void Add(Product product)
		{
			// Buisness codlarini bura yaza bilerik.
			_productDal.Add(product);
			
		}

		public void Update(Product product)
		{
			_productDal.Update(product);
			
		}

		public void Delete(Product product)
		{
			_productDal.Delete(product);
			
		}
	}
}