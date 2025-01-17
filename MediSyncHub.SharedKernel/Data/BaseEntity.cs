﻿namespace MediSyncHub.SharedKernel.Data;

public class BaseEntity<T>
{
    public required T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
