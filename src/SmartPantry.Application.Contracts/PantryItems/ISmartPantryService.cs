using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartPantry.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace SmartPantry.PantryItems;

public interface ISmartPantryService : ICrudAppService<
    PantryItemDto, //Used to show pantry items
    Guid, //Primary key of the pantry item entity
    PagedAndSortedResultRequestDto,
    CreateUpdatePantryItemDto> //Used to create/update a pantry item
{
    //Task<List<PantryItemDto>> GetListAsync();
    //Task<PantryItemDto> CreateAsync(string note, string status, DateTime expiration, string barcode, Guid pantryId);
    //Task<PantryItemDto> CreateAsync(PantryItemDto input);

    //Task DeleteAsync(Guid id);
    Task<PantryItemDto> GetAsync(Guid id);
}

