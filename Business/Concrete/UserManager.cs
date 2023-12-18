﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Concrete
{
	public class UserManager : IUserService
	{
		private IUserDal _userDal;



		public UserManager(IUserDal userDal)
		{
			_userDal = userDal;

		}




		public void Register(User user,string hashedPassword)
		{


			// Kullanıcı adı ve e-posta adresi daha önce kullanılmış mı kontrolü
			var existingUser = _userDal.Get(u => u.UserName == user.UserName || u.Email == user.Email);
			if (existingUser != null)
			{



				throw new Exception("Istifadəçi adı və ya E-poçt artıq mövcuddur");


			}
			//hashla
			user.Password = hashedPassword;


			_userDal.Add(user);
		}

		public User Login(string userName, string password)
		{

			var user = _userDal.Get(u => u.UserName == userName);
			if (user != null && PasswordHelper.VerifyPassword(password, user.Password))
			{
				return user;
			}
			

			return null;



		}
		

		public void Update(User user)
		{

			_userDal.Update(user);
		}

		public void Delete(User user)
		{

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

