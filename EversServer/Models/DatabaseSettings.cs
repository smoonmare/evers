namespace EversServer.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string MachineCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}