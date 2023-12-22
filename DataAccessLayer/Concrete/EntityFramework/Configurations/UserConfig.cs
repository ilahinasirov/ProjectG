using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.EntityFramework.Configurations
{
	public class UserConfig : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");

			builder.HasKey(k => k.Id);

			builder.Property(x => x.Id).UseIdentityColumn();
			builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(40);
			builder.Property(x => x.RolePriority).HasColumnType("nvarchar").HasMaxLength(40);
			builder.Property(x => x.SurName).HasColumnType("nvarchar").HasMaxLength(40);
			builder.Property(x => x.UserName).IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
			builder.Property(x => x.Email).HasColumnType("nvarchar").HasMaxLength(50);
			builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(40).IsRequired(false);
			builder.Property(x => x.PasswordHash).HasColumnType("varbinary(MAX)").HasMaxLength(8000);
			builder.Property(x => x.PasswordSalt).HasColumnType("varbinary(MAX)").HasMaxLength(8000);
			builder.Property(x => x.Status).HasColumnType("bit");


			builder.HasMany(p => p.UserRequests).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
			builder.HasMany(p => p.UserDepartments).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
			builder.HasMany(p => p.UserOperationClaims).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
		}
	}
}
