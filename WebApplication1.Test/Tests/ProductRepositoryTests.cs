#if true

namespace WebApplication1.Test.Tests;

[TestCaseOrderer("WebApplication1.Test.Orderers.AlphabeticalOrderer1", "WebApplication1.Test")]
public class ProductRepositoryTests : TestBed<TestFixture>
{
    private readonly IProductRepository _sut;
    private readonly ILogger<ProductRepository> _logger = Substitute.For<ILogger<ProductRepository>>();

    public ProductRepositoryTests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        var options = _fixture.GetService<IOptions<Config.Options>>(_testOutputHelper);
        if (options is not null)
        {
            HibernateHelper.InitSessionFactory(options.Value.ConnectionString);
        }
        _sut = new ProductRepository(_logger);
    }

    public static IEnumerable<object[]> GetProducts()
    {
        string[] products = new string[] {
            "nhibernate",
            "fluent-validation",
            "fluent-nhibernate",
            "cat",
            "dog",
            "car",
            "plane",
            "train",
            "lift",
        };
        foreach (var product in products)
        {
            yield return new object[] { product };
        }
    }

    [Theory]
    [MemberData(nameof(GetProducts))]
    public async Task Test02_TryDeleteProduct_ThenInsertProduct(string name)
    {
        CancellationToken cancellationToken = CancellationToken.None;
        // Make sure the product does not exist.
        var model = await _sut.SelectByNameAsync(name, cancellationToken);
        if (model is not null)
        {
            await _sut.DeleteAsync(model.Id, cancellationToken);
        }
        // Insert a new record.
        model = new() { Name = name };
        var id = await _sut.InsertAsync(model, cancellationToken);
        Assert.NotNull(id);
    }
}
#endif
