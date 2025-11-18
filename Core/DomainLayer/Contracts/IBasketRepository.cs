using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomianLayer.Models.BasketModule;

namespace DomianLayer.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Key);
        Task<CustomerBasket?> CreatedOrUpdatedBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null);
        Task<bool> DeleteBasketAsync(string Id);
    }
}