using System.Collections.Generic;
using System.Text.Json;

using Gamehound.ItemKit.Resources;

namespace Gamehound.ItemKit.Editor;


public partial class DamageTypeResourceGenerators : ResourceFromJson {

    public DamageTypeResourceGenerators(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonDamageTypeData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonDamageTypeData item in data) {
            DamageTypeResource value = item.DamageType;
            value.CreateResource(
                path: OutputDir,
                options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                }
            );
        }
    } // GenerateResources


    public override string GenerateBtnLable => "Generate Damage Types";

} // class

