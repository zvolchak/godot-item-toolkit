using Gamehound.ItemKit.Resources;
using Godot;
using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


public partial class PropertyModResourceGenerator : ResourceFromJson {

    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonPropertymodData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonPropertymodData modData in data) {
            foreach (PropertyModifierResource mod in modData.Modifiers) {
                mod.CreateResource(options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                });
            } // foreach mod
        } // foreach data
    } // GenerateResources


    /****************************** GETTERS ***********************************/

    protected override string GenerateBtnLable => "Generate Modifiers";

    /// <summary>
    /// Matching setting name with PropertyModifierResource classname so that it can be accessed
    /// later by the PropertyModifierResource instance by its GetOutputDir() method.
    /// </summary>
    protected override string OutputSettingName => $"itemkit/PropertyModifierResource/output_path";

} // class
