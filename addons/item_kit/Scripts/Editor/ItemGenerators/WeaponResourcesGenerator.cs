using Godot;
using System.Text.Json;
using System.Collections.Generic;
using Gamehound.ItemKit.Resources;
using Godot.Collections;
using System.Linq;

namespace Gamehound.ItemKit.Editor;


public partial class WeaponResourcesGenerator : ResourceFromJson {


    public WeaponResourcesGenerator() : base() { }
    public WeaponResourcesGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        JsonCacheCleaner.Clear();
        var data = JsonSerializer.Deserialize<List<WeaponResource>>(
            jsonContent, SerializerOptions
        );

        foreach (WeaponResource entry in data) {
            string fileName = $"{entry.ID.ToLower()}.tres";
            string path = $"{OutputDir.TrimEnd('/')}/{fileName}";

            var shape = entry.InventoryShape.CreateResource() as ItemShapeResource;
            var rarity = entry.Rarity.CreateResource() as RarityResource;
            var images = new Array<ItemImageResource>(
                entry.Images.Select(img => img.CreateResource() as ItemImageResource)
            );
            var statReq = new Array<PropertyModifierResource>(
                entry.StatRequirements.Select(req => {
                    var res = req?.CreateResource();
                    return res as PropertyModifierResource ?? null;
                })
            );
            //var weaponClass = entry.WeaponClass.CreateResource() as WeaponClassResource;
            var weaponClass = entry.WeaponClass.CreateResource(
                settingName: "WeaponClassResource") as ItemResourceBase;
            var holdingTypes = new Array<ItemResourceBase>(
                entry.HoldingTypes.Select(
                    ht => ht.CreateResource(settingName: "HoldingTypeResource") as ItemResourceBase
                )
            );

            /// Override sub resources with a reference to a previously created resources.
            updateDamageReferences(entry.Damages);
            entry.InventoryShape = shape ?? entry.InventoryShape;
            entry.Rarity = rarity ?? entry.Rarity;
            entry.Images = images ?? entry.Images;
            entry.StatRequirements = statReq ?? entry.StatRequirements;
            entry.WeaponClass = weaponClass ?? entry.WeaponClass;
            entry.HoldingTypes = holdingTypes ?? entry.HoldingTypes;

            entry.CreateResource(
                path,
                isOverwrite: GetSettingsValue(IsOverwriteSettingPath).AsBool()
            );
        } // foreach

        GD.Print($"{data.Count} Weapons generated.");
    } // GenerateResources


    private void updateDamageReferences(Array<DamageData> damages) {
        foreach (var damage in damages) {
            var at = damage.AttackType?.CreateResource(settingName: "AttackTypeResource");
            var dt = damage.DamageType?.CreateResource(settingName: "DamageTypeResource");

            damage.AttackType = at as ItemResourceBase;
            damage.DamageType = dt as ItemResourceBase;
        } // foreach
    } // updateDamageReferences


    public override string GenerateBtnLable { get; } = "Generate Weapon Resource";

} // WeaponResourcesGenerator
