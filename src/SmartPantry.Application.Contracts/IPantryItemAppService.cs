using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SmartPantryApp;

public interface IPantryItemAppService :
    ICrudAppService<
        PantryItemDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdatePantryItemDto>
{
}