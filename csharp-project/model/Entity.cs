namespace csharp_project.model;

public interface IEntity<TId>
{
    public TId Id { get; set; }
}