using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Gamehound.ItemKit.Resources;
using Gamehound.ItemKit.Utils;
using Godot;

namespace Gamehound.ItemKit.Editor;


public class ItemShapeSerializer : JsonConverter<ItemShapeResource> {

    public override ItemShapeResource Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    ) {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        float X = ItemKitUtils.GetFloatFromJson(root, "x");
        float Y = ItemKitUtils.GetFloatFromJson(root, "y");
        return new ItemShapeResource();
    } // Read


    public override void Write(
        Utf8JsonWriter writer,
        ItemShapeResource value,
        JsonSerializerOptions options
    ) {
        writer.WriteStartObject();
        writer.WriteNumber("width", value.Width);

        writer.WritePropertyName("layout");
        writer.WriteStartArray();
        foreach (int cell in value.Layout) {
            writer.WriteNumberValue(cell);
        }
        writer.WriteEndArray();

        writer.WriteEndObject();
    } // Write
} // class
