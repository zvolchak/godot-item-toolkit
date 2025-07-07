using System.Collections.Generic;
using System.Text.Json;

using Gamehound.ItemKit.Resources;

namespace Gamehound.ItemKit.Editor;


public partial class HoldingTypeResourceGenerator : ResourceFromJson {

    public HoldingTypeResourceGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonHoldingTypeData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonHoldingTypeData item in data) {
            HoldingTypeResource value = item.HoldingType;
            value.CreateResource(
                path: OutputDir,
                options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                }
            );
        }
    } // GenerateResources


    public override string GenerateBtnLable => "Generate Holding Types";

} // class

