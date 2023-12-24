using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
	public class AccessToken
	{
		//JWT-də faktiki məlumatı(faydalı yükü) təmsil edən sahə. İstifadəçi ID, rollar və ya digər məlumatları
		//burada saxlayacam.
		public string Token {get; set; }

		//Tokenin etibarlılıq müddətini əks etdirən sahə. Müəyyən bir müddətdən sonra tokeni etibarsız edəcəm.
		public DateTime Expiration {get; set; }
	}
}
