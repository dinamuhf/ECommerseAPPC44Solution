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
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.subtotal)
                    .HasColumnType("decimal(8,2)");
      
            builder.HasMany(o => o.items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
           
            builder.HasOne(o => o.DeliveryMethod)
               .WithMany()
               .HasForeignKey(o => o.DeliveryMethodId);
       
            builder.OwnsOne(o => o.shipToAddress);

        }
    }
}
