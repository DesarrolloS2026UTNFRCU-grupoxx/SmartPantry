using Shouldly;
using SmartPantry.PantryItems;
using System;
using System.Linq;
using Xunit;

namespace SmartPantryApp.Entities;

public class PantryItem_Tests
{
    [Fact]
    public void Update_con_datos_validos_normaliza_los_textos()
    {
        var status = Enum.GetNames(typeof(ItemStatus)).First();
        var item = new PantryItem(
            Guid.NewGuid(), Guid.NewGuid(), "12345678",
            new DateTime(2026, 12, 1), status, null);

        item.Update("  87654321  ", new DateTime(2026, 12, 15),
            "  " + status.ToUpper() + "  ", "  sin gluten  ");

        item.Barcode.ShouldBe("87654321");
        item.Status.ShouldBe(status);
        item.Note.ShouldBe("sin gluten");
        item.Expiration.ShouldBe(new DateTime(2026, 12, 15));
    }
    [Fact]
    public void Update_con_datos_invalidos_no_cambia_nada()
    {
        var status = Enum.GetNames(typeof(ItemStatus)).First();
        var item = new PantryItem(
            Guid.NewGuid(), Guid.NewGuid(), "12345678",
            new DateTime(2026, 12, 1), status, "nota original");

        Should.Throw<ArgumentException>(() =>
            item.Update("87654321", new DateTime(2026, 12, 15),
                status, new string('x', PantryItem.MaxNoteLength + 1)));

        item.Barcode.ShouldBe("12345678");
        item.Expiration.ShouldBe(new DateTime(2026, 12, 1));
        item.Status.ShouldBe(status);
        item.Note.ShouldBe("nota original");
    }
}

