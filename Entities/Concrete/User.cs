	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Core.Entities;

	namespace Entities.Concrete
	{
		public class User : IEntity
		{
			[Key]
			public int Id { get; set; }
			
			public int RolePriority { get; set; }
			public string Name { get; set; }
			public string SurName { get; set; }
			[MaxLength(20, ErrorMessage = "UserName cannot exceed 20 characters.")]
			public string UserName { get; set; }
			[EmailAddress(ErrorMessage = "Invalid email format.")]
			public string Email { get; set; }
			[DataType(DataType.Password)]
			public string Password { get; set; }
			public string PhoneNumber { get; set; }


			public List<UserDepartment> UserDepartments { get; set; } = new List<UserDepartment>();
			public List<UserRequest> UserRequests { get; set; } = new List<UserRequest>();
		}
	}
