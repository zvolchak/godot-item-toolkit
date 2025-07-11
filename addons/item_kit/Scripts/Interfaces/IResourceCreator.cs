using Gamehound.ItemKit.Editor;

using Godot;

namespace Gamehound.ItemKit.Interfaces;


public interface IResourceCreator {

    public Resource CreateResource(
        string destination = null,
        string settingName = null,
        bool isOverwrite = false,
        ResourceOptions options = null
    );


    public string Hook_Preprocess(
        string path = null,
        ResourceOptions options = null
    );


    public Resource Hook_LoadResource(
        string path = null,
        ResourceOptions options = null
    );


    public Resource Hook_ProcessDuplicate(
        Resource existingResource,
        bool isOverwrite = false,
        ResourceOptions options = null
    );


    public Resource Hook_SaveResource(
        Resource resource,
        string path = null,
        ResourceOptions options = null
    );


    public Resource Hook_Postprocess(
        Resource resource,
        string path = null,
        ResourceOptions options = null
    );


    public string GetFullPath(string settingName = null);

} // IResourceCreator
