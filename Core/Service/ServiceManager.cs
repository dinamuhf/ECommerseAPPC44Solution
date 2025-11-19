using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomianLayer.Contracts;
using DomianLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository _basketRepository,UserManager<ApplicationUser> _userManager,IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, mapper));
        private readonly Lazy<IAuthenticationService> _LazyAthuenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager,configuration,mapper));
        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_basketRepository, mapper, unitOfWork));
        private readonly Lazy<IPaymentService> _LazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(configuration,_basketRepository,unitOfWork,mapper));
        public IProductService ProductService => _LazyProductService.Value;

        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _LazyAthuenticationService.Value;

        public IOrderService OrderService => _LazyOrderService.Value;

        public IPaymentService PaymentService => _LazyPaymentService.Value;
    }
}
