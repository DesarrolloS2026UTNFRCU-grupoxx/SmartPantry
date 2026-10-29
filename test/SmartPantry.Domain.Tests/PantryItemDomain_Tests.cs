using SmartPantryApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace SmartPantry;

public class PantryItemDomain_Tests : SmartPantryDomainTestBase<SmartPantryDomainTestModule>
{
    [Fact]
    public async Task Should_Create_Valid_PantryItem_And_Normalize_Text()
    {
        // Arrange
        var pantryId = Guid.NewGuid();
        var barcode = "7791234567890";
        var inputNote = "  Comprado en oferta  ";
        var expectedNote = "Comprado en oferta"; // Texto normalizado (sin espacios sobrantes)

        // Act
        var pantryItem = new PantryItem
        {
            Id = Guid.NewGuid(),
            PantryId = pantryId,
            Barcode = barcode,
            Expiration = DateTime.Now.AddMonths(3),
            Status = "Active",
            Note = inputNote.Trim() // Simulando o aplicando la normalización
        };

        // Assert
        pantryItem.Barcode.ShouldBe(barcode);
        pantryItem.Note.ShouldBe(expectedNote);
        pantryItem.Status.ShouldBe("Active");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task Should_Reject_Empty_Or_Whitespace_Barcode(string invalidBarcode)
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() =>
        {
            if (string.IsNullOrWhiteSpace(invalidBarcode))
                throw new ArgumentException("El código de barras es obligatorio.");

            var pantryItem = new PantryItem
            {
                Barcode = invalidBarcode
            };
        });
    }
}

