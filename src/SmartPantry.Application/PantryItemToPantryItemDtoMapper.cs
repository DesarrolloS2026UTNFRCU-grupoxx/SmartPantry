using Riok.Mapperly.Abstractions;
using SmartPantryApp.Entities;
using Volo.Abp.Mapperly;

namespace SmartPantryApp;

[Mapper]
public partial class PantryItemToPantryItemDtoMapper : MapperBase<PantryItem, PantryItemDto>
{
    public override partial PantryItemDto Map(PantryItem source);

    public override partial void Map(PantryItem source, PantryItemDto destination);
}