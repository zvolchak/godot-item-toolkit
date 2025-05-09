using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gamehound.ItemKit.Utils;
using Godot;

namespace Gamehound.ItemKit.Editor;


public class ColorSerializer : JsonConverter<Color> {
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var r = ItemKitUtils.GetFloatFromJson(root, "r");
        var g = ItemKitUtils.GetFloatFromJson(root, "g");
        var b = ItemKitUtils.GetFloatFromJson(root, "b");
        var a = ItemKitUtils.GetFloatFromJson(root, "a", 1);

        return new Color(r, g, b, a);
    } // Read


    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions opts) {
        writer.WriteStartObject();
        writer.WriteNumber("r", value.R);
        writer.WriteNumber("g", value.G);
        writer.WriteNumber("b", value.B);
        writer.WriteNumber("a", value.A);
        writer.WriteEndObject();
    } // Write

} // class
