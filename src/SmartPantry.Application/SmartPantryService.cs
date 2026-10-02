using SmartPantry.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TodoApp;

public class TodoAppService : ApplicationService, ISmartPantryService
{
    private readonly IRepository<PantryItem, Guid> _todoItemRepository;

    public TodoAppService(IRepository<PantryItem, Guid> todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    // TODO: Implement the methods here...
}

// aca creamos/implementamos los servicios 
public async Task<PantryItemDto> CreateAsync(string note, string status, DateTime expiration, string barcode, Guid pantryId)
    {
        var pantryItem = await _todoItemRepository.InsertAsync(
            new PantryItem { Note = note, 
                             Expiration = expiration,
                             Barcode = barcode,
                             PantryId = pantryId,
                             Status = status}
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
