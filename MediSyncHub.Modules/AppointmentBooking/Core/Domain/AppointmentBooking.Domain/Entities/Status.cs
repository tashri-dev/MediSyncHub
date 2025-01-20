namespace AppointmentBooking.Domain.Entities;

public enum Status : byte
{
    Pending = 0,
    Cancelled = 1,
    Completed = 2
}