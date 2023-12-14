using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.Concrete.EntityFramework.Contexts
{
	public class ProjectGContext:DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

			optionsBuilder.UseSqlServer(@"Server=WIN-9P2F6MA0QH8\SQLEXPRESS;Database=ProjectG;Trusted_Connection=true;TrustServerCertificate=true");

			//optionsBuilder.UseSqlServer(@"Data Source=WIN-9P2F6MA0QH8\SQLEXPRESS;Initial Catalog=ProjectG;Integrated Security=True");

		}




		public DbSet<User> Users { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Request> Requests { get; set; }
		public DbSet<UserDepartment> UserDepartments { get; set; }
		public DbSet<UserRequest> UserRequests { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Many-to-Many ilişkilerini belirtmek için
			modelBuilder.Entity<UserDepartment>()
				.HasKey(ud => new { ud.UserId, ud.DepartmentId });

			modelBuilder.Entity<UserRequest>()
				.HasKey(ur => new { ur.UserId, ur.RequestId });

			// User - UserDepartment Many-to-Many ilişkisi
			modelBuilder.Entity<User>()
				.HasMany(u => u.UserDepartments)
				.WithOne(ud => ud.User)
				.HasForeignKey(ud => ud.UserId);

			modelBuilder.Entity<Department>()
				.HasMany(d => d.UserDepartments)
				.WithOne(ud => ud.Department)
				.HasForeignKey(ud => ud.DepartmentId);

			// Request - RequestUser Many-to-Many ilişkisi
			modelBuilder.Entity<Request>()
				.HasMany(r => r.UserRequests)
				.WithOne(ur => ur.Request)
				.HasForeignKey(ur => ur.RequestId);

			modelBuilder.Entity<User>()
				.HasMany(u => u.UserRequests)
				.WithOne(ur => ur.User)
				.HasForeignKey(ur => ur.UserId);

			// Diğer konfigürasyonları buraya ekleyebilirsiniz.
		}
	}
}
