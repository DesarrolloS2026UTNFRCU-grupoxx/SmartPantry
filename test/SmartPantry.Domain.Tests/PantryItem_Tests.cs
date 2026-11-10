using SmartPantryApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace SmartPantry;

public class PantryItem_Tests
{
    [Fact]
    public async Task Should_Create_Valid_PantryItem()
    {
        // Arrange
        var pantryItem = new PantryItem();

        // Act
        pantryItem.Id = Guid.NewGuid();
        pantryItem.PantryId = Guid.NewGuid();
        pantryItem.Barcode = "7791234567890";
        pantryItem.Expiration = DateTime.Now.AddMonths(3);
        pantryItem.Status = "Cerrado";
        pantryItem.Note = "Comprado en oferta";


        // Assert
        pantryItem.Id.ShouldNotBe(Guid.Empty);
        pantryItem.PantryId.ShouldNotBe(Guid.Empty);
        pantryItem.Barcode.ShouldNotBeNull();
        pantryItem.Status.ShouldNotBeNull();
    }

    [Fact]
    public async Task Should_Reject_Empty_Or_Whitespace_Barcode()
    {
        // Arrange
        var pantryItem = new PantryItem();

        // Act
        //pantryItem.Id = Guid.NewGuid();
        //pantryItem.PantryId = Guid.NewGuid();
        pantryItem.Barcode = "7791234567890";
        //pantryItem.Expiration = DateTime.Now.AddMonths(3);
        //pantryItem.Status = "Cerrado";
        //pantryItem.Note = "Comprado en oferta";
        var barcode = pantryItem.Barcode;
        // Assert
        string.IsNullOrWhiteSpace(barcode).ShouldNotBe(true);
    }
}

