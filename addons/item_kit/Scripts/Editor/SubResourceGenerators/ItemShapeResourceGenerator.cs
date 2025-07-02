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
                options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                }
            );
        }
    } // GenerateResources


    public override string GenerateBtnLable => "Generate Shapes";

    // Matching setting name with ItemShapeResource classname so that it con
    // be accessed later by the ItemShapeResource instance by its GetOutputDir()
    // method.
    //public override string OutputSettingName => $"itemkit/ItemShapeResource/output_path";

} // class

