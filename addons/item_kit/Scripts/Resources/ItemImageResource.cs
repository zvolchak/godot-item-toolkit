using System;
using Godot;
using Gamehound.ItemKit.Editor;
using Gamehound.ItemKit.Interfaces;

namespace Gamehound.ItemKit.Resources;


[Tool]
public partial class ItemImageResource :
    ItemResourceBase,
    IImageData {

    [Export] public string ImagePath { get; set; } = string.Empty;

    [Export] public Texture2D TextureAsset { get; set; }


    public override Resource Hook_SaveResource(
        Resource resource,
        string path = null,
        ResourceOptions options = null
    ) {
        Variant spritesDir;
        if (options == null || (options?.other?.ContainsKey("imgs_dir_path") ?? false)) {
            spritesDir = ProjectSettings
                .GetSetting($"itemkit/{GetType().Name}/imgs_dir_path")
                .AsString();
        } else {
            options.other.TryGetValue("imgs_dir_path", out spritesDir);
        }

        (resource as ItemImageResource).TextureAsset = getTextureFromPath(
            spritesDir.AsString(),
            ImagePath
        );

        resource = base.Hook_SaveResource(resource, path: path, options: options);
        return resource;
    } // Hook_Postprocess


    protected Texture2D getTextureFromPath(string rootDir, string imagePath) {
        Texture2D texture = null;
        string path = $"{rootDir}/{imagePath}".TrimStart('/');
        if (!path.StartsWith("res://")) {
            path = $"res://{path}";
        }

        texture = ResourceLoader.Load<Texture2D>(path);
        if (texture == null) {
            GD.PushWarning($"{GetType().Name}:: Failed to load texture from path: {path}");
        }

        return texture;
    } // getTextureFromPath


    public override bool Equals(object obj) {
        if (obj is not ItemImageResource other)
            return false;

        // Don't check for ID since it is technically irrelevant for two
        // resources considered the same. User might have used different IDs
        // for the same content by mistake or unknowingly.
        return Category == other.Category
            && (TextureAsset == other.TextureAsset
                || Category == other.Category);
    } // Equals


    public override int GetHashCode() {
        return HashCode.Combine(ID, Category, ImagePath, TextureAsset);
    } // GetHashCode

} // class
