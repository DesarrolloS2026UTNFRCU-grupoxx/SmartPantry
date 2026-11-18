using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPantry.PantryItems;

public class CreateUpdatePantryItemDto
{
    [Required]
    public Guid PantryId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Barcode { get; set; } = string.Empty;

    [Required]
    public DateTime Expiration { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;
}