using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartPantryApp;

public interface ISmartPantryService : IApplicationService
{
    Task<List<PantryItemDto>> GetListAsync();
    Task<PantryItemDto> CreateAsync(string note, string status, DateTime expiration, string barcode, Guid pantryId);
    Task DeleteAsync(Guid id);
}

