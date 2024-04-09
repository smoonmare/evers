using EversServer.Interfaces;

namespace EversServer.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string MachineCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}