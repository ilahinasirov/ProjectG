using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace DataAccessLayer.Concrete.EntityFramework.Configurations
{
	public class UserOperationClaimConfig : IEntityTypeConfiguration<UserOperationClaim>
	{
		public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
		{
			builder.ToTable("UserOperationClaims");

			builder.HasKey(k => k.Id);


			builder.Property(x => x.UserId).HasColumnType("int").HasMaxLength(40);
			builder.Property(x => x.OperationClaimId).HasColumnType("int").HasMaxLength(40);


			builder.HasOne(p => p.User).WithMany(x => x.UserOperationClaims).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
			builder.HasOne(p => p.OperationClaim).WithMany(x => x.UserOperationClaims).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
		}
	}
}
