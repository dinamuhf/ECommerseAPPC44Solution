using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomianLayer.Models.OrderModule;

namespace Service.Specifications
{
    public class OrderSpecifications:BaseSpecifications<Order,Guid>
    {
        //Get All Orders By Email
        public OrderSpecifications(string Email):base(o=>o.buyerEmail==Email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.items);
            AddOrderByDescending(o => o.orderDate);

        }
         //Get Order By Id
         public OrderSpecifications(Guid Id):base(o=>o.Id==Id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.items);
        }
    }
}
