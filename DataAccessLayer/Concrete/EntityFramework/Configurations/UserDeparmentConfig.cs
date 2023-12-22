using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.EntityFramework.Configurations
{
	public class UserDeparmentConfig : IEntityTypeConfiguration<UserDepartment>
	{
		public void Configure(EntityTypeBuilder<UserDepartment> builder)
		{
			builder.ToTable("UserDepartments");

			builder.HasKey(k => k.Id);

			builder.Property(x => x.Id).UseIdentityColumn();


			builder.HasOne(p => p.Department).WithMany(x => x.UserDepartments).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
			builder.HasOne(p => p.User).WithMany(x => x.UserDepartments).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
		}
	}
}
