using Godot;
using Gamehound.ItemKit.Interfaces;
using Gamehound.ItemKit.Editor;
using static Godot.ResourceSaver;

namespace Gamehound.ItemKit.Resources;


public partial class ItemResourceBase<T> :
    Resource,
    IIdentifier,
    IResourceCreator<T> where T : ItemResourceBase<T>, new() {

    [Export] public string ID { get; set; }
    [Export] public virtual string Name { get; set; }
    [Export] public virtual string Category { get; set; }
    [Export(PropertyHint.MultilineText)] public virtual string Description { get; set; }


    /// <summary>
    /// Create a resource from the current instance. It will first try to load the
    /// resource from the disk or create a new one from the current instance.
    /// </summary>
    public virtual T CreateResource(
        string path = null,
        ResourceOptions options = null
    ) {
        path = Hook_Preprocess(path: path, options: options);
        T existingResource = Hook_LoadResource(path: path, options: options);
        existingResource = Hook_ProcessDuplicate(existingResource, options: options);
        if (existingResource != null)
            return existingResource;

        Hook_SaveResource(path: path, options: options);
        return this as T;
    } // CreateResource


    /// <summary>
    /// Prepare the environment before loading or saving the resource. In the
    /// base method, it makes sure the path is valid and the directory exists.
    /// If directory does not exist, it will try to create it.
    ///
    /// Returns the full path to the resource that will be loaded or saved.
    /// </summary>
    public virtual string Hook_Preprocess(
        string path = null,
        ResourceOptions options = null
    ) {
        if (path == "" || path == null)
            path = GetFullPath();

        string dirPath = System.IO.Path.GetDirectoryName(path);
        string globalDirPath = ProjectSettings.GlobalizePath(dirPath);
        // Ensure the directory exists
        using (var dirAccess = DirAccess.Open(globalDirPath)) {
            dirAccess?.MakeDirRecursive(globalDirPath);
        }

        return path;
    } // Hook_Prepare


    /// <summary>
    /// Save this resource to the disk into the GetFullPath() location, unless
    /// "path" parameter is set to a different value.
    /// </summary>
    public virtual T Hook_SaveResource(
        string path = null,
        ResourceOptions options = null
    ) {
        if (path == "" || path == null)
            path = GetFullPath();

        Error result = ResourceSaver.Save(
            this,
            path,
            SaverFlags.Compress | SaverFlags.OmitEditorProperties
        );

        if (result != Error.Ok) {
            string errorMsg = $"Failed to save [{GetType().Name}] resource " +
                            $"at {path}: {result}";

            if (result == Error.CantOpen) {
                errorMsg = $"{errorMsg}\n - Most likey directory permissions " +
                            "issue. Try creating one manually.";
            }

            GD.PushError(errorMsg);
        } else {
            GD.Print($"[{GetType().Name}] created: {path}");
            GD.Print($" - {this}");
            GD.Print("----");
        }

        return this as T;
    } // Hook_SaveResource


    /// <summary>
    /// Load a resource from either GetFullPath() location or from the "path"
    /// parameter if it is provided. If the resource does not exist, it will
    /// return null.
    /// </summary>
    public virtual T Hook_LoadResource(
        string path = null,
        ResourceOptions options = null
    ) {
        if (path == "" || path == null)
            path = GetFullPath();

        string globalPath = ProjectSettings.GlobalizePath(path);
        if (!ResourceLoader.Exists(globalPath)) {
            return null;
        }

        return ResourceLoader.Load<T>(globalPath);
    } // LoadExistingResource


    /// <summary>
    /// This method tries to figure out what to do with This resource if the
    /// passed "existing" resource is not null. Will return existing one,
    /// unless options.IsOverwrite is set to true.
    ///
    /// Returning null means no existing resource needs to be used as reference.
    /// Instead, this current resource is modified and will need to be saved.
    ///
    /// TODO: Make overwriting process smarter. Aka, it sohuld look into the
    /// JSON data and try to figure out which fields are present in the JSON and
    /// overwrite only those.
    /// </summary>
    public virtual T Hook_ProcessDuplicate(
        T existing,
        ResourceOptions options = null
    ) {
        if (existing == null)
            return null;

        string path = existing.GetFullPath();
        if (existing != null && !(options?.IsOverwrite ?? false)) {
            GD.PushWarning(
                $"[{GetType().Name}::{existing.ID}] exists at " +
                $"{path}. No overwrite is set. Returning existing as is."
            );
            return existing;
        } else if (existing != null && (options?.IsOverwrite ?? false)) {
            GD.PushWarning(
                $"""
                [{GetType().Name}::{existing.ID}] already exists: {path}.
                Overwriting existing resource.
                """
            );
            this.CopyFrom(existing);
        }

        // null means no existing resource needs to be used as reference. Instead,
        // this current resource is modified and will need to be saved.
        return null;
    } // Hook_ProcessDuplicate


    /// <summary>
    /// Copy the properties from the source resource to this resource.
    /// </summary>
    public virtual T CopyFrom(T source) {
        if (source == null) {
            GD.PushError("CopyFrom failed: Source is null.");
            return (T)this;
        }

        foreach (var property in typeof(T).GetProperties()) {
            if (property.CanRead && property.CanWrite) {
                property.SetValue(this, property.GetValue(source));
            }
        } // foreach

        return (T)this;
    } // CopyFrom


    public override string ToString() {
        string result = string.Empty;
        foreach (var property in GetType().GetProperties()) {
            if (property != null && property.CanRead && property.CanWrite) {
                result += $"{property.Name}: {property.GetValue(this)}, ";
            }
        }

        return $"{GetType().Name}: [{result}]";
    } // ToString


    /*************************** GETTERS **************************************/

    /// <summary>
    /// Returns a filename for the resource that will be saved into GetOutputDir()
    /// directory.
    ///
    /// Override this method for each resource type to set a custom filename.
    /// </summary>
    public virtual string GetResourceFilename() {
        return $"{ID.ToLower()}.tres";
    } // ResourceFilename


    /// <summary>
    /// Directory path where the resources will be saved. It will first try to
    /// get the path from the ProjectSettings $"itemkit/{GetType().Name}/output_path",
    /// or fallback to "res://resources/".
    ///
    /// Override this method for eahc resource type to set a custom path.
    /// </summary>
    public virtual string GetOutputDir() {
        string dirPath = ProjectSettings.GetSetting(
            $"itemkit/{GetType().Name}/output_path"
        ).AsString();
        return dirPath != null ? dirPath.ToString() : "res://resources/";
    } // GetOutputDir


    public virtual string GetFullPath() {
        string path = GetOutputDir().TrimEnd('/');
        string filename = GetResourceFilename().TrimStart('/');
        return $"{path}/{filename}";
    } // GetFullPath

} // class
