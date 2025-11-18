using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomianLayer.Contracts;
using DomianLayer.Exceptions;
using DomianLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.Dtos.BasketModule;


namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> CreatedOrUpdatedBasketAsync(BasketDto basketDto)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basketDto);
            var CreatedOrUpdatesBasket = await _basketRepository.CreatedOrUpdatedBasketAsync(CustomerBasket);

            if (CreatedOrUpdatesBasket is not null)
            {
                return await GetBasketAsync(basketDto.Id);
            }
            else
            {
                throw new Exception("Can Not Update Or Create Basket  Now");
            }


        }



        public async Task<BasketDto?> GetBasketAsync(string Key)
        {
            var Basket = await _basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else
            {
                throw new BasketNotFoundException(Key);
            }
        }

        public async Task<bool> DeleteBasketAsync(string Key)
     => await _basketRepository.DeleteBasketAsync(Key);
    }
}
