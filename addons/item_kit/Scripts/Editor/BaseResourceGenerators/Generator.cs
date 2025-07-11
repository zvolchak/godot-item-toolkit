using System.Collections.Generic;
using System.Text.Json;

using Gamehound.ItemKit.Resources;

using Godot;

namespace Gamehound.ItemKit.Editor;


public abstract partial class Generator<TJson, TResource> : ResourceFromJson
    where TResource : ItemResourceBase {

    public Generator(
        string inputPath,
        string outputPath,
        string settingName = null,
        string generateBtnText = null
    ) : base(inputPath, outputPath, settingName: settingName, generateBtnText: generateBtnText) {
    }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<TJson>>(jsonContent, SerializerOptions);

        foreach (TJson item in data) {
            TResource resource = ExtractResource(item);
            resource.CreateResource(
                path: OutputDir,
                isOverwrite: GetSettingsValue(IsOverwriteSettingPath).AsBool()
            );
        }
    } // GenerateResources


    protected abstract TResource ExtractResource(TJson item);

} // class
