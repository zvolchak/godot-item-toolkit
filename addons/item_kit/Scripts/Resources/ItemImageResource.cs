using Gamehound.ItemKit.Interfaces;
using Gamehound.ItemKit.Utils;

using Godot;
using Godot.Collections;

using System;
using System.Text.Json.Serialization;

namespace Gamehound.ItemKit.Resources;


[Tool]
[GlobalClass]
public partial class ItemImageResource :
    Resource,
    IIdentifier,
    IImageData {

    [Export] public string ID { get; set; } = string.Empty;

    // The name of the context in which this icon should be shown for
    // the item. e.g. "Inventory", "Preview", "Equipped" and etc.
    [Export] public virtual string Name { get; set; } = string.Empty;

    [Export] public virtual string Category { get; set; } = string.Empty;

    [Export] public virtual string Description { get; set; } = string.Empty;

    [Export] public string ImagePath { get; set; } = string.Empty;

    [Export] public Texture2D TextureAsset { get; set; }


    public ItemImageResource CreateResource(
        string path,
        Dictionary<string, Variant> options = null
    ) {
        ItemKitUtils.ValidateResource(this, path);
        // Overwrite the resource ef it exists (by not doing anything with
        // ValidateResource return value), because why not.

        string imagesDir = options != null && options.ContainsKey("ImagesDir")
            ? options["ImagesDir"].AsString()
            : string.Empty;

        Texture2D imageTexture = getTextureFromPath(imagesDir, ImagePath);
        ItemImageResource resource = new ItemImageResource {
            ID = ID,
            Name = Name,
            Description = Description,
            ImagePath = ImagePath,
            Category = Category,
            TextureAsset = imageTexture
        };

        resource.SetScript(ResourceLoader.Load<Script>(
            "res://addons/item_kit/Scripts/Resources/ItemImageResource.cs"
        ));
        return resource;
    } // CreateResouceFile


    protected Texture2D getTextureFromPath(string rootDir, string imagePath) {
        Texture2D texture = null;
        string path = $"{rootDir}/{imagePath}".TrimStart('/');
        if (!path.StartsWith("res://")) {
            path = $"res://{path}";
        }
        try {
            texture = ResourceLoader.Load<Texture2D>(path);
            if (texture == null) {
                GD.PushWarning($"{GetType().Name}:: Failed to load texture from path: {path}");
            }
        } catch (Exception e) {
            GD.PushWarning($"{GetType().Name}:: Failed to load texture from path: {path}");
            GD.PushError(e.Message);
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


    public override string ToString() {
        return $"{GetType().Name}:: " +
        $"[ID={ID}, " +
        $"Name={Name}, " +
        $"Category={Category}, " +
        $"ImagePath={ImagePath}, " +
        $"TextureAsset={TextureAsset}]";
    } // ToString
} // class
