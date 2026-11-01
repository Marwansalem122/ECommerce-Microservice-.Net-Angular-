using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;

namespace eCommerce.BusinessLogicLayer.Mappers
{
    internal class ProductToProductResponseProfile : Profile
    {
        public ProductToProductResponseProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForCtorParam("Category", opt => opt.MapFrom(src => ParseCategory(src.Category)));
        }

        private static CategoryOptions ParseCategory(string category)
        {
            return Enum.TryParse<CategoryOptions>(category, true, out var parsedCategory)
                ? parsedCategory
                : default;
        }
    }
}