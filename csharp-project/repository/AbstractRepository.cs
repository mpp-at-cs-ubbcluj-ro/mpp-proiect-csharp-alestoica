using System.Collections.ObjectModel;
using System.Data;
using csharp_project.model;
namespace csharp_project.repository;

public class AbstractRepository<TId, TE> : IRepository<TId, TE> where TE : IEntity<TId> where TId : notnull
{
    private IDictionary<TId, TE> Entities;

    public AbstractRepository()
    {
        Entities = new Dictionary<TId, TE>();
    }

    public TE FindOne(TId id)
    {
        if (Entities.ContainsKey(id))
            return Entities[id];
        throw new ReadOnlyException("This entity doesn't exist!");
    }

    public IEnumerable<TE> FindAll()
    {
        return Entities.Values;
    }

    public void Add(TE entity)
    {
        if (Entities.ContainsKey(entity.Id))
            throw new ReadOnlyException("This entity already exist!");
        Entities.Add(entity.Id, entity);
    }

    public void Delete(TId id)
    {
        if (Entities.ContainsKey(id))
            Entities.Remove(id);
        throw new ReadOnlyException("This entity doesn't exist!");
    }

    public void Update(TId id, TE entity)
    {
        if (Entities.ContainsKey(id))
            Entities[id] = entity;
        throw new ReadOnlyException("This entity doesn't exist!");
    }

    public ICollection<TE> GetAll()
    {
        return Entities.Values;
    }
}