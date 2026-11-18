using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace SmartPantry.PantryItems;

public abstract class SmartPantryService_Tests<TStartupModule> : SmartPantryApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ISmartPantryService _smartPantryService;

    protected SmartPantryService_Tests()
    {
        _smartPantryService = GetRequiredService<ISmartPantryService>();
    }


    [Fact]
    public async Task Should_Create_Valid_PantryItem()
    {
        //
        var a = new CreateUpdatePantryItemDto
        {

            Note = "Mayonesa",
            Status = "Cerrado",
            Expiration = DateTime.Now.AddMonths(3),
            Barcode = "7791234567890",
            PantryId = Guid.NewGuid()
        };
        var result = await _smartPantryService.CreateAsync(a
            
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.Name.ShouldBe("Mayonesa");
    }

    [Fact]
    public async Task Should_Not_Create_A_PantryItem_With_Empty_Barcode()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _smartPantryService.CreateAsync(
                new CreateUpdatePantryItemDto
                {

                    Note = "Mayonesa",
                    Status = "Cerrado",
                    Expiration = DateTime.Now.AddMonths(3),
                    //Barcode = "7791234567890",
                    PantryId = Guid.NewGuid()
                }
            );
        });

        exception.ValidationErrors
            .ShouldContain(err => err.MemberNames.Any(mem => mem == "Barcode"));
    }
}