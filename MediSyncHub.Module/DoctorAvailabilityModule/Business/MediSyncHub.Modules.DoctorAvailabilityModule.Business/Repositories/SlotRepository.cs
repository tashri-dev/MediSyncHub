using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Repository;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Repositories;

internal class SlotRepository : ISlotRepository
{
    private readonly AvailabilityDbContext _context;

    public SlotRepository(AvailabilityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Slot>> GetAvailableSlotsAsync()
    {
        return await _context.Slots
            .Where(x => !x.IsReserved)
            .OrderBy(x => x.Time)
            .ToListAsync();
    }

    public async Task<Slot> GetByIdAsync(Guid id)
    {
        return await _context.Slots.FindAsync(id);
    }

    public async Task AddAsync(Slot slot)
    {
        await _context.Slots.AddAsync(slot);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Slot slot)
    {
        _context.Slots.Update(slot);
        await _context.SaveChangesAsync();
    }
}