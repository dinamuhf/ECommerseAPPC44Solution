using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomianLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Configurations
{
    public class DeliverMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(D => D.Cost)
                .HasColumnType("decimal(8,2)");
         
            builder.Property(D => D.ShortName)
               .HasColumnType("varchar")
               .HasMaxLength(50);
   
            builder.Property(D => D.DeliveryTime)
               .HasColumnType("varchar")
               .HasMaxLength(50);
          
            builder.Property(D => D.Description)
               .HasColumnType("varchar")
               .HasMaxLength(100);
            



        }
    }
}
