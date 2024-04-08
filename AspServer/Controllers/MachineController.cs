using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspServer.Models;
using AspServer.Services;

[ApiController]
[Route("api/[controller]")]
public class MachineController : ControllerBase
{
    private readonly MachineService _machineService;

    public MachineController(MachineService machineService)
    {
        _machineService = machineService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Machine>>> Get() =>
        await _machineService.Get();

    [HttpGet("{id:length(24)}", Name = "GetMachine")]
    public async Task<ActionResult<Machine>> Get(string id)
    {
        var machine = await _machineService.Get(id);

        if (machine == null)
        {
            return NotFound();
        }

        return machine;
    }

    [HttpPost]
    public async Task<ActionResult<Machine>> Create(Machine machine)
    {
        await _machineService.Create(machine);

        return CreatedAtRoute("GetMachine", new { id = machine.Id.ToString() }, machine);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Machine machineIn)
    {
        var machine = await _machineService.Get(id);

        if (machine == null)
        {
            return NotFound();
        }

        await _machineService.Update(id, machineIn);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var machine = await _machineService.Get(id);

        if (machine == null)
        {
            return NotFound();
        }

        await _machineService.Remove(machine.Id);

        return NoContent();
    }
}
