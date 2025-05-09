using Gamehound.ItemKit.Resources;
using Godot;
using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


[Tool]
[GlobalClass]
public partial class ItemShapeResourceGenerator : ResourceFromJson {

    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonItemShapeData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonItemShapeData shapeData in data) {
            ItemShapeResource shape = shapeData.InventoryShape;
            string fileName = $"{shape.ID.ToLower()}.tres";
            string path = $"{OutputDir.TrimEnd('/')}/{fileName}";
            shape.CreateResource(path: path);
        }
    } // GenerateResources


    protected override string GenerateBtnLable => "Generate Shapes";

    // Matching setting name with ItemShapeResource classname so that it con
    // be accessed later by the ItemShapeResource instance by its GetOutputDir()
    // method.
    protected override string OutputSettingName => $"itemkit/ItemShapeResource/output_path";

} // class
