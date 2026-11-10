using Xunit;

namespace SmartPantry.EntityFrameworkCore.Applications;

[Collection(SmartPantryTestConsts.CollectionDefinitionName)]
public class EfCorePantryItemCrudAppService_Tests
    : PantryItemCrudAppService_Tests<SmartPantryEntityFrameworkCoreTestModule>
{
}