using Gamehound.ItemKit.Resources;
using Godot;
using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


[Tool]
[GlobalClass]
public partial class PropertyModResourceGenerator : ResourceFromJson {

    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonPropertymodData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonPropertymodData modData in data) {
            foreach (PropertyModifierResource mod in modData.Modifiers) {
                string fileName = $"{mod.ID.ToLower()}_modifier.tres";
                string path = $"{OutputDir.TrimEnd('/')}/{fileName}";
                PropertyModifierResource modifierResource = mod.CreateResource(path);
                saveResource(modifierResource, path);
            } // foreach mod
        } // foreach data
    } // GenerateResources


    /****************************** GETTERS ***********************************/

    protected override string GenerateBtnLable => "Generate Modifiers";

} // class
