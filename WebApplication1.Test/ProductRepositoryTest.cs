namespace WebApplication1.Test;

public class ProductRepositoryTest : TestBed<TestFixture>
{
    public ProductRepositoryTest(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        var options = _fixture.GetService<IOptions<Options>>(_testOutputHelper);
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
    public void InsertProduct(string name)
    {
        var repo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper)!;
        Assert.NotNull(repo);

        Task.Run(() => TryDeleteThenInsert(repo, name, CancellationToken.None));
    }

    private async Task TryDeleteThenInsert(IProductRepository repo, string name, CancellationToken cancellationToken)
    {
        await DeleteProductAsync(repo, name, cancellationToken);
        var id = await InsertProductAsync(repo, name, cancellationToken);
        Assert.NotNull(id);
    }

    private async Task<bool> DeleteProductAsync(IProductRepository repo, string name, CancellationToken cancellationToken)
    {
        var model = await repo.SelectByNameAsync(name, cancellationToken);
        if (model is not null)
        {
            return await repo.DeleteAsync(model.Id, cancellationToken);
        }
        return false;
    }

    private async Task<int?> InsertProductAsync(IProductRepository repo, string name, CancellationToken cancellationToken)
    {
        Product model = new() { Name = name };
        return await repo.InsertAsync(model, cancellationToken);
    }
}