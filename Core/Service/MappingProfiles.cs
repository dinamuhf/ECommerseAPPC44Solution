using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.ProductModule;
using Shared.DTOS;
using Shared.DTOS.BasketDtos;

namespace Service
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            #region Product
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                            .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductTybe.Name))
                            .ForMember(dist => dist.PictureUrl, options => options.MapFrom<PictureUrlResolver>());
            CreateMap<ProductTybe, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();




            #endregion
            #region Basket
            CreateMap<CustomerBasket, BasketDto>().ReverseMap;
            CreateMap<BasketItem, BasketItemDto>().ReverseMap;
            #endregion
        }
    }
}
