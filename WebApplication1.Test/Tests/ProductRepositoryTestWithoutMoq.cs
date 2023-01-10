namespace WebApplication1.Test.Tests;

[TestCaseOrderer("WebApplication1.Test.Orderers.AlphabeticalOrderer", "WebApplication1.Test")]
public class ProductRepositoryTestWithoutMoq : TestBed<TestFixture>
{
    public ProductRepositoryTestWithoutMoq(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        var options = _fixture.GetService<IOptions<Config.Options>>(_testOutputHelper);
        if (options is not null)
        {
            HibernateHelper.InitSessionFactory(options.Value.ConnectionString);
        }
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
    public async Task Test01_DeleteProductIfExists(string name)
    {
        var repo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper)!;
        Assert.NotNull(repo);

        var model = await repo.SelectByNameAsync(name, default);
        if (model is not null)
        {
            await repo.DeleteAsync(model.Id, default);
        }
    }

    [Theory]
    [MemberData(nameof(GetProducts))]
    public async Task Test02_InsertProduct(string name)
    {
        var repo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper)!;
        Assert.NotNull(repo);

        Product model = new() { Name = name };
        var id = await repo.InsertAsync(model, default);
        Assert.NotNull(id);
    }
}