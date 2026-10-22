using SmartPantry.Localization;
using SmartPantryApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SmartPantryApp;

/* Inherit your application services from this class.
 */
public abstract class SmartPantryAppService : ApplicationService, ISmartPantryService
{
    private readonly IRepository<PantryItem, Guid> _smartPantryItemRepository;

    public SmartPantryAppService(IRepository<PantryItem, Guid> smartPantryItemRepository)
    {
        _smartPantryItemRepository = smartPantryItemRepository;
    }
    public async Task<List<PantryItemDto>> GetListAsync()
    {
        var items = await _smartPantryItemRepository.GetListAsync();
        return items
            .Select(item => new PantryItemDto
            {
                Id = item.Id,
                /* Text = item.text */
            }).ToList();
    }
    public async Task<PantryItemDto> CreateAsync(string note, string status, DateTime expiration, string barcode, Guid pantryId)
    {
        var pantryItem = await _smartPantryItemRepository.InsertAsync(
            new PantryItem
            {
                Note = note,
                Expiration = expiration,
                Barcode = barcode,
                PantryId = pantryId,
                Status = status
            }
        );

        return new PantryItemDto
        {
            Id = pantryItem.Id,
            Note = pantryItem.Note,
            Expiration = pantryItem.Expiration,
            Barcode = pantryItem.Barcode,
            Status = pantryItem.Status
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        await _smartPantryItemRepository.DeleteAsync(id);
    }
}