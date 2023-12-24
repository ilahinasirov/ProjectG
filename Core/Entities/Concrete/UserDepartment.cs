using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
	public class UserDepartment
	{
		[Key]
		public int Id { get; set; }
		//Foreign keys
		public int UserId { get; set; }
		public int DepartmentId { get; set; }

		// Navigation properties
		public User User { get; set; }
		public Department? Department { get; set; }


	}
}
