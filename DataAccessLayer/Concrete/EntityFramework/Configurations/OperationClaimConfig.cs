using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace DataAccessLayer.Concrete.EntityFramework.Configurations
{
	public class OperationClaimConfig : IEntityTypeConfiguration<OperationClaim>
	{
		public void Configure(EntityTypeBuilder<OperationClaim> builder)
		{
			builder.ToTable("OperationClaims");

			builder.HasKey(k => k.Id);

			
			builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(40);

			builder.HasMany(p => p.UserOperationClaims).WithOne(x => x.OperationClaim).OnDelete(DeleteBehavior.Cascade).IsRequired(false);


		}
	}
}
