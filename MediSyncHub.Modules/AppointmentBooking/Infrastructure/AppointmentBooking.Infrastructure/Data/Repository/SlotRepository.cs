using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Repository;
using AppointmentBooking.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Data.Repository;

public class SlotRepository(BookingDbContext context) : ISlotRepository
{
    private readonly BookingDbContext _context = context;

    public async Task<IEnumerable<Slot>> GetAvailableSlotsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Slots
            .Where(x => !x.IsReserved && x.Time > DateTime.UtcNow)
            .OrderBy(x => x.Time)
            .ToListAsync(cancellationToken);
    }

    public async Task<Slot> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Slots
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new Exception("Slot not found");
    }

    public async Task AddAsync(Slot slot, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Slots.AddAsync(slot, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task UpdateAsync(Slot slot)
    {
        _context.Slots.Update(slot);
        return Task.CompletedTask;
    }
}