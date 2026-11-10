using System;
using Volo.Abp.Application.Dtos;

namespace SmartPantryApp;

public class PantryItemDto : EntityDto<Guid>
{
    public Guid PantryId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}