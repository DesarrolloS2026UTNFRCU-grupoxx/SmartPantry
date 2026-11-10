using SmartPantry.PantryItems;
using System;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace SmartPantryApp.Entities;

public class PantryItem : BasicAggregateRoot<Guid>
{
    public const int MaxBarcodeLength = 50;
    public const int MaxNoteLength = 500;

    public Guid PantryId { get; private set; }
    public string Barcode { get; private set; } = string.Empty;
    public DateTime Expiration { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public string Note { get; private set; } = string.Empty;

    protected PantryItem()
    {
    }

    public PantryItem(Guid id, Guid pantryId, string barcode,
                      DateTime expiration, string status, string? note)
        : base(id)
    {
        PantryId = pantryId;
        Barcode = CheckBarcode(barcode);
        Expiration = CheckExpiration(expiration);
        Status = CheckStatus(status);
        Note = CheckNote(note);
    }

    public void Update(string barcode, DateTime expiration,
                       string status, string? note)
    {
        var newBarcode = CheckBarcode(barcode);
        var newExpiration = CheckExpiration(expiration);
        var newStatus = CheckStatus(status);
        var newNote = CheckNote(note);

        Barcode = newBarcode;
        Expiration = newExpiration;
        Status = newStatus;
        Note = newNote;
    }

    public void EnsureCanBeDeleted()
    {
        if (Status == nameof(ItemStatus.Consumido))
            throw new InvalidOperationException(
                "un ítem consumido no puede eliminarse");
    }

    private static string CheckBarcode(string barcode)
    {
        var value = barcode?.Trim();
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("barcode no puede estar vacío");
        if (value.Length > MaxBarcodeLength)
            throw new ArgumentException("barcode demasiado largo");
        return value;
    }

    private static DateTime CheckExpiration(DateTime expiration)
    {
        if (expiration == default)
            throw new ArgumentException("expiration no es una fecha válida");
        return expiration;
    }

    private static string CheckStatus(string status)
    {
        var value = status?.Trim();
        var canonical = Enum.GetNames(typeof(ItemStatus))
            .FirstOrDefault(n => string.Equals(
                n, value, StringComparison.OrdinalIgnoreCase));
        if (canonical == null)
            throw new ArgumentException("status no es un estado permitido");
        return canonical;
    }

    private static string CheckNote(string? note)
    {
        var value = note?.Trim() ?? string.Empty;
        if (value.Length > MaxNoteLength)
            throw new ArgumentException("note demasiado larga");
        return value;
    }
}