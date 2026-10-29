using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPantryApp;

public class CreateUpdatePantryItemDto
{
    public Guid PantryId { get; set; }

    [Required]
    public string Barcode { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;
}