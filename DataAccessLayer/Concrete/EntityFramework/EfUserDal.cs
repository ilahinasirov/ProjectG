using Core.DataAccess.EntityframeWork;
using Core.Entities.Concrete;
using Core.Utilities.Resources.Enum;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
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

        public List<Department> GetAllDepartments()
        {
            return Context.Departments.ToList();
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

        public List<Department> GetUserDepartmentIds(int userId)
        {
            var result = Context.Departments.Where(x => x.Id == userId).ToList();
            return result;
        }

        public List<Request> GetUserRequests(int userId)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            var userWithDepartments = _context.Users
                                      .Include(u => u.UserDepartments)
                                      .Include(u => u.UserRequests)
                                      .FirstOrDefault(u => u.Id == userId);

            if (userWithDepartments == null || !userWithDepartments.UserDepartments.Any())
            {
                return new List<Request>();
            }

            var departmentIds = userWithDepartments.UserDepartments.Select(ud => ud.DepartmentId).ToList();

            var requests = _context.Requests
                                   .Where(r => departmentIds.Contains(r.DepartmentId) && r.ConfirmationCount == userWithDepartments.RolePriority - 1)
                                   .Include(u => u.UserRequests)
                                   .ToList();

            return requests;
        }

        public User GetUserWithRelations(int id)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            var result = _context.Users.Where(x => x.Id == id)
                .Include(x => x.UserRequests)
                .Include(x => x.UserDepartments)
                .FirstOrDefault();
            return result;
        }

        public int UpdateUserRequest(UserRequest userRequest, ActionType actionType)
        {
            var request = Context.Requests.Where(x => x.Id == userRequest.RequestId).FirstOrDefault();

            if (actionType == ActionType.ConfirmRequest) request.ConfirmationCount++;
            else request.ConfirmationCount--;

            Context.UserRequests.Add(userRequest);
            return Context.SaveChanges();
        }
    }
}
