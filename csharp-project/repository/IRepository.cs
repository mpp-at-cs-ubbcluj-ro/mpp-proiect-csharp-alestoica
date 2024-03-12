using System.Collections.ObjectModel;
using csharp_project.model;
namespace csharp_project.repository;

public interface IRepository<TId, TE> where TE : IEntity<TId>
{
    TE FindOne(TId id);
    IEnumerable<TE> FindAll();
    void Add(TE entity);
    void Delete(TId id);
    void Update(TId id, TE entity);
    ICollection<TE> GetAll();
}