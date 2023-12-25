using Core.DataAccess.EntityframeWork;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccessLayer.Concrete.EntityFramework
{
	public class EfRequestDal : EfEntityRepositoryBase<Request, ProjectGContext>, IRequestDal
	{
		public EfRequestDal(ProjectGContext applicationContext) : base(applicationContext)
		{
		}
	}
}
