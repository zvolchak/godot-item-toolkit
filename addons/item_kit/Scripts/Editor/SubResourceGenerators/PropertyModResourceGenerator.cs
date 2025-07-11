using Gamehound.ItemKit.Resources;
using Godot;
using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


public partial class PropertyModResourceGenerator : ResourceFromJson {

    public PropertyModResourceGenerator() : base() { }
    public PropertyModResourceGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonPropertymodData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonPropertymodData modData in data) {
            foreach (PropertyModifierResource mod in modData.Modifiers) {
                mod.CreateResource(
                    path: OutputDir,
                    isOverwrite: GetSettingsValue(IsOverwriteSettingPath).AsBool()
                );
            } // foreach mod
        } // foreach data
    } // GenerateResources


    /****************************** GETTERS ***********************************/

    public override string GenerateBtnLable => "Generate Modifiers";

    /// <summary>
    /// Matching setting name with PropertyModifierResource classname so that it can be accessed
    /// later by the PropertyModifierResource instance by its GetOutputDir() method.
    /// </summary>
    public override string OutputSettingPath => $"itemkit/PropertyModifierResource/output_path";

} // class
