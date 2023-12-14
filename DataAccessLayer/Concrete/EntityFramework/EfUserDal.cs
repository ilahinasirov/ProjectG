using Core.DataAccess.EntityframeWork;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace DataAccessLayer.Concrete.EntityFramework
{
	public class EfUserDal : EfEntityRepositoryBase<User, ProjectGContext>, IUserDal
	{
	}
}
