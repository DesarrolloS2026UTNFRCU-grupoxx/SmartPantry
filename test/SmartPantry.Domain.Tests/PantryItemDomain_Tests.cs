using System;
using SmartPantryApp.Entities;
using Shouldly;
using Xunit;

namespace SmartPantry;

public class PantryItemDomain_Tests
{
    [Fact]
    public void Should_Create_Valid_PantryItem_And_Normalize_Text()
    {
        var pantryId = Guid.NewGuid();
        var barcode = "7791234567890";
        var inputNote = "  Comprado en oferta  ";
        var expectedNote = "Comprado en oferta";

        var pantryItem = new PantryItem(
            Guid.NewGuid(),
            pantryId,
            barcode,
            DateTime.Now.AddMonths(3),
            "Consumido",
            inputNote);

        pantryItem.Barcode.ShouldBe(barcode);
        pantryItem.Note.ShouldBe(expectedNote);
        pantryItem.Status.ShouldBe("Consumido");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Should_Reject_Empty_Or_Whitespace_Barcode(string invalidBarcode)
    {
        Should.Throw<ArgumentException>(() => new PantryItem(
            Guid.NewGuid(),
            Guid.NewGuid(),
            invalidBarcode,
            DateTime.Now.AddMonths(3),
            "Consumido",
            null));
    }
}
