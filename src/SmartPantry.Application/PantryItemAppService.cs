using System;
using System.Linq;
using System.Threading.Tasks;
using SmartPantryApp.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SmartPantryApp;

public class PantryItemAppService :
    CrudAppService<
        PantryItem,
        PantryItemDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdatePantryItemDto>,
    IPantryItemAppService
{
    public PantryItemAppService(IRepository<PantryItem, Guid> repository)
        : base(repository)
    {
    }

    public override async Task<PantryItemDto> CreateAsync(
        CreateUpdatePantryItemDto input)
    {
        var item = new PantryItem(
            GuidGenerator.Create(),
            input.PantryId,
            input.Barcode,
            input.Expiration,
            input.Status,
            input.Note
        );

        await Repository.InsertAsync(item);

        return ObjectMapper.Map<PantryItem, PantryItemDto>(item);
    }

    protected override void MapToEntity(
        CreateUpdatePantryItemDto updateInput, PantryItem entity)
    {
        entity.Update(
            updateInput.Barcode,
            updateInput.Expiration,
            updateInput.Status,
            updateInput.Note
        );
    }

    protected override async Task DeleteByIdAsync(Guid id)
    {
        var item = await GetEntityByIdAsync(id);
        item.EnsureCanBeDeleted();
        await Repository.DeleteAsync(item);
    }

    protected override IQueryable<PantryItem> ApplyDefaultSorting(
        IQueryable<PantryItem> query)
    {
        return query.OrderBy(x => x.Expiration);
    }
}