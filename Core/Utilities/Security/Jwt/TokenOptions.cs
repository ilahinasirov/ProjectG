using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
	public class TokenOptions
	{
		//istfiadeci kutlesi
		public string Audience { get; set; }
		//imzalayan
		public string Issuer { get; set; }
		//token muddeti deqiqe ile
		public int AccessTokenExpiration { get; set; }
		public string SecurityKey { get; set; }
	}
}
