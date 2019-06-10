using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

public abstract class UnitOfWorkBase : IUnitOfWork
{
    protected readonly IDbConnection db;
    IDbTransaction tr;

    public UnitOfWorkBase(IDbConnectionFactory dbFactory)
    {
        db = dbFactory.Open();
    }

    public void BeginTransaction()
    {
        tr = db.OpenTransaction();
    }

    public void Commit()
    {
        tr?.Commit();
    }

    public void Rollback()
    {
        tr?.Rollback();
    }

    public void Dispose()
    {
        tr?.Dispose();
        db?.Dispose();
    }
}