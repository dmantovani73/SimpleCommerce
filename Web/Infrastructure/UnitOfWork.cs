using ServiceStack.Data;

public class UnitOfWork : UnitOfWorkBase
{
    public UnitOfWork(IDbConnectionFactory dbFactory = null) : base(dbFactory)
    { }

    IRepository<Product> products;

    public IRepository<Product> Products => products ?? (products = new Repository<Product>(db));
}