using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using DomianLayer.Models.BasketModule;
using DomianLayer.Models.IdentityModule;
using DomianLayer.Models.OrderModule;
using Service.MappingProfiles;
using Shared.Dtos.BasketModule;
using Shared.Dtos.OrderModule;
using Shared.Dtos.ProductModule;
namespace Service
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            #region Product
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<Product, ProductResultDto>()
     .ForMember(dest => dest.productType, options => options.MapFrom(src => src.ProductType.Name))
     .ForMember(dest => dest.productBrand, options => options.MapFrom(src => src.ProductBrand.Name))
     .ForMember(dest => dest.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            #endregion
            #region Basket
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            #endregion
            #region Identity
            CreateMap<Address, AddressDto>().ReverseMap();
            #endregion
            #region Order
            CreateMap<DeliveryMethod, DeliveryMethodResult>()
                     .ForMember(dest => dest.Cost, options => options.MapFrom(src => src.Cost));
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.Product.PictureUrl));
            CreateMap<Order, OrderResult>();
     //.ForMember(dest => dest.buyerEmail, opt => opt.MapFrom(src => src.buyerEmail))
     //.ForMember(dest => dest.shipToAddress, opt => opt.MapFrom(src => src.shipToAddress))
     //.ForMember(dest => dest.items, opt => opt.MapFrom(src => src.items))
     //.ForMember(dest => dest.status, opt => opt.MapFrom(src => src..ToString()))
     //.ForMember(dest => dest.deliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
     //.ForMember(dest => dest.deliveryCost, opt => opt.MapFrom(src => src.DeliveryMethod.Cost))
     //.ForMember(dest => dest.subtotal, opt => opt.MapFrom(src => src.SubTotal))
     //.ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.SubTotal + src.DeliveryMethod.Cost))
     //.ForMember(dest => dest.orderDate, opt => opt.MapFrom(src => src.OrderDate));


            #endregion

        }
    }
}
