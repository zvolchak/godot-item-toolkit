using Godot;
using System.Text.Json;
using System.Collections.Generic;
using Gamehound.ItemKit.Resources;

namespace Gamehound.ItemKit.Editor;


public partial class WeaponResourcesGenerator : ResourceFromJson {

    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        JsonCacheCleaner.Clear();
        var data = JsonSerializer.Deserialize<List<WeaponResource>>(
            jsonContent, SerializerOptions
        );

        foreach (WeaponResource entry in data) {
            string fileName = $"{entry.ID.ToLower()}.tres";
            string path = $"{OutputDir.TrimEnd('/')}/{fileName}";
            var shape = entry.InventoryShape.CreateResource();
            // var startRequirement = entry.StatRequirements.CreateResource();

            // Override InventoryShape that was created from the json data
            // with the one that was created from the resource instance that
            // is saved on the disk. Aka reuse previously created shape.
            entry.InventoryShape = shape;
            entry.CreateResource(path);
        } // foreach

        GD.Print($"{data.Count} Weapons generated.");
    } // GenerateResources

    protected override string GenerateBtnLable { get; } = "Generate Weapon Resource";

} // WeaponResourcesGenerator
