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
	public class RequestConfig : IEntityTypeConfiguration<Request>
	{
		public void Configure(EntityTypeBuilder<Request> builder)
		{
			builder.ToTable("Requests");

			builder.HasKey(k => k.Id);

			builder.Property(x => x.Id).UseIdentityColumn();


			builder.HasMany(p => p.UserRequests).WithOne(x => x.Request).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
		}

	}
}
