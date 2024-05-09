using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using EversServer.Models;
using EversServer.Services;

namespace EversServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly MachineService _machineService;
        private readonly ILogger<MachineController> _logger;

        public MachineController(MachineService machineService, ILogger<MachineController> logger)
        {
            _machineService = machineService;
            _logger = logger;
        }

        // GET: api/Machine
        [HttpGet]
        public async Task<ActionResult<List<Machine>>> Get()
        {
            _logger.LogInformation("Handling GET request for all machines");
            return await _machineService.GetAsync();
        }

        // GET: api/Machine/5
        [HttpGet("{id:length(24)}", Name = "GetMachine")]
        public async Task<ActionResult<Machine>> Get(string id)
        {
            _logger.LogInformation("Handling GET request for machine with ID {MachineId}", id);
            var machine = await _machineService.GetAsync(id);

            if (machine == null)
            {
                _logger.LogWarning("Machine with ID {MachineId} not found", id);
                return NotFound();
            }

            return machine;
        }

        // POST: api/Machine
        [HttpPost]
        public async Task<ActionResult<Machine>> Post([FromBody] Machine machine)
        {
            _logger.LogInformation("Handling POST request to create a new machine");
            await _machineService.CreateAsync(machine);

            return CreatedAtRoute("GetMachine", new { id = machine.Id?.ToString() }, machine);
        }

        // PUT: api/Machine/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody] Machine updatedMachine)
        {
            if (updatedMachine == null || updatedMachine.Id != id)
            {
                _logger.LogWarning("Received null Patch document for machine with ID {MachineId}", id);
                return BadRequest("Invalid machine data.");
            }

            var existingMachine = await _machineService.GetAsync(id);
            if (existingMachine == null)
            {
                _logger.LogWarning("Machine with ID {MachineId} not found", id);
                return NotFound();
            }

            var updateResult = await _machineService.UpdatePartialAsync(id, updatedMachine);
            if (!updateResult)
            {
                _logger.LogWarning("Failed to update machine with ID {MachineId}", id);
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Machine/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Handling DELETE request for machine with ID {MachineId}", id);
            var machine = await _machineService.GetAsync(id);

            if (machine == null)
            {
                _logger.LogWarning("Machine with ID {MachineId} not found", id);
                return NotFound();
            }

            var deleteResult = await _machineService.RemoveAsync(id);

            if (!deleteResult)
            {
                _logger.LogWarning("Failed to delete machine with ID {MachineId}", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}