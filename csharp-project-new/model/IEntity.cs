namespace csharp_project_new.model
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}