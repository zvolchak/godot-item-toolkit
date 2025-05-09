using Gamehound.ItemKit.Resources;

using Godot;
using Godot.Collections;

using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


[Tool]
public partial class TexturesResourceGenerator : ResourceFromJson {


    protected LineEdit _inputImgDirField;


    protected override void init() {
        base.init();

        string tooltip = """
            Path to the root directory where the images are stored. A res://
            prefix will be added to the path if it is not already present.
        """;
        var imgDirRow = BuildInputRow(
            ref _inputImgDirField,
            "Images Directory",
            GetImagesDirSetting(),
            tooltip: tooltip
        );

        _inputImgDirField.TextChanged += (text) => {
            ProjectSettings.SetSetting(ImgsDirSettingName, text);
        };

        AddChild(imgDirRow);
        MoveChild(imgDirRow, 0);
    } // init


    protected override void saveSettings() {
        base.saveSettings();

        SetSettingsValue(ImgsDirSettingName, ImagesPath);
        ProjectSettings.Save();
    } // SaveSettings


    public override void GenerateResources(string jsonContent) {
        base.GenerateResources(jsonContent);

        var data = JsonSerializer.Deserialize<List<JsonItemTextureData>>(
            jsonContent, SerializerOptions
        );

        foreach (JsonItemTextureData textureData in data) {
            foreach (ItemImageResource texture in textureData.Images) {
                string fileName = $"{texture.ID.Replace(" ", "_").ToLower()}";

                if (texture.Category != string.Empty) {
                    fileName = $"{fileName}_{texture.Category
                            .Replace(" ", "_").ToLower()}";
                }

                fileName = $"{fileName}_texture.tres";
                string path = $"{OutputDir.TrimEnd('/')}/{fileName}";
                ItemImageResource textureResource = texture.CreateResource(
                    path,
                    new Godot.Collections.Dictionary<string, Variant> {
                        { "ImagesDir", _inputImgDirField.Text }
                    }
                );

                saveResource(textureResource, path);
            } // for images
        } // for data
    } // GenerateResources


    /********************************* GETTERS *********************************/

    protected override string GenerateBtnLable => "Generate Textures";

    protected virtual string ImagesPath => "res://sprites/";

    protected virtual string ImgsDirSettingName => $"itemkit/{GetType().Name}/imgs_dir_path";


    public virtual string GetImagesDirSetting() {
        string text = ProjectSettings.HasSetting(ImgsDirSettingName)
            ? (string)ProjectSettings.GetSetting(ImgsDirSettingName)
            : "";
        return text;
    } // GetImagesDirSetting

} // TexturesResourceGenerator
