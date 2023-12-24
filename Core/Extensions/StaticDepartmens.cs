using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Core.Extensions
{
	public class StaticDepartmens
	{
		public  List<Department> GetDepartments()
		{
			return new List<Department>
			{
				new Department { Id = 1, Name = "IT Department" },
				new Department { Id = 2, Name = "HR Department" },
				new Department { Id = 3, Name = "Sales Department" }
				
			};
		}
	}
}
