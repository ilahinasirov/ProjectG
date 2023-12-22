using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Business.Abstract
{
	public interface IUserService
	{
		List<OperationClaim> GetClaims(User user);

		User GetByMail (string mail);
		User GetByUserName (string userName);

		void Add(User user);


		// second
		public void Register(User user, string hashedPassword);
		void Update(User user);
		User GetById(int userId);
		
		User Login(string userName, string password);

		void Delete(User user);
	}
}
