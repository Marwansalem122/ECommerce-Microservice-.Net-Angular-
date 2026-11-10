using AutoMapper;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.Mappers
{
    public class OrderUpdateRequestToOrderMappingProfile : Profile
    {
        public OrderUpdateRequestToOrderMappingProfile()
        {
            CreateMap<OrderUpdateRequest, Order>()
                .ForMember(destination => destination._id, options => options.Ignore())
                .ForMember(destination => destination.TotalBill, options => options.Ignore());
        }
    }
}
