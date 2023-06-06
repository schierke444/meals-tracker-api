﻿namespace BuildingBlocks.Events;
public class CategoryRecordResult
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}