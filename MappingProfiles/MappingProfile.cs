using AutoMapper;
using WebApi.Controllers.DTOs;
using WebApi.Entities;

namespace WebApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<ProductImage, ProductImageDto>();
        }
    }
}