using SmartPantry.PantryItems;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace SmartPantryApp.Entities;

public class PantryItem: BasicAggregateRoot<Guid>
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid PantryId { get; set; } = Guid.Empty;
    public string Barcode { get; set; } = string.Empty;
    public DateTime Expiration { get; set; } = DateTime.MinValue;
    public ItemStatus Status { get; set; }
    public string Note { get; set; } = string.Empty;
}
