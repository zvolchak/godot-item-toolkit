#pragma warning disable CA2255

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;

/* This is needed to cleanup Json cache that is used by generator scripts.
 * Otherwise, Godot will error with ".NET: Failed to unload assemblies.".
 */
public static class JsonCacheCleaner {
    private static MethodInfo _clearCacheMethod;


    [ModuleInitializer]
    public static void Init() {
        var asm = typeof(JsonSerializerOptions).Assembly;
        var handlerType = asm.GetType(
            "System.Text.Json.JsonSerializerOptionsUpdateHandler"
        );
        _clearCacheMethod = handlerType?
            .GetMethod("ClearCache", BindingFlags.Static | BindingFlags.Public);

        var ctx = AssemblyLoadContext.GetLoadContext(
            Assembly.GetExecutingAssembly()
        );
        ctx.Unloading += OnUnloading;
    } // Init


    private static void OnUnloading(AssemblyLoadContext _) {
        _clearCacheMethod?.Invoke(null, new object[] { null });
    } // OnUnloading

} // JsonCacheCleaner

#pragma warning restore CA2255