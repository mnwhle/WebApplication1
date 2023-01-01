namespace WebApplication1.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, CreateProductRequest>().ReverseMap();
        CreateMap<Product, UpdateProductRequest>().ReverseMap();
    }
}
