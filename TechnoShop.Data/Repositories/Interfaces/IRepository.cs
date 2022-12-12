namespace TechnoShop.Data.Repositories.Interfaces;

public interface IRepository<TEntity>
    where TEntity : class
{
    IQueryable<TEntity> GetAll();

    Task<TEntity> GetById(string id);

    Task Add(TEntity entity);

    void Update(string id, TEntity entity);

    Task Delete(string id);

    Task Save();
}