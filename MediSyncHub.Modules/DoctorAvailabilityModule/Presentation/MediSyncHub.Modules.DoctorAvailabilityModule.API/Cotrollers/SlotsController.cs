using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Dtos;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.API.Cotrollers;

[ApiController]
[Route("api/[controller]")]
public class SlotsController(ISlotService slotService) : ControllerBase
{
    [HttpGet("get-all-slots")]
    public async Task<ActionResult<IEnumerable<SlotDto>>> ListAllSlots(CancellationToken cancellationToken = default)
    {
        var slots = await slotService.GetAllSlotsAsync(cancellationToken);
        return Ok(slots);
    }

    [HttpPost("create-slot")]
    public async Task<ActionResult<SlotDto>> CreateSlot(CreateSlotDto request)
    {
        var slot = await slotService.CreateSlotAsync(request);

        return CreatedAtAction(nameof(ListAllSlots), slot);
    }
}