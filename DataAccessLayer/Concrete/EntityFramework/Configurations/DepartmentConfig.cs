using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.EntityFramework.Configurations
{
	public class DepartmentConfig : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.ToTable("Departments");

			builder.HasKey(k => k.Id);

			builder.Property(x => x.Id).UseIdentityColumn();


			builder.HasMany(p => p.UserDepartments).WithOne(x => x.Department).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
		}
	}
}
