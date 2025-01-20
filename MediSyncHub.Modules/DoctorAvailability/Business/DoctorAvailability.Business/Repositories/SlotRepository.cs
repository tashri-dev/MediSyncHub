using DoctorAvailability.Business.Data;
using DoctorAvailability.Data.Entities;
using DoctorAvailability.Data.Repository;

using Microsoft.EntityFrameworkCore;

namespace DoctorAvailability.Business.Repositories;

internal class SlotRepository(DoctorAvailabilityDbContext context) : ISlotRepository
{
    public async Task<IEnumerable<Slot>> GetAvailableSlotsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Slots
            .Where(x => !x.IsReserved)
            .OrderBy(x => x.Time)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Slot>> GetAllSlotsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Slots.ToListAsync(cancellationToken);
    }

    public async Task<Slot> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Slots.FindAsync(id, cancellationToken) ?? throw new Exception(" Not Found");
    }

    public async Task AddAsync(Slot slot, CancellationToken cancellationToken = default)
    {
        await context.Slots.AddAsync(slot, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Slot slot, CancellationToken cancellationToken = default)
    {
        context.Slots.Update(slot);
        await context.SaveChangesAsync(cancellationToken);
    }
}