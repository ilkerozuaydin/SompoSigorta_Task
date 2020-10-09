using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mappings
{
    public class ProposalMapping: IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.Property(t => t.ProductNo).HasMaxLength(20);
            builder.ToTable("Proposal", "product");
        }
    }
}
