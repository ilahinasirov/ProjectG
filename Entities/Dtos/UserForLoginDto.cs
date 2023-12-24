using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Dtos.Abstract;

namespace Entities.Dtos
{
    public class UserForLoginDto:IDto
	{
		
			[DisplayName("İstifadəçi adı"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!")]
			public string UserName { get; set; }


			[DisplayName("Şifrə"), Required(ErrorMessage = "{0} sahəsi boş keçilməz!"), DataType(DataType.Password)]
			public string Password { get; set; }
		
	}
}
