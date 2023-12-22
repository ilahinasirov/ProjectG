using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
	public class UserRequest
	{
		[Key]
		public int Id { get; set; }
		
		public int UserId { get; set; }
		
		public int RequestId { get; set; }
		public string Comment { get; set; }

		public User User { get; set; }
		public Request Request { get; set; }
	}
}
