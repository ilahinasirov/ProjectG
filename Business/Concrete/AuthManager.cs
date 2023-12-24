using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
	public class AuthManager : IAuthService
	{
		private IUserService _userService;
		private ITokenHelper _tokenHelper;

		public AuthManager(IUserService userService, ITokenHelper tokenHelper)
		{
			_userService = userService;
			_tokenHelper = tokenHelper;
		}

		public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
		{
			byte[] passwordHash, passwordSalt;
			HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
			var user = new User
			{
				Email = userForRegisterDto.Email,
				Name = userForRegisterDto.Name,
				SurName = userForRegisterDto.SurName,
				UserName = userForRegisterDto.UserName,
				//UserDepartments = userForRegisterDto.SelectedDepartment,
				
				UserDepartments = new List<UserDepartment>
					{ new UserDepartment
						{
							DepartmentId = userForRegisterDto.SelectedDepartment

						}
					},
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				/*UserDepartments = userForRegisterDto.SelectedRole,*/
				Status = true

			};
			_userService.Add(user);
			return new SuccessDataResult<User>(user, Messages.UserRegisteredSuccessfully);
		}



		public IDataResult<User> Login(UserForLoginDto userForLoginDto)
		{
			var userToCheck = _userService.GetByUserName(userForLoginDto.UserName);
			if (userToCheck == null)
			{
				return new ErrorDataResult<User>(Messages.UserNotFound);
			}

			if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
			{
				return new ErrorDataResult<User>(Messages.PasswordNotFound);
			}

			return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
		}

		public IResult UserAlreadyExists(string userName)
		{
			if (_userService.GetByUserName(userName) != null)
			{
				return new ErrorResult(Messages.UserAlreadyExists);
			}

			return new SuccessResult();
		}

		public IDataResult<AccessToken> CreateAccessToken(User user)
		{
			var claims = _userService.GetClaims(user);
			var accessToken = _tokenHelper.CreateToken(user, claims);
			return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
		}
	}
}
