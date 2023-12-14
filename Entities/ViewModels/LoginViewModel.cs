using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels;

public class LoginViewModel
{
	[DisplayName("İstifadəçi adı"),Required(ErrorMessage = "{0} sahəsi boş keçilməz!")]
	public string UserName  { get; set; }


	[DisplayName("Şifrə"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!"),DataType(DataType.Password)]
	public	string Password { get; set; }
}