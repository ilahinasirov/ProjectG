using System.ComponentModel.DataAnnotations;
using Entities.Concrete;

namespace Core.Entities.Concrete
{
	public class User : IEntity
	{
		public User()
		{
			UserOperationClaims = UserOperationClaims ?? new List<UserOperationClaim>();
		}
		public int Id { get; set; }

		public int RolePriority { get; set; }

		public string Name { get; set; }

		public string SurName { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public byte []  PasswordSalt{ get; set; }
		public byte []  PasswordHash{ get; set; }
		public bool Status { get; set; }


		public List<UserDepartment> UserDepartments { get; set; } = new List<UserDepartment>();
		public List<UserRequest> UserRequests { get; set; } = new List<UserRequest>();
		public virtual List<UserOperationClaim> UserOperationClaims { get; set; }
	}
}
