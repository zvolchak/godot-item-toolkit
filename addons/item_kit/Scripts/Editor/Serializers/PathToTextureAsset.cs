using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gamehound.ItemKit.Utils;
using Godot;

namespace Gamehound.ItemKit.Editor;


public class PathToTextureAsset : JsonConverter<Texture2D> {

    public override Texture2D Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        string texturePath = ItemKitUtils.GetStringFromJson(root, "Path");
        Texture2D textureAsset = ItemKitUtils.LoadTextureFromPath(texturePath);
        return textureAsset;
    } // Read


    public override void Write(Utf8JsonWriter writer, Texture2D value, JsonSerializerOptions options) {
        writer.WriteStartObject();
        string texturePath = value.ResourcePath;

        if (!string.IsNullOrEmpty(texturePath)) {
            writer.WriteString("Path", texturePath);
        }
        else {
            GD.PrintErr($"[{GetType().Name}]:: Texture does not have a valid resource path.");
            writer.WriteString("Path", "");
        }

        writer.WriteEndObject();
    } // Write
} // class