namespace WebApplication1.Test.Fixtures;

public class TestFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        => services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddScoped<IProductRepository, ProductRepository>()
            .Configure<Options>(config => configuration?.GetSection(nameof(Options)).Bind(config));

    protected override ValueTask DisposeAsyncCore()
        => new();

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        yield return new() { Filename = "appsettings.json", IsOptional = false, };
    }
}
