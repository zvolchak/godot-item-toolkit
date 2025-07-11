using Gamehound.ItemKit.Resources;
using Godot;
using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


public partial class TexturesResourceGenerator : ResourceFromJson {


    protected LineEdit _inputImgDirField;


    public TexturesResourceGenerator() : base() { }
    public TexturesResourceGenerator(
        string inputPath,
        string outputPath,
        string settingName = null
    ) : base(inputPath, outputPath, settingName) { }


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

        foreach (JsonItemTextureData imgJson in data) {
            foreach (ItemImageResource img in imgJson.Images) {
                var opts = new Godot.Collections.Dictionary<string, Variant>();
                opts.Add("imgs_dir_path", GetSettingsValue(IsOverwriteSettingPath).AsString());

                img.CreateResource(
                    path: OutputDir,
                    isOverwrite: GetSettingsValue(IsOverwriteSettingPath).AsBool(),
                    options: new ResourceOptions {
                        other = opts
                    }
                );
            } // for images
        } // for data
    } // GenerateResources


    /********************************* GETTERS *********************************/

    public override string GenerateBtnLable => "Generate Textures";

    protected virtual string ImagesPath => "res://sprites/";

    protected virtual string ImgsDirSettingName {
        get {
            // The `ItemImageResource` name should match what is set in item_kit.cs for the
            // TexturesResourceGenerator instance.
            return $"itemkit/ItemImageResource/imgs_dir_path";
        }
    }


    /// <summary>
    /// Location of the sprits/texture that will be used as base path to set textures for a
    /// resource.
    /// </summary>
    public virtual string GetImagesDirSetting() {
        string text = ProjectSettings.HasSetting(ImgsDirSettingName)
            ? (string)ProjectSettings.GetSetting(ImgsDirSettingName)
            : "";
        return text;
    } // GetImagesDirSetting

} // TexturesResourceGenerator
