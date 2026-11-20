using SmartPantry.Localization;
using SmartPantry.PantryItems;
using SmartPantry.Permissions;
using SmartPantry.Shared;
using SmartPantryApp.Entities;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SmartPantry.PantryItems;

public class SmartPantryService : ApplicationService, ISmartPantryService
{
    private readonly IRepository<PantryItem, Guid> _smartPantryItemRepository;

    public SmartPantryService(IRepository<PantryItem, Guid> smartPantryItemRepository)
    {
        _smartPantryItemRepository = smartPantryItemRepository;
    }
    /*public async Task<List<PantryItemDto>> GetListAsync()
    {
        var items = await _smartPantryItemRepository.GetListAsync();
        return items
            .Select(item => new PantryItemDto
            {
                Id = item.Id
            }).ToList();
    }*/
    public async Task<PantryItemDto> GetAsync(Guid id)
    {
        var item = await _smartPantryItemRepository.GetAsync(id);
        return ToDto(item);
    }
    public async Task<PantryItemDto> CreateAsync(CreateUpdatePantryItemDto input)
    {
        var pantryItem = new PantryItem
        {
            Id = GuidGenerator.Create(),
            Name = input.Name,
            PantryId = input.PantryId,
            Barcode = input.Barcode,
            Expiration = input.Expiration,
            Status = ParseStatus(input.Status),
            Note = input.Note
        };
        await _smartPantryItemRepository.InsertAsync(pantryItem);
        return ToDto(pantryItem);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _smartPantryItemRepository.DeleteAsync(id);
    }
    public async Task<PagedResultDto<PantryItemDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _smartPantryItemRepository.GetQueryableAsync();
        var query = queryable
            //.OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var items = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        var itemsDtos = items.Select(ToDto).ToList();
        //await SetAuthorNamesAsync(itemsDtos);

        return new PagedResultDto<PantryItemDto>(
            totalCount,
            itemsDtos
        );
    }   
        public async Task<PantryItemDto> UpdateAsync(Guid id, CreateUpdatePantryItemDto input)
    {
        var item = await _smartPantryItemRepository.GetAsync(id);
        item.Name = input.Name;
        item.PantryId = input.PantryId;
        item.Barcode = input.Barcode;
        item.Expiration = input.Expiration;
        item.Status = ParseStatus(input.Status);
        item.Note = input.Note;
        await _smartPantryItemRepository.UpdateAsync(item);
        return ToDto(item);
    }

    private static ItemStatus ParseStatus(string value)
    {
        if (!Enum.TryParse<ItemStatus>(value, true, out var status) || !Enum.IsDefined(status))
        {
            throw new ArgumentException("El estado del ítem no es válido.", nameof(value));
        }

        return status;
    }

    private static PantryItemDto ToDto(PantryItem item) => new()
    {
        Id = item.Id,
        Name = item.Name,
        PantryId = item.PantryId,
        Barcode = item.Barcode,
        Expiration = item.Expiration,
        Status = item.Status,
        Note = item.Note
    };
}
