using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


public partial class ItemBaseResourceGenerator : ResourceFromJson {


    public ItemBaseResourceGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonItemBaseData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonItemBaseData item in data) {
            var value = item.Item;
            value.CreateResource(
                path: $"{GetSettingsValue(OutputSettingName).AsString()}/{value.GetResourceFilename()}",
                options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                }
            );
        }
    } // GenerateResources

} // class

