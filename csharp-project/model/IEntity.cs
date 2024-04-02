namespace csharp_project.model
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}