using System;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();

    void Rollback();

    void Commit();
}