using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
				throw new Exception("Istifadəçi adı və ya E-poçt artıq mövcuddur");

				// İlgili hata mesajlarını ve yönlendirmeleri ekleyebilirsiniz.
				//ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten kullanılıyor.");
				//ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
				// veya başka bir işlem...
			}

			// İş kodları buraya gelecek (varsa)

			// Kullanıcıyı kaydet
			_userDal.Add(user);
		}

		public User Login(string userName, string password)
		{
			
				var user = _userDal.Get(u => u.UserName == userName && u.Password == password);


				return user;
			

			
		}

		public void Update(User user)
		{
			// Kullanıcıyı güncelleme işlemleri burada gerçekleştirilir
			_userDal.Update(user);
		}

		public void Delete(User user)
		{
			// Kullanıcıyı silme işlemleri burada gerçekleştirilir
			_userDal.Delete(user);
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

