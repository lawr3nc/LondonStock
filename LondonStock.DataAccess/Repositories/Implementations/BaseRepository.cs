using System.Collections;
using System.Reflection;
using ErrorOr;
using LondonStock.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace LondonStock.DataAccess.Repositories.Implementations;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    protected static readonly Hashtable _lseDataStore = new();
    public ILogger<BaseRepository<TEntity>> _logger;

    protected BaseRepository(ILogger<BaseRepository<TEntity>> logger)
    {
        _logger = logger;
    }

    public virtual ErrorOr<TEntity> Get(object id)
    {
        var entity = _lseDataStore[id];

        if (entity != null)
            return ErrorOrFactory.From((TEntity)entity);
        
        _logger.LogInformation($"Entity of type {typeof(TEntity).Name} is not found");
        return Error.NotFound(
            code: "Not Found",
            description: $"{typeof(TEntity).Name} not Found");

    }

    public virtual ErrorOr<Success> Add(TEntity entity)
    {
        object id = GetId(entity);
        _lseDataStore.Add(id, entity);

        return Result.Success;
    }

    public ErrorOr<IEnumerable<TEntity>> GetAll()
    {
        List<TEntity> items = new();

        foreach(object value in _lseDataStore.Values)
        {
            _logger.LogDebug($"Type of value: {value.GetType().Name} and typeof TEntity: {typeof(TEntity).Name}");

            if(value.GetType().Name == typeof(TEntity).Name)
                items.Add((TEntity)value);
        }

        return items.ToErrorOr<IEnumerable<TEntity>>();
    }

    private object GetId(TEntity entity){
        Type t = entity.GetType();
        PropertyInfo prop = t.GetProperty("Id");
        return prop.GetValue(entity);
    }
}