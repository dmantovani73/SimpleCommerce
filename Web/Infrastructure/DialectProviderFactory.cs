using ServiceStack.OrmLite;
using System;

public static class DialectProviderFactory
{
    public static IOrmLiteDialectProvider Create(DialectProvider dialectProvider)
    {
        switch (dialectProvider)
        {
            case DialectProvider.Sqlite: return SqliteDialect.Provider;
            case DialectProvider.PostgreSQL: return PostgreSqlDialect.Provider;

            default: throw new NotSupportedException();
        }
    }
}