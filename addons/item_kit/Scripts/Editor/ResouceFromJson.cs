using Godot;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;

[Tool]
public partial class ResourceFromJson : VBoxContainer {

    protected Button _generateButton;
    protected LineEdit _inputPathField;
    protected LineEdit _outputPathField;
    protected string _fileContent;


    public ResourceFromJson() { }


    public ResourceFromJson(string outputPath) {
        SetOutputDir(outputPath);
    }


    public override void _Ready() {
        this.init();
    } // _Ready


    protected virtual void init() {
        GD.Print($"- {this.GetType()} initialized");

        var inputRow = BuildInputRow(
            ref _inputPathField,
            "Input",
            GetSettingsValue(InputSettingName, InputPath)
        );
        var outputRow = BuildInputRow(
            ref _outputPathField,
            "Output",
            GetSettingsValue(OutputSettingName, InputPath)
        );

        _generateButton = new Button { Text = GenerateBtnLable };
        _generateButton.Pressed += OnGeneratePressed;

        _inputPathField.TextChanged += (text) => {
            SetSettingsValue(InputSettingName, text);
        };
        _outputPathField.TextChanged += (text) => {
            SetSettingsValue(OutputSettingName, text);
        };

        AddChild(inputRow);
        AddChild(outputRow);
        AddChild(_generateButton);
    }


    protected virtual void saveSettings() {
        SetSettingsValue(InputSettingName, _inputPathField.Text);
        SetSettingsValue(OutputSettingName, _outputPathField.Text);
        ProjectSettings.Save();
    } // SaveSettings


    public virtual void GenerateResources(string jsonContent) {
    } // GenerateResources


    public virtual void OnGeneratePressed() {
        this.saveSettings();

        string inputPath = _inputPathField.Text;

        if (!FileAccess.FileExists(inputPath)) {
            _fileContent = string.Empty;
            GD.PrintErr($"- {GetType()}::Missing JSON file: {inputPath}");
            return;
        }

        using (var file = FileAccess.Open(inputPath, FileAccess.ModeFlags.Read)) {
            _fileContent = file.GetAsText();
        }

        GenerateResources(_fileContent);
    } // OnGeneratePressed


    protected virtual HBoxContainer BuildInputRow(
        ref LineEdit targetField,
        string labelText,
        string fieldSettingValue,
        string placeholder = "",
        string tooltip = ""
    ) {
        HBoxContainer row = new HBoxContainer();

        Label label = new Label {
            Text = labelText,
        };
        row.TooltipText = tooltip;

        targetField = new LineEdit {
            PlaceholderText = placeholder,
            Text = fieldSettingValue,
            SizeFlagsHorizontal = SizeFlags.Expand | SizeFlags.Fill
        };

        row.AddChild(label);
        row.AddChild(targetField);

        return row;
    } // BuildInputRow


    protected virtual void saveResource(Resource resource, string path) {
        string dirPath = System.IO.Path.GetDirectoryName(path);
        string globalDirPath = ProjectSettings.GlobalizePath(dirPath);
        // Ensure the directory exists
        using (var dirAccess = DirAccess.Open(globalDirPath)) {
            dirAccess?.MakeDirRecursive(globalDirPath);
        }

        Error result = ResourceSaver.Save(
            resource,
            path,
            ResourceSaver.SaverFlags.Compress | ResourceSaver.SaverFlags.OmitEditorProperties
        );

        if (result != Error.Ok) {
            string errorMsg = $"Failed to save [{resource.GetType().Name}] resource at {path}: {result}";
            if (result == Error.CantOpen){
                errorMsg =$"{errorMsg}\n - Most likey directory permissions issue. Try creating one manually.";
            }

            GD.PushError(errorMsg);
        } else {
            GD.Print($"[{resource.GetType().Name}] created: {path}");
            GD.Print($" - {resource}");
            GD.Print("----");
        }
    } // saveResource


    public virtual void SetOutputDir(string path) {
        if (_outputPathField == null)
            _outputPathField = new LineEdit();

        _outputPathField.Text = path;
    } // SetOutputDir


    public virtual void SetSettingsValue(string keyName, string value) {
        ProjectSettings.SetSetting(keyName, value);
    } // SetSettingsValue


    /*********************************GETTERS*********************************/

    protected virtual string GenerateBtnLable => "Generate";
    protected virtual string InputPath => _inputPathField?.Text ?? "res://data/";
    protected virtual string OutputDir => _outputPathField?.Text ?? "res://resources/";

    protected virtual string InputSettingName => $"itemkit/{GetType().Name}/input_path";
    protected virtual string OutputSettingName => $"itemkit/{GetType().Name}/output_path";

    protected virtual JsonSerializerOptions SerializerOptions {
        get {
            JsonSerializerOptions opts = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            };
            opts.Converters.Add(new ColorSerializer());
            opts.Converters.Add(new Vector2Serializer());
            opts.Converters.Add(new ListToGodotArraySerializer<int>());
            opts.Converters.Add(new ListToGodotArraySerializer<float>());
            opts.Converters.Add(new ListToGodotArraySerializer<string>());

            return opts;
        }
    } // SerializerOptions


    public virtual string GetSettingsValue(string keyName, string defaultValue = "") {
        return ProjectSettings.HasSetting(keyName)
            ? (string)ProjectSettings.GetSetting(keyName)
            : defaultValue;
    }


    public virtual string GetInputSetting() {
        return GetSettingsValue(InputSettingName, InputPath);
    } // GetInputSetting


    public virtual string GetOutputSetting() {
        return GetSettingsValue(OutputSettingName, OutputDir);
    } // GetOutputSetting

} // class
