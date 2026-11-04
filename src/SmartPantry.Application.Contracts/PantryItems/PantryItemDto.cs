using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using System.Text;

namespace SmartPantry.PantryItems
{
    public class PantryItemDto : AuditedEntityDto<Guid>
    {
        public string Barcode { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime Added { get; set; }
        public string? Note { get; set; }
    }
}
