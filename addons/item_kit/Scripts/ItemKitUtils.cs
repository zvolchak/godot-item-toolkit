using Gamehound.ItemKit.Interfaces;

using Godot;

using System;
using System.IO;
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


    // public static Resource ValidateResource(Resource res, string path) {
    //     // Ensure the folder exists on a disk (absolute OS path)
    //     var absPath = ProjectSettings.GlobalizePath(path);
    //     var absDir = Path.GetDirectoryName(absPath);
    //     Directory.CreateDirectory(absDir);

    //     // Try loading existing resource to prevent duplicate .tres files.
    //     Resource existing = null;
    //     if (ResourceLoader.Exists(path)) {
    //         existing = GD.Load(path);
    //     }
    //     if (existing == null)
    //         return null;

    //     IIdentifier existingId = existing as IIdentifier;
    //     IIdentifier resId = res as IIdentifier;
    //     // Check the content of a resource for duplication, but ignore the ID,
    //     // since same ID assumes an overwrite.
    //     if (existing != null && existing.Equals(res)) {
    //         GD.PushWarning($"Duplicate {res.GetType().Name}: incoming.ID='{resId.ID}' vs existing.ID='{existingId.ID}'");
    //         return existing;
    //     }

    //     if (existingId?.ID == resId?.ID) {
    //         GD.PushWarning($"{res.GetType().Name}:: Incoming resource ID '{resId.ID}' will overwrite existing resource.");
    //     }

    //     return existing;
    // } // CreateResource


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
