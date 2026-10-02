using System;

namespace SmartPantry;

public class PantryItemDto
{
    public Guid Id { get; set; }
    public Guid PantryId { get; set; } = Guid.Empty;
    public string Barcode { get; set; } = string.Empty;
    public DateTime Expiration { get; set; } = DateTime.MinValue;
    public string Status { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}