using MongoDB.Driver;
using MongoDB.Bson;
using EversServer.Interfaces;
using EversServer.Models;

namespace EversServer.Services
{
    public class MachineService
    {
        private readonly IMongoCollection<Machine> _machines;
        private readonly ILogger<MachineService> _logger;

        public MachineService(IDatabaseSettings settings, ILogger<MachineService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _machines = database.GetCollection<Machine>(settings.MachineCollectionName);
            _logger = logger;
        }

        public async Task<List<Machine>> GetAsync() =>
            await _machines.Find(_ => true).ToListAsync();

        public async Task<Machine?> GetAsync(string id)
        {
            _logger.LogInformation("Searching for machine with ID {MachineId}", id);
            var machine = await _machines.Find<Machine>(machine => machine.Id == id).FirstOrDefaultAsync();

            if (machine == null)
            {
                _logger.LogWarning("Machine with ID {MachineId} not found", id);
            }

            return machine;
        }

        public async Task CreateAsync(Machine machine)
        {
            _logger.LogInformation("Creating a new machine with ID {MachineId}", machine.Id);
            await _machines.InsertOneAsync(machine);
        }

        // Method for handling direct updates with a Machine object
        public async Task<bool> UpdatePartialAsync(string id, Machine machineIn)
        {
            // Log the start of the update
            _logger.LogInformation("Attempting to update machine with ID {MachineId}", id);

            // Create a filter to find the document to update
            var filter = Builders<Machine>.Filter.Eq(m => m.Id, id);

            // Log the replacement document for debugging
            var machine = await _machines.Find(machine => machine.Id == id).FirstOrDefaultAsync();
            _logger.LogInformation("Original Document: {Machine}", Newtonsoft.Json.JsonConvert.SerializeObject(machine));
            _logger.LogInformation("Replacement Document: {MachineIn}", Newtonsoft.Json.JsonConvert.SerializeObject(machineIn));

            // Log the filter being used
            _logger.LogInformation("Using filter for update: {Filter}", filter);

            // Attempt to replace the found document with the supplied Machine object
            var replaceResult = await _machines.ReplaceOneAsync(filter, machineIn, new ReplaceOptions { IsUpsert = false });

            // Log the result of the operation
            _logger.LogInformation("Replace result: IsAcknowledged={IsAcknowledged}, ModifiedCount={ModifiedCount}",
                replaceResult.IsAcknowledged, replaceResult.ModifiedCount);
            

            // Return true if the operation was acknowledged and at least one document was modified
            var success = replaceResult.IsAcknowledged && replaceResult.ModifiedCount > 0;

            if (!success)
            {
                _logger.LogWarning("Failed to update machine with ID {MachineId}", id);
            }

            return success;
        }

        

        public async Task<bool> RemoveAsync(string id)
        {
            var deleteResult = await _machines.DeleteOneAsync(machine => machine.Id == id);
            var success = deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

            if (!success)
            {
                _logger.LogWarning("Failed to delete machine with ID {MachineId}", id);
            }

            return success;
        }
    }
}