﻿using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace DataAccessLayer.Abstract
{
	public interface IUserDal: IEntityRepository<User>
	{
		List<OperationClaim> GetClaims(User user);
		User GetUserWithRelations(int id);
	}
}
