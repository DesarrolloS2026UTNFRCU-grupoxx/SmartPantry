using Shouldly;
using SmartPantryApp;
using SmartPantryApp.Entities;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using Xunit;

namespace SmartPantry;

public class PantryItemAppService_Validation_Tests : SmartPantryApplicationTestBase<SmartPantryApplicationTestModule>
{
    private readonly ISmartPantryService _pantryItemAppService;
    private readonly IRepository<PantryItem, Guid> _pantryItemRepository;

    public PantryItemAppService_Validation_Tests()
    {
        _pantryItemAppService = GetRequiredService<ISmartPantryService>();
        _pantryItemRepository = GetRequiredService<IRepository<PantryItem, Guid>>();
    }

    [Fact]
    public async Task Should_Create_And_Get_PantryItem_By_Id()
    {
        var pantryId = Guid.NewGuid();

        var createdDto = await _pantryItemAppService.CreateAsync(
            note: "Leche descremada",
            status: "Active",
            expiration: DateTime.Now.AddDays(30),
            barcode: "7798765432109",
            pantryId: pantryId);

        var itemInDb = await _pantryItemRepository.GetAsync(createdDto.Id);

        itemInDb.ShouldNotBeNull();
        itemInDb.Barcode.ShouldBe("7798765432109");
        itemInDb.Note.ShouldBe("Leche descremada");
        itemInDb.Status.ShouldBe("Active");
    }
    public async Task Should_Fail_when_barcode_isEmpty()
    {
        var pantryId = Guid.NewGuid();

        var createdDto = await _pantryItemAppService.CreateAsync(
            note: "Leche descremada",
            status: "Active",
            expiration: DateTime.Now.AddDays(30),
            barcode: "",
            pantryId: pantryId);

        var
    }

}