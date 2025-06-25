using Gamehound.ItemKit.Editor;

using Godot;

namespace Gamehound.ItemKit.Interfaces;


//public interface IResourceCreator<T> where T : Resource {

//    public T CreateResource(
//        string destination = null,
//        ResourceOptions options = null
//    );

//    public string Hook_Preprocess(
//        string path = null,
//        ResourceOptions options = null
//    );

//    public T Hook_LoadResource(
//        string path = null,
//        ResourceOptions options = null
//    );

//    public T Hook_ProcessDuplicate(
//        T existingResource,
//        ResourceOptions options = null
//    );

//    public T Hook_SaveResource(
//        T resource,
//        string path = null,
//        ResourceOptions options = null
//    );


//    public T Hook_Postprocess(
//        T resource,
//        string path = null,
//        ResourceOptions options = null
//    );

//    public string GetFullPath();

//} // IResourceCreator


public interface IResourceCreator {

    public Resource CreateResource(
        string destination = null,
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

    public string GetFullPath();

} // IResourceCreator
