using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using Entities.ViewModels;

namespace Business.Concrete
{
	public class UserManager:IUserService
	{
		private IUserDal _userDal;

		public UserManager(IUserDal userDal)
		{
			_userDal = userDal;
		}

		public void Register(User user)
		{
			
			// Kullanıcı adı ve e-posta adresi daha önce kullanılmış mı kontrolü
			var existingUser = _userDal.Get(u => u.UserName == user.UserName || u.Email == user.Email);
			if (existingUser != null)
			{
				

				// Kullanıcı adı veya e-posta adresi zaten kullanılıyor
				throw new Exception("Istifadəçi adı vəya E-poçt artıq mövcuddur");
				
				// İlgili hata mesajlarını ve yönlendirmeleri ekleyebilirsiniz.
				// ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten kullanılıyor.");
				// ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
				return; // veya başka bir işlem...
			}

			// İş kodları buraya gelecek (varsa)

			// Kullanıcıyı kaydet
			_userDal.Add(user);
		}

		public User GetById(int userId)
		{
			return _userDal.Get(u => u.Id == userId);
		}

		public User GetByUserName(string userName)
		{
			return _userDal.Get(u => u.UserName == userName);
		}
	}
}

