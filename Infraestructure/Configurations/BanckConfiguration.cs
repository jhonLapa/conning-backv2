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
    public class BanckConfiguration : IEntityTypeConfiguration<Banck>
    {
        public void Configure(EntityTypeBuilder<Banck> builder)
        {
            builder.ToTable("Banks");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.ShortName).HasColumnName("ShortName");
            builder.Property(e => e.SwiftCode).HasColumnName("SwiftCode");
            builder.Property(e => e.CountryCode).HasColumnName("CountryCode");
            builder.Property(e => e.State).HasColumnName("State");
        }
    }
}
