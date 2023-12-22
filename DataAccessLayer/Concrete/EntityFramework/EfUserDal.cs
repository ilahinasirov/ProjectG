using Core.DataAccess.EntityframeWork;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.EntityFramework
{
	public class EfUserDal : EfEntityRepositoryBase<User, ProjectGContext>, IUserDal
	{
		private readonly ProjectGContext _context;
		public EfUserDal(ProjectGContext context) : base(context)
		{
			_context = context;
		}

		public List<OperationClaim> GetClaims(User user)
		{
			
				var result = from operationClaim in _context.OperationClaims
							 join
								 userOperationClaims in _context.UserOperationClaims on operationClaim.Id equals
								 userOperationClaims.UserId
							 where userOperationClaims.UserId == user.Id
							 select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
				return result.ToList();
		}

		public User GetUserWithRelations(int id)
		{
			_context.ChangeTracker.LazyLoadingEnabled = false;
			var result = _context.Users.Where(x => x.Id == id)
				.Include(x=>x.UserRequests)
				.Include(x=>x.UserDepartments)
				.FirstOrDefault();
			return result;
		}
	}
}
