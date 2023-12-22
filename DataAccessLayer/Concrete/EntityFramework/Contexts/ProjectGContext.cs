using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccessLayer.Concrete.EntityFramework.Configurations;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.Concrete.EntityFramework.Contexts
{
	public class ProjectGContext:DbContext
	{

		public ProjectGContext(DbContextOptions<ProjectGContext> options) : base(options)
		{
		}

		public ProjectGContext()
		{
		}


		public DbSet<User> Users { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Request> Requests { get; set; }
		public DbSet<UserDepartment> UserDepartments { get; set; }
		public DbSet<UserRequest> UserRequests { get; set; }

		public DbSet<OperationClaim> OperationClaims { get; set; }
		public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfig());
			modelBuilder.ApplyConfiguration(new RequestConfig());
			modelBuilder.ApplyConfiguration(new DepartmentConfig());
			modelBuilder.ApplyConfiguration(new UserRequestConfig());
			modelBuilder.ApplyConfiguration(new UserDeparmentConfig());
			modelBuilder.ApplyConfiguration(new OperationClaimConfig());
			modelBuilder.ApplyConfiguration(new UserOperationClaimConfig());
		}
	}
}