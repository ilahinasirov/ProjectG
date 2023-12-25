using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Resources.Enum;
using Entities.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IUserDal: IEntityRepository<User>
	{
		List<OperationClaim> GetClaims(User user);
		User GetUserWithRelations(int id);

		List<Department> GetUserDepartmentIds(int userId);
		List<Department> GetAllDepartments();

		List<Request> GetUserRequests(int userId);

		int UpdateUserRequest(UserRequest userRequest, ActionType actionType);
	}
}
