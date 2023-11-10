using AutoMapper;
using BusinessModel.Entities;
using DataAccess.Dtos;
using DataAccess.Dtos.RequestModel;

namespace DataAccess.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.CategoryName, 
                y => y.MapFrom(src => src.Category.CategoryName))
            .ReverseMap();

        CreateMap<Order, OrderDto>()
            .ForMember(x => x.MemberFirstname, y => y.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.MemberLastname, y => y.MapFrom(src => src.User.LastName))
            .ReverseMap();

        CreateMap<Order, OrderDetailDto>()
            .ReverseMap();

        CreateMap<AddProductRequest, Product>().ReverseMap();
        CreateMap<UpdateProductRequest, Product>().ReverseMap();

        CreateMap<AddOrderRequest, Order>().ReverseMap();
        CreateMap<AddOrderDetailRequest, OrderDetail>().ReverseMap();
    }
}