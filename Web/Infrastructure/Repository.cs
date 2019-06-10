using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class Repository<T> : RepositoryBase, IRepository<T> where T : class, IHasId
{
    public Repository(IDbConnection db) : base(db)
    { }

    public virtual List<T> GetAll() => db.Select<T>();

    public virtual Task<List<T>> GetAllAsync() => db.SelectAsync<T>();

    public virtual List<T> Get(Expression<Func<T, bool>> exp) => db.Select<T>(exp);

    public virtual Task<List<T>> GetAsync(Expression<Func<T, bool>> exp) => db.SelectAsync<T>(exp);

    public virtual T GetById(int id) => db.SingleById<T>(id);

    public virtual Task<T> GetByIdAsync(int id) => db.SingleByIdAsync<T>(id);

    public virtual T GetById(string id) => db.SingleById<T>(id);

    public virtual Task<T> GetByIdAsync(string id) => db.SingleByIdAsync<T>(id);

    public bool Save(T o) => db.Save<T>(o);

    public virtual Task<bool> SaveAsync(T o) => db.SaveAsync<T>(o);

    public int Delete(int id) => db.DeleteById<T>(id);

    public Task<int> DeleteAsync(int id) => db.DeleteByIdAsync<T>(id);

    public int Delete(Expression<Func<T, bool>> exp) => db.Delete<T>(exp);

    public Task<int> DeleteAsync(Expression<Func<T, bool>> exp) => db.DeleteAsync<T>(exp);
}