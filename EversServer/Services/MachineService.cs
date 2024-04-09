using MongoDB.Driver;
using EversServer.Interfaces;
using EversServer.Models;

namespace EversServer.Services
{
    public class MachineService
    {
        private readonly IMongoCollection<Machine> _machines;

        public MachineService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _machines = database.GetCollection<Machine>(settings.MachineCollectionName);
        }

        public async Task<List<Machine>> GetAsync() =>
            await _machines.Find(_ => true).ToListAsync();

        public async Task<Machine?> GetAsync(string id) =>
            await _machines.Find<Machine>(machine => machine.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Machine machine) =>
            await _machines.InsertOneAsync(machine);

        public async Task<bool> UpdateAsync(string id, Machine machineIn)
        {
            var updateResult = await _machines.ReplaceOneAsync(
                machine => machine.Id == id, machineIn, new ReplaceOptions { IsUpsert = false });
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        // Updated method with acknowledgment check
        public async Task<bool> RemoveAsync(string id)
        {
            var deleteResult = await _machines.DeleteOneAsync(machine => machine.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

    }
}