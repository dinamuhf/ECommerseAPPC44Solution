using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomianLayer.Models.OrderModule;

namespace Service.Specifications
{
   public class orderWithPaymentIntentIdSpecifications:BaseSpecifications<Order,Guid>
    {
        public orderWithPaymentIntentIdSpecifications(string paymentIntentId)
     : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }


    }

}
