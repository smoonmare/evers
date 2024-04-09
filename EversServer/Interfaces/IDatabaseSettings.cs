namespace EversServer.Interfaces
{
    public interface IDatabaseSettings
    {
        string? MachineCollectionName { get; set; }
        string? ConnectionString { get; set; }
        string? DatabaseName { get; set; }
    }
}