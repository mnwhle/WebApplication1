#if true
namespace WebApplication1.Test.Tests;

[TestCaseOrderer("WebApplication1.Test.Orderers.AlphabeticalOrderer", "WebApplication1.Test")]
public class ProductRepositoryTestWithMoq : TestBed<TestFixture>
{
    private readonly IProductRepository sut;
    private readonly Mock<ILogger<ProductRepository>> loggerMock = new();

    public ProductRepositoryTestWithMoq(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        var options = _fixture.GetService<IOptions<Config.Options>>(_testOutputHelper);
        if (options is not null)
        {
            HibernateHelper.InitSessionFactory(options.Value.ConnectionString);
        }
        sut = new ProductRepository(loggerMock.Object);
        Assert.NotNull(sut);
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

    /// <summary>
    /// Deletes the existing record if it is found.
    /// </summary>
    /// <returns>Both true and false are valid results so there is nothing to assert.</returns>
    [Theory]
    [MemberData(nameof(GetProducts))]
    public async Task DeleteProductIfExists(string name)
    {
        var model = await sut.SelectByNameAsync(name, default);
        if (model is not null)
        {
            await sut.DeleteAsync(model.Id, default);
        }
    }

    [Theory]
    [MemberData(nameof(GetProducts))]
    public async Task Test02_InsertProduct(string name)
    {
        Product model = new() { Name = name };
        var id = await sut.InsertAsync(model, default);
        Assert.NotNull(id);
    }
}
#endif
