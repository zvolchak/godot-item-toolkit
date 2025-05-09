using Gamehound.ItemKit.Editor;

using Godot;

namespace Gamehound.ItemKit.Interfaces;


public interface IResourceCreator<T> where T : Resource {

    public T CreateResource(
        string destination = null,
        ResourceOptions options = null
    );

    public string Hook_Preprocess(
        string path = null,
        ResourceOptions options = null
    );

    public T Hook_LoadResource(
        string path = null,
        ResourceOptions options = null
    );

    public T Hook_ProcessDuplicate(
        T existingResource,
        ResourceOptions options = null
    );

    public T Hook_SaveResource(
        string path = null,
        ResourceOptions options = null
    );

    public string GetFullPath();

} // IResourceCreator
