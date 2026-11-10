using SmartPantry.PantryItems;
using Xunit;

namespace SmartPantry.EntityFrameworkCore.Applications;

[Collection(SmartPantryTestConsts.CollectionDefinitionName)]
public class EfCorePantryItemAppService_Tests
    : PantryItemAppService_Validation_Tests<SmartPantryEntityFrameworkCoreTestModule>
{
}
