using Gamehound.ItemKit.Interfaces;

using Godot;

using System;
using System.Text.Json;

namespace Gamehound.ItemKit.Utils;


public static class ItemKitUtils {

    /// <summary>
    /// Get to float value from the json content. Key name is case insensitive.
    /// </summary>
    public static float GetFloatFromJson(
        JsonElement root,
        string keyName,
        float defaultValue = 0f
    ) {
        foreach (var prop in root.EnumerateObject()) {
            if (prop.Name.Equals(keyName, StringComparison.OrdinalIgnoreCase)
                && prop.Value.TryGetSingle(out var v)) {
                return v;
            }
        }
        return defaultValue;
    } // GetJsonFloat


    /// <summary>
    /// Get to float value from the json content. Key name is case insensitive.
    /// </summary>
    public static string GetStringFromJson(
        JsonElement root,
        string keyName,
        string defaultValue = ""
    ) {
        foreach (var prop in root.EnumerateObject()) {
            if (!prop.Name.Equals(keyName, StringComparison.OrdinalIgnoreCase))
                continue;
            return prop.Value.GetString();
        }
        return defaultValue;
    } // GetStringFromJson


    public static Texture2D LoadTextureFromPath(string path) {
        if (ResourceLoader.Exists(path, "Texture2D")) {
            var texture = ResourceLoader.Load(path, "Texture2D") as Texture2D;
            return texture;
        } else {
            GD.PrintErr($"Texture not found at path: {path}");
            return null;
        }
    } // LoadTextureFromPath

} // class
