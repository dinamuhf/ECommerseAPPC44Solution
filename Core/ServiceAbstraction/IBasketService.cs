using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.BasketModule;


namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string Key);
        Task<BasketDto?> CreatedOrUpdatedBasketAsync(BasketDto basketDto);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
