using System;
using System.Threading.Tasks;
using Shouldly;
using SmartPantryApp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace SmartPantry;

public abstract class PantryItemCrudAppService_Tests<TStartupModule>
    : SmartPantryApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IPantryItemAppService _service;

    protected PantryItemCrudAppService_Tests()
    {
        _service = GetRequiredService<IPantryItemAppService>();
    }

    [Fact]
    public async Task Should_Register_List_Update_Get_And_Delete()
    {
        var created = await _service.CreateAsync(new CreateUpdatePantryItemDto
        {
            PantryId = Guid.NewGuid(),
            Barcode = "  7791234567890  ",
            Expiration = DateTime.Now.AddDays(30),
            Status = "cerrado",
            Note = "  Leche  "
        });

        created.Id.ShouldNotBe(Guid.Empty);
        created.Barcode.ShouldBe("7791234567890");
        created.Status.ShouldBe("Cerrado");
        created.Note.ShouldBe("Leche");

        var list = await _service.GetListAsync(
            new PagedAndSortedResultRequestDto { MaxResultCount = 10 });
        list.TotalCount.ShouldBeGreaterThan(0);
        list.Items.ShouldContain(x => x.Id == created.Id);

        var updated = await _service.UpdateAsync(created.Id,
            new CreateUpdatePantryItemDto
            {
                PantryId = created.PantryId,
                Barcode = "7790000000001",
                Expiration = DateTime.Now.AddDays(60),
                Status = "Abierto",
                Note = "  Ya abierta  "
            });

        updated.Barcode.ShouldBe("7790000000001");
        updated.Status.ShouldBe("Abierto");
        updated.Note.ShouldBe("Ya abierta");

        var found = await _service.GetAsync(created.Id);
        found.Barcode.ShouldBe("7790000000001");

        await _service.DeleteAsync(created.Id);

        await Assert.ThrowsAnyAsync<EntityNotFoundException>(async () =>   
            await _service.GetAsync(created.Id));
    }

    [Fact]
    public async Task Should_Not_Change_Item_When_Update_Is_Invalid()
    {
        var created = await _service.CreateAsync(new CreateUpdatePantryItemDto
        {
            PantryId = Guid.NewGuid(),
            Barcode = "7791234567890",
            Expiration = DateTime.Now.AddDays(30),
            Status = "Cerrado",
            Note = "original"
        });

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.UpdateAsync(created.Id,
                new CreateUpdatePantryItemDto
                {
                    PantryId = created.PantryId,
                    Barcode = "7790000000001",
                    Expiration = DateTime.Now.AddDays(60),
                    Status = "Inventado",
                    Note = "nueva"
                }));

        var found = await _service.GetAsync(created.Id);
        found.Barcode.ShouldBe("7791234567890");
        found.Status.ShouldBe("Cerrado");
        found.Note.ShouldBe("original");
    }

    [Fact]
    public async Task Should_Not_Delete_Consumed_Item()
    {
        var created = await _service.CreateAsync(new CreateUpdatePantryItemDto
        {
            PantryId = Guid.NewGuid(),
            Barcode = "7791234567890",
            Expiration = DateTime.Now.AddDays(30),
            Status = "Consumido",
            Note = ""
        });

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.DeleteAsync(created.Id));

        var found = await _service.GetAsync(created.Id);
        found.Id.ShouldBe(created.Id);
    }
}