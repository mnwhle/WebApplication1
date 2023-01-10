namespace WebApplication1.Test.Tests;

[TestCaseOrderer("WebApplication1.Test.Orderers.AlphabeticalOrderer", "WebApplication1.Test")]
public class ProductRepositoryTestWithDependencyInjection : TestBed<TestFixture>
{
    public ProductRepositoryTestWithDependencyInjection(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        var options = _fixture.GetService<IOptions<Config.Options>>(_testOutputHelper);
        if (options is not null)
        {
            HibernateHelper.InitSessionFactory(options.Value.ConnectionString);
        }
    }

    [Theory]
    [InlineData("nhibernate")]
    [InlineData("fluent-validation")]
    [InlineData("fluent-nhibernate")]
    [InlineData("cat")]
    [InlineData("dog")]
    [InlineData("car")]
    [InlineData("plane")]
    [InlineData("train")]
    [InlineData("lift")]
    public async Task Test03_DeleteProductIfExists(string name)
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
    [InlineData("nhibernate")]
    [InlineData("fluent-validation")]
    [InlineData("fluent-nhibernate")]
    [InlineData("cat")]
    [InlineData("dog")]
    [InlineData("car")]
    [InlineData("plane")]
    [InlineData("train")]
    [InlineData("lift")]
    public async Task Test02_InsertProduct(string name)
    {
        var repo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper)!;
        Assert.NotNull(repo);

        Product model = new() { Name = name };
        var id = await repo.InsertAsync(model, default);
        Assert.NotNull(id);
    }
}