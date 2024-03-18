using System.Collections.Generic;
using csharp_project_new.model;

namespace csharp_project_new.repository
{
    public interface IRepository<TId, TE> where TE : IEntity<TId>
    {
        TE FindOne(TId id);
        IEnumerable<TE> FindAll();
        void Add(TE entity);
        void Delete(TId id);
        void Update(TId id, TE entity);
    }
}