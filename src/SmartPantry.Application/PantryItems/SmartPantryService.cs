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
using Volo.Abp.ObjectMapping;

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
        return new PantryItemDto
        {
            Id = item.Id,
            Note = item.Note,
            Expiration = item.Expiration,
            Barcode = item.Barcode,
            Status = item.Status,
            PantryId = item.PantryId
        };
    }
    public async Task<PantryItemDto> CreateAsync(CreateUpdatePantryItemDto input)
    {
        var pantryItem = ObjectMapper.Map<CreateUpdatePantryItemDto, PantryItem>(input);
        await _smartPantryItemRepository.InsertAsync(pantryItem);
        return ObjectMapper.Map<PantryItem, PantryItemDto>(pantryItem);
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

        var itemsDtos = ObjectMapper.Map<List<PantryItem>, List<PantryItemDto>>(items);
        //await SetAuthorNamesAsync(itemsDtos);

        return new PagedResultDto<PantryItemDto>(
            totalCount,
            itemsDtos
        );
    }   
        public async Task<PantryItemDto> UpdateAsync(Guid id, CreateUpdatePantryItemDto input)
    {
        var item = await _smartPantryItemRepository.GetAsync(id);
        ObjectMapper.Map(input, item);
        await _smartPantryItemRepository.UpdateAsync(item);
        return ObjectMapper.Map<PantryItem, PantryItemDto>(item);
    }
}
