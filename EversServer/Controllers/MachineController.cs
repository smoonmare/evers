using Microsoft.AspNetCore.Mvc;
using EversServer.Models;
using EversServer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EversServer.Controllers
{
    [Route("api/[controller]")]
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

        // PUT: api/Machine/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody] Machine machineIn)
        {
            var machine = await _machineService.GetAsync(id);

            if (machine == null)
            {
                return NotFound();
            }

            var updateResult = await _machineService.UpdateAsync(id, machineIn);

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