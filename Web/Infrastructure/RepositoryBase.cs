using System.Data;

public abstract class RepositoryBase
{
    protected IDbConnection db;

    public RepositoryBase(IDbConnection db)
    {
        this.db = db;
    }
}