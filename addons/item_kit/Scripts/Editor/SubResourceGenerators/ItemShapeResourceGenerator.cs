using System.Collections.Generic;
using System.Text.Json;
using Gamehound.ItemKit.Resources;

using Godot;


namespace Gamehound.ItemKit.Editor;


public partial class ItemShapeResourceGenerator : ResourceFromJson {


    public ItemShapeResourceGenerator(): base() { }
    public ItemShapeResourceGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonItemShapeData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonItemShapeData shapeData in data) {
            ItemShapeResource shape = shapeData.InventoryShape;
            var s = shape.CreateResource(
                path: OutputDir,
                isOverwrite: GetSettingsValue(IsOverwriteSettingPath).AsBool()
            );
        }
    } // GenerateResources


    public override string GenerateBtnLable => "Generate Shapes";

} // class

