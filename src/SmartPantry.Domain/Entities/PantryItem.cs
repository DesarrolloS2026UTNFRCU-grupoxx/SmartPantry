using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace SmartPantry.Entities
{
    public class PantryItem: BasicAggregateRoot<Guid>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid PantryId { get; set; } = Guid.Empty;
        public string Barcode { get; set; } = string.Empty;
        public DateTime Expiration { get; set; } = DateTime.MinValue;
        public string Status { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
