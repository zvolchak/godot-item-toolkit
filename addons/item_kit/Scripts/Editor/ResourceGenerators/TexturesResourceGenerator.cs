using Gamehound.ItemKit.Resources;

using Godot;
using Godot.Collections;

using System.Collections.Generic;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


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

        foreach (JsonItemTextureData imgJson in data) {
            foreach (ItemImageResource img in imgJson.Images) {
                var opts = new Godot.Collections.Dictionary<string, Variant>();
                opts.Add("sprites_dir", GetSettingsValue(IsOverwriteSettingName).AsString());

                img.CreateResource(options: new ResourceOptions {
                    IsOverwrite = GetSettingsValue(IsOverwriteSettingName).AsBool(),
                    other = opts
                });
            } // for images
        } // for data
    } // GenerateResources


    /********************************* GETTERS *********************************/

    protected override string GenerateBtnLable => "Generate Textures";

    protected virtual string ImagesPath => "res://sprites/";

    protected virtual string ImgsDirSettingName {
        get {
            return $"itemkit/{GetType().Name}/imgs_dir_path";
        }
    }


    // Matching setting name with ItemShapeResource classname so that it con
    // be accessed later by the ItemShapeResource instance by its GetOutputDir()
    // method.
    protected override string OutputSettingName {
        get {
            return $"itemkit/{GetType().Name}/output_path";
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
