using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;

namespace Gamehound.ItemKit.Editor;


public class ListToGodotArraySerializer<[MustBeVariant] T> : JsonConverter<Godot.Collections.Array<T>> {

    public override Godot.Collections.Array<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        var list = JsonSerializer.Deserialize<List<T>>(ref reader, options);
        return new Godot.Collections.Array<T>(list);
    } // Read

    public override void Write(Utf8JsonWriter writer, Godot.Collections.Array<T> value, JsonSerializerOptions options) {
        var list = new List<T>(value);
        JsonSerializer.Serialize(writer, list, options);
    } // Write
} // class
