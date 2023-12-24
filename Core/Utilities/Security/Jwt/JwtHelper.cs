using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt
{
	public class JwtHelper:ITokenHelper
	{
		public IConfiguration  Configuration { get; }
		private TokenOptions _tokenOptions;
		private readonly DateTime _accessTokenExpiration;

		public JwtHelper(IConfiguration configuration)
		{
			Configuration = configuration;
			//_tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
			_tokenOptions = new TokenOptions
			{
				Audience = Configuration["TokenOptions:Audience"],
				Issuer = Configuration["TokenOptions:Issuer"],
				AccessTokenExpiration = Convert.ToInt32(Configuration["TokenOptions:AccessTokenExpiration"]),
				SecurityKey = Configuration["TokenOptions:SecurityKey"]
				// Diğer özellikleri de aynı şekilde alabilirsiniz.
			};
			_accessTokenExpiration= DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
		}

		public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
		{
			var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
			var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
			var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var token = jwtSecurityTokenHandler.WriteToken(jwt);

			return new AccessToken
			{
				Token = token,
				Expiration = _accessTokenExpiration
			};

		}

		public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
			SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
		{
			var jwt = new JwtSecurityToken(
				issuer:tokenOptions.Issuer,
				audience:tokenOptions.Audience,
				expires:_accessTokenExpiration,
				notBefore:DateTime.Now,
				claims: SetClaims(user, operationClaims) ,
				signingCredentials:signingCredentials);
			return jwt;
		}

		private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
		{
			var claims = new List<Claim>();
			claims.AddNameId(user.Id.ToString());
			claims.AddEmail(user.Email);
			claims.AddName(user.Name);
			claims.AddUserName(user.UserName);
			claims.AddRoles(operationClaims.Select(c=>c.Name).ToArray());
			return claims;


		}
		public static IEnumerable<Claim> GetClaims(string token)
		{
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

			if (jwtSecurityTokenHandler.CanReadToken(token))
			{
				var jwtSecurityToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;

				if (jwtSecurityToken != null)
				{
					return jwtSecurityToken.Claims;
				}
			}

			return null;
		}
	}
}
