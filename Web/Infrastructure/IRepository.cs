using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IRepository<T> where T : class, IHasId
{
    List<T> GetAll();

    Task<List<T>> GetAllAsync();

    List<T> Get(Expression<Func<T, bool>> exp);

    Task<List<T>> GetAsync(Expression<Func<T, bool>> exp);

    T GetById(int id);

    Task<T> GetByIdAsync(int id);

    T GetById(string id);

    Task<T> GetByIdAsync(string id);

    bool Save(T o);

    Task<bool> SaveAsync(T o);

    int Delete(int id);

    Task<int> DeleteAsync(int id);

    int Delete(Expression<Func<T, bool>> exp);

    Task<int> DeleteAsync(Expression<Func<T, bool>> exp);
}