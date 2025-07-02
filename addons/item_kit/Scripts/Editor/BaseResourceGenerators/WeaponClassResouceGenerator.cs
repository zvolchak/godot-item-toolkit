using System.Collections.Generic;
using System.Text.Json;

using Gamehound.ItemKit.Resources;

namespace Gamehound.ItemKit.Editor;


public partial class WeaponClassResouceGenerator : ResourceFromJson {


    public WeaponClassResouceGenerator() : base() { }
    public WeaponClassResouceGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonWeaponClassData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonWeaponClassData item in data) {
            WeaponClassResource value = item.WeaponClass;
            value.CreateResource(
                path: OutputDir,
                options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                }
            );
        }
    } // GenerateResources


    public override string GenerateBtnLable => "Generate Weapon Class";

} // class

