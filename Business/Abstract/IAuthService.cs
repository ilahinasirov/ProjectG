﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Abstract
{
	public interface IAuthService
	{
		IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
		IDataResult<User> Login(UserForLoginDto userForLoginDto);
		IResult UserAlreadyExists(string userName);
		IDataResult<AccessToken> CreateAccessToken(User user);
	}
}
