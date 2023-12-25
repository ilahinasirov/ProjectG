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
        public UserRequest(int userId, int requestId, string comment, string username, bool confirmationStatus)
        {
            UserId = userId;
			RequestId = requestId;
			Comment = comment;
            UserName = username;
            UpdateType = confirmationStatus;
        }

        public UserRequest()
        {
        }

        public int Id { get; set; }
		
		public int UserId { get; set; }
		
		public int RequestId { get; set; }
		public string Comment { get; set; }

        public DateTime RecordDate { get; set; }
        public string UserName { get; set; }

        public bool UpdateType { get; set; }

        public User User { get; set; }
		public Request Request { get; set; }
	}
}
