using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Request : IEntity
	{
		public int Id { get; set; }
		public int BaseId { get; set; }
		public int DepartmentId { get; set; }

		public int ConfirmationCount { get; set; }
		public DateTime Time { get; set; } 

		public List<UserRequest> UserRequests { get; set; } = new List<UserRequest>();


	}
}
