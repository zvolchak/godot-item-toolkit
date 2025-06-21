using Godot;
using Gamehound.ItemKit.Resources;
using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;

//[GlobalClass]
public partial class RarityResourceGenerator: ResourceFromJson {

    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonRarityData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonRarityData rarityData in data) {
            RarityResource rarity = rarityData.Rarity;
            rarity.CreateResource();
        }
    } // GenerateResources


    protected override string GenerateBtnLable => "Generate Rarities";

    // Matching setting name with ItemShapeResource classname so that it con
    // be accessed later by the ItemShapeResource instance by its GetOutputDir()
    // method.
    protected override string OutputSettingName => $"itemkit/RarityResource/output_path";

} // class
