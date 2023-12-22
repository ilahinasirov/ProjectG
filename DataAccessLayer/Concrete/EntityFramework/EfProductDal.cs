using Core.DataAccess.EntityframeWork;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccessLayer.Concrete.EntityFramework
{
	public class EfProductDal : EfEntityRepositoryBase<Product, ProjectGContext>, IProductDal
	{
		public EfProductDal(ProjectGContext context) : base(context)
		{
		}
	}
}