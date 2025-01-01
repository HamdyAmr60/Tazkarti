using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;

namespace Tazkarti.Repository.Data.Config
{
    public class PartyConfig : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.HasKey(p => p.Id);

            // Configure properties
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.invitationUrl)
                   .HasMaxLength(300); // Adjust length as needed

            builder.Property(p => p.Time)
                   .IsRequired();

            // Configure relationships
            builder.HasMany(p => p.Guests)
                   .WithOne(g => g.Party)
                   .HasForeignKey(g => g.PartyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
