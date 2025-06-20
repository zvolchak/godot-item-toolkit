using Gamehound.ItemKit.Resources;
using Godot;
using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


[GlobalClass]
public partial class PropertyModResourceGenerator : ResourceFromJson {

    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonPropertymodData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonPropertymodData modData in data) {
            foreach (PropertyModifierResource mod in modData.Modifiers) {
                GD.Print(mod);
                mod.CreateResource(options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                });
            } // foreach mod
        } // foreach data
    } // GenerateResources


    /****************************** GETTERS ***********************************/

    protected override string GenerateBtnLable => "Generate Modifiers";
    // Matching setting name with PropertyModifierResource classname so that it con
    // be accessed later by the PropertyModifierResource instance by its GetOutputDir()
    // method.
    protected override string OutputSettingName => $"itemkit/PropertyModifierResource/output_path";

} // class
