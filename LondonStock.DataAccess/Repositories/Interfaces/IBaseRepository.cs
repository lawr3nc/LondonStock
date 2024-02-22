using ErrorOr;

namespace LondonStock.DataAccess.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public ErrorOr<TEntity> Get(object id);
    public ErrorOr<Success> Add(TEntity entity);

    public ErrorOr<IEnumerable<TEntity>> GetAll();

}