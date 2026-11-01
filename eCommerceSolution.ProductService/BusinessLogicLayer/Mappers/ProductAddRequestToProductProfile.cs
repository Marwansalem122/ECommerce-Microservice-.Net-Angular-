using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;

namespace eCommerce.BusinessLogicLayer.Mappers
{
    public class ProductAddRequestToProductProfile:Profile
    {
        public ProductAddRequestToProductProfile()
        {
            //CreateMap<Product, ProductAddRequest>();
            CreateMap<ProductAddRequest, Product>()
                .ForMember(dest => dest.ProductID, opt => opt.Ignore());
        }
    }
}
