using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configurations
{
    public class PensionConfiguration : IEntityTypeConfiguration<Pension>
    {
        public void Configure(EntityTypeBuilder<Pension> builder)
        {
            builder.ToTable("PensionFunds");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Name).HasColumnName("Name");
        }
    }
}
