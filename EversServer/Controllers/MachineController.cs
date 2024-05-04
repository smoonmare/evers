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

        public MachineController(MachineService machineService)
        {
            _machineService = machineService;
        }

        // GET: api/Machine
        [HttpGet]
        public async Task<ActionResult<List<Machine>>> Get()
        {
            return await _machineService.GetAsync();
        }

        // GET: api/Machine/5
        [HttpGet("{id:length(24)}", Name = "GetMachine")]
        public async Task<ActionResult<Machine>> Get(string id)
        {
            var machine = await _machineService.GetAsync(id);

            if (machine == null)
            {
                return NotFound();
            }

            return machine;
        }

        // POST: api/Machine
        [HttpPost]
        public async Task<ActionResult<Machine>> Post([FromBody] Machine machine)
        {
            await _machineService.CreateAsync(machine);

            return CreatedAtRoute("GetMachine", new { id = machine.Id?.ToString() }, machine);
        }

        // PATCH: api/Machine/5
        [HttpPatch("{id:length(24)}")]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<Machine> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var machineFromDb = await _machineService.GetAsync(id);

            if (machineFromDb == null)
            {
                return NotFound();
            }

            // Correct error handling in ApplyTo
            patchDoc.ApplyTo(machineFromDb, (error) =>
            {
                ModelState.AddModelError(error.Operation.path, error.ErrorMessage);
            });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Since the machineFromDb is now patched, you need to map it back to a dictionary if your UpdatePartialAsync expects one
            // Alternatively, you can adjust your service to directly accept the patched machine entity if that's more appropriate
            var updateResult = await _machineService.UpdatePartialAsync(id, machineFromDb); // This line may need adjustment based on your implementation

            if (!updateResult)
            {
                return NotFound();
            }

            return NoContent();
        }


        // DELETE: api/Machine/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var machine = await _machineService.GetAsync(id);

            if (machine == null)
            {
                return NotFound();
            }

            var deleteResult = await _machineService.RemoveAsync(id);

            if (!deleteResult)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}