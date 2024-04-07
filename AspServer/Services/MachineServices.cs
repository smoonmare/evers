using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace.Models; // Replace with your actual namespace

public class MachineService
{
    private readonly IMongoCollection<Machine> _machines;

    public MachineService(IYourDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _machines = database.GetCollection<Machine>(settings.MachinesCollectionName);
    }

    public async Task<List<Machine>> Get() =>
        await _machines.Find(machine => true).ToListAsync();

    public async Task<Machine> Get(string id) =>
        await _machines.Find<Machine>(machine => machine.Id == id).FirstOrDefaultAsync();

    public async Task<Machine> Create(Machine machine)
    {
        await _machines.InsertOneAsync(machine);
        return machine;
    }

    public async Task Update(string id, Machine machineIn) =>
        await _machines.ReplaceOneAsync(machine => machine.Id == id, machineIn);

    public async Task Remove(string id) => 
        await _machines.DeleteOneAsync(machine => machine.Id == id);
}
