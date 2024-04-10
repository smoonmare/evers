using MongoDB.Driver;
using EversServer.Interfaces;
using EversServer.Models;
using System.Collections.Generic;

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

        // Method for handling direct updates with a Machine object
        public async Task<bool> UpdatePartialAsync(string id, Machine machineIn)
        {
            // Create a filter to find the document to update
            var filter = Builders<Machine>.Filter.Eq(m => m.Id, id);

            // Attempt to replace the found document with the supplied Machine object
            var replaceResult = await _machines.ReplaceOneAsync(filter, machineIn, new ReplaceOptions { IsUpsert = false });

            // Return true if the operation was acknowledged and at least one document was modified
            return replaceResult.IsAcknowledged && replaceResult.ModifiedCount > 0;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var deleteResult = await _machines.DeleteOneAsync(machine => machine.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}