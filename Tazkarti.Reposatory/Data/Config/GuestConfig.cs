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
    public class GuestConfig : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(g => g.Id);

            // Configure properties
            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(100);


            builder.HasOne(g => g.Party)
                   .WithMany(p => p.Guests)
                   .HasForeignKey(g => g.PartyId);

            builder.Property(g => g.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}
