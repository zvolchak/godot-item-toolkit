#pragma warning disable CA2255

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Text.Json;

using Godot;

namespace Gamehound.ItemKit.Editor;

/* This is needed to cleanup Json cache that is used by generator scripts.
 * Otherwise, Godot will error with ".NET: Failed to unload assemblies.".
 */
public static class JsonCacheCleaner {
    private static MethodInfo _clearCacheMethod;


    [ModuleInitializer]
    public static void Init() {
        GD.Print("JsonCacheCleaner initializing...");
        Clear();
    } // Init


    private static void OnUnloading(AssemblyLoadContext _) {
        Clear();
    } // OnUnloading


    public static void Clear() {
        AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly())!.Unloading += alc =>
        {
            var assembly = typeof(JsonSerializerOptions).Assembly;
            var updateHandlerType = assembly.GetType("System.Text.Json.JsonSerializerOptionsUpdateHandler");
            var clearCacheMethod = updateHandlerType?.GetMethod("ClearCache", BindingFlags.Static | BindingFlags.Public);
            clearCacheMethod?.Invoke(null, new object?[] { null });
        };
    }

} // JsonCacheCleaner

#pragma warning restore CA2255
