using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IUserService
	{
		public void Register(User user, string hashedPassword);
		void Update(User user);
		User GetById(int userId);
		User GetByUserName(string userName);
		User Login(string userName, string password);

		void Delete(User user);
	}
}
