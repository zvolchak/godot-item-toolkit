using System.Collections.Generic;
using System.Text.Json;

using Gamehound.ItemKit.Resources;

namespace Gamehound.ItemKit.Editor;


public partial class AttackTypeResourceGenerator : ResourceFromJson {

    public AttackTypeResourceGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonAttackTypeData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonAttackTypeData item in data) {
            AttackTypeResource value = item.AttackType;
            value.CreateResource(
                path: OutputDir,
                options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                }
            );
        }
    } // GenerateResources


    public override string GenerateBtnLable => "Generate Attack Types";

} // class

