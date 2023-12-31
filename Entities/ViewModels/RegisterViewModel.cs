﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Entities.Concrete;


namespace Entities.ViewModels
{
	public class RegisterViewModel
	{
		[DisplayName("Ad"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!")]
		public string Name { get; set; }
		[DisplayName("Soyad"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!")]
		public string SurName { get; set; }

		[DisplayName("İstifadəçi adı"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!")]
		public string UserName { get; set; }



		[DisplayName("E-Poçt"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!"),
		 DataType(DataType.EmailAddress)]
		public string Email { get; set; }


		public List<UserDepartment> SelectedRole { get; set; }

		[DisplayName("Departament"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!")]
		public List<string> AvailableRoles { get; set; } = new List<string>();


		[DisplayName("Şifrə"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!"), DataType(DataType.Password)]
		public string Password { get; set; }


		[DisplayName("Təkrar Şifrə"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!"), DataType(DataType.Password),
		Compare("Password", ErrorMessage = "Şifrə ilə Təkrar Şifrə uyğun gəlmir.")]

		public string RePassword { get; set; }
	}
}
