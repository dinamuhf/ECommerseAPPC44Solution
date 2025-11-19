using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using DomianLayer.Contracts;
using DomianLayer.Exceptions;
using DomianLayer.Models.OrderModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.Dtos.OrderModule;


namespace Service
{
    public class OrderService(IBasketRepository _basketRepository, IMapper _mapper,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrder(OrderRequest orderDto, string Email)
        {
          
            var OrderAddress = _mapper.Map<AddressDto, ShippingAddress>(orderDto.ShipToAddress);
      
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);
            ArgumentException.ThrowIfNullOrEmpty(Basket.PaymentIntentId);
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var OrderSpec = new orderWithPaymentIntentIdSpecifications(Basket.PaymentIntentId);
            var ExistingOrder =  await OrderRepo.GetByIdAsync(OrderSpec);

             if(ExistingOrder is not null)
            {
                OrderRepo.Remove(ExistingOrder);
            }
       
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
             foreach( var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);


                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrderd()
                    {
                        ProductId = Product.Id,
                        ProductName = Product.Name,
                        PictureUrl = Product.PictureUrl
                    },
                    Price= Product.Price,
                    Quantity= item.Quantity


                };


                OrderItems.Add(orderItem);
            }
       
            var DeliveryMethod =  await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

      
            var SubTotal = OrderItems.Sum(I => I.Quantity * I.Price);

           
            var Order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, SubTotal,Basket.PaymentIntentId);

            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderResult>(Order);


        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethods()
        {
            var DeliveryMethods =  await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodResult>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderResult>> GetAllOrdersAsync(string Email)
        {
            //var Spec = new OrderSpecifications(Email);
            //var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            //return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
            var spec = new OrderSpecifications(Email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderResult>>(orders);

        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid Id)
        {
            var Spec = new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return _mapper.Map<OrderResult>(Order);
        }
    }
}
