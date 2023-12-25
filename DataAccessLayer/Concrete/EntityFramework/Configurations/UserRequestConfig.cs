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
	public class UserRequestConfig : IEntityTypeConfiguration<UserRequest>
	{
		public void Configure(EntityTypeBuilder<UserRequest> builder)
		{
			builder.ToTable("UserRequests");

			builder.HasKey(k => k.Id);

			builder.Property(x => x.Id).UseIdentityColumn();

			builder.Property(x => x.RecordDate).HasDefaultValue(DateTime.UtcNow);


			builder.HasOne(p => p.Request).WithMany(x => x.UserRequests).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
			builder.HasOne(p => p.User).WithMany(x => x.UserRequests).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
		}
	}
}
