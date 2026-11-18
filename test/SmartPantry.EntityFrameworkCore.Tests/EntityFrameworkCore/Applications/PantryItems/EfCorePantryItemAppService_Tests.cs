using SmartPantry.PantryItems;
using Xunit;

namespace SmartPantry.EntityFrameworkCore.Applications.PantryItems;

[Collection(SmartPantryTestConsts.CollectionDefinitionName)]
public class EfCorePantryItemService_Tests : SmartPantryService_Tests<SmartPantryEntityFrameworkCoreTestModule>
{
}
