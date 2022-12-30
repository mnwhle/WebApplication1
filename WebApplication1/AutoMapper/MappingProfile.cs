namespace WebApplication1.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponce>().ReverseMap();

        CreateMap<Product, CreateProductRequest>().ReverseMap();
    }
}
