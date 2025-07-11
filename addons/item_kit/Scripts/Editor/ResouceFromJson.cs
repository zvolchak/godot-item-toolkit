using Godot;

using System;
using System.Text.Json;

namespace Gamehound.ItemKit.Editor;


public partial class ResourceFromJson : VBoxContainer {

    protected Button _generateButton;
    protected string _generateBtnTxt = "Generate";
    protected LineEdit _inputPathField;
    protected LineEdit _outputPathField;
    protected string _fileContent;
    protected string _settingName;


    public ResourceFromJson() { }


    public ResourceFromJson(string inputPath, string outputPath, string settingName = null, string generateBtnText = null) {
        if (settingName == null || settingName == "") {
            _settingName = GetType().Name;
        } else {
            _settingName = settingName;
        }

        SetSettingsValue(InputSettingPath, inputPath);
        SetSettingsValue(OutputSettingPath, outputPath);

        if (generateBtnText != null) {
            _generateBtnTxt = generateBtnText;
        }
    }


    public override void _Ready() {
        this.init();
    } // _Ready


    protected virtual void init() {
        GD.Print($"- {this.GetType()} initialized");

        var inputRow = BuildInputRow(
            ref _inputPathField,
            "Input",
            GetSettingsValue(InputSettingPath, InputPath).AsString()
        );
        var outputRow = BuildInputRow(
            ref _outputPathField,
            "Output",
            GetSettingsValue(OutputSettingPath, InputPath).AsString()
        );

        var isOverwriteCheckbox = BuildCheckboxRow(
            "Is Overwrite",
            GetSettingsValue(IsOverwriteSettingPath, false).As<bool>(),
            (isChecked) => {
                SetSettingsValue(IsOverwriteSettingPath, isChecked);
            }
        );

        _generateButton = new Button { Text = GenerateBtnLable };
        _generateButton.Pressed += OnGeneratePressed;

        _inputPathField.TextChanged += (text) => {
            SetSettingsValue(InputSettingPath, text);
        };
        _outputPathField.TextChanged += (text) => {
            SetSettingsValue(OutputSettingPath, text);
        };

        AddChild(inputRow);
        AddChild(outputRow);
        AddChild(isOverwriteCheckbox);
        AddChild(_generateButton);
    }


    protected virtual void saveSettings() {
        SetSettingsValue(InputSettingPath, _inputPathField.Text);
        SetSettingsValue(OutputSettingPath, _outputPathField.Text);
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


    protected virtual HBoxContainer BuildCheckboxRow(
        string labelText,
        bool initialState,
        Action<bool> onToggled
    ) {
        // Create a horizontal container for the checkbox and label
        HBoxContainer row = new HBoxContainer();

        // Create the checkbox
        CheckBox checkBox = new CheckBox {
            ButtonPressed = initialState
        };

        // Connect the toggled signal to the provided action
        checkBox.Toggled += (pressed) => onToggled(pressed);

        // Create the label
        Label label = new Label {
            Text = labelText
        };

        // Add the checkbox and label to the row
        row.AddChild(checkBox);
        row.AddChild(label);

        return row;
    } // BuildCheckboxRow


    public virtual void SetOutputDir(string path) {
        if (_outputPathField == null)
            _outputPathField = new LineEdit();

        _outputPathField.Text = path;
    } // SetOutputDir


    public virtual void SetSettingsValue(string keyName, Variant value) {
        ProjectSettings.SetSetting(keyName, value);
    } // SetSettingsValue


    /*********************************GETTERS*********************************/

    public virtual string GenerateBtnLable {
        get {
            return _generateBtnTxt;
        }
    }

    public virtual string InputPath => _inputPathField?.Text ?? "res://data/";
    public virtual string OutputDir => _outputPathField?.Text ?? "res://resources/";

    public virtual string InputSettingPath => $"itemkit/{SettingName}/input_path";
    public virtual string OutputSettingPath => $"itemkit/{SettingName}/output_path";
    public virtual string IsOverwriteSettingPath => $"itemkit/{SettingName}/is_overwrite";

    public virtual string SettingName {
        get {
            if (_settingName == "") {
                return GetType().Name;
            }
            return _settingName;
        }
    } // SettingName


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


    public virtual Variant GetSettingsValue(
        string keyName,
        Variant defaultValue = new Variant()
    ) {
        return ProjectSettings.HasSetting(keyName)
            ? ProjectSettings.GetSetting(keyName)
            : defaultValue;
    }

} // class
