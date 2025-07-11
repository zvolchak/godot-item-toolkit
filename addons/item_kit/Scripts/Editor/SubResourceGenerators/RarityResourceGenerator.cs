using Godot;
using Gamehound.ItemKit.Resources;
using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


public partial class RarityResourceGenerator: ResourceFromJson {

    public RarityResourceGenerator() : base() { }
    public RarityResourceGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonRarityData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonRarityData rarityData in data) {
            RarityResource rarity = rarityData.Rarity;
            rarity.CreateResource(
                path: OutputDir,
                isOverwrite: GetSettingsValue(IsOverwriteSettingPath).AsBool()
            );
        }
    } // GenerateResources


    public override string GenerateBtnLable => "Generate Rarities";

} // class
