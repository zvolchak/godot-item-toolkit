using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gamehound.ItemKit.Utils;
using Godot;

namespace Gamehound.ItemKit.Editor;


public class Vector2Serializer : JsonConverter<Vector2> {

    public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        float X = ItemKitUtils.GetFloatFromJson(root, "x");
        float Y = ItemKitUtils.GetFloatFromJson(root, "y");
        return new Vector2(X, Y);
    } // Read

    public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options) {
        writer.WriteStartObject();
        writer.WriteNumber("x", value.X);
        writer.WriteNumber("y", value.Y);
        writer.WriteEndObject();
    } // Write
} // class