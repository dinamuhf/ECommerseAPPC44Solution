using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.OrderModule;


namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderResult> CreateOrder(OrderRequest orderDto, string Email);
        Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethods();
        Task<IEnumerable<OrderResult>> GetAllOrdersAsync(string Email);
        Task<OrderResult> GetOrderByIdAsync(Guid Id);

    }
}
