using AutoMapper;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.BusinessLogicLayer.DTO;

namespace eCommerce.BusinessLogicLayer.Mappers
{
    public class ProductUpdateRequestToProductProfile : Profile
    {
        public ProductUpdateRequestToProductProfile()
        {
            CreateMap<ProductUpdateRequest, Product>();
        }
    }
}
