using Godot;
using System.Text.Json;
using System.Collections.Generic;
using Gamehound.ItemKit.Resources;

namespace Gamehound.ItemKit.Editor;

public partial class ArmorResourcesGenerator : VBoxContainer {
    private Button _generateButton;
    private LineEdit _inputPathField;
    private LineEdit _outputPathField;

    //public override void _Ready() {
    //    GD.Print("- ArmorResourcesGenerator initialized");
    //    _inputPathField = new LineEdit { PlaceholderText = "Input JSON Path" };
    //    _inputPathField.Text = "res://data/armor.json";

    //    _outputPathField = new LineEdit { PlaceholderText = "Output Resource Folder" };
    //    _outputPathField.Text = "res://resources/armor/";

    //    _generateButton = new Button { Text = "Generate Armor" };
    //    _generateButton.Pressed += OnGeneratePressed;

    //    AddChild(_inputPathField);
    //    AddChild(_outputPathField);
    //    AddChild(_generateButton);
    //} // _Ready


    //private void OnGeneratePressed() {
    //    string inputPath = _inputPathField.Text;
    //    string outputDir = _outputPathField.Text;

    //    if (!FileAccess.FileExists(inputPath)) {
    //        GD.PrintErr($"Missing JSON file: {inputPath}");
    //        return;
    //    }

    //    string jsonText;
    //    using (var file = FileAccess.Open(inputPath, FileAccess.ModeFlags.Read)) {
    //        jsonText = file.GetAsText();
    //    }

    //    var data = JsonSerializer.Deserialize<List<ArmorData>>(jsonText);

    //    DirAccess.MakeDirAbsolute(ProjectSettings.GlobalizePath(outputDir));

    //    foreach (var entry in data) {
    //        var res = new ArmorResource {
    //            ID = entry.ID,
    //            ItemName = entry.ItemName,
    //            Description = entry.Description,
    //            RarityWeight = entry.RarityWeight,
    //            Power = entry.Power,
    //            RequiredLevel = entry.RequiredLevel,
    //            CategoryName = entry.CategoryName,
    //            ArmorSlot = entry.ArmorSlot,
    //            Defense = entry.Defense,
    //            Weight = entry.Weight,
    //            Modifiers = new Godot.Collections.Array<PropertyModifierResource>(),
    //            Textures = new Godot.Collections.Array<ItemIconEntryResource>()
    //        };

    //        // Modifiers
    //        if (entry.Modifiers != null) {
    //            foreach (var mod in entry.Modifiers) {
    //                if ((mod.FlatMin != 0 || mod.FlatMax != 0) && (mod.PercentMin != 0 || mod.PercentMax != 0)) {
    //                    GD.PrintErr($"Modifier for {entry.ID}:{mod.TargetProperty} defines both Flat and Percent. Skipping.");
    //                    continue;
    //                }

    //                var modifier = new PropertyModifierResource {
    //                    TargetProperty = mod.TargetProperty,
    //                    FlatValue = new Vector2(mod.FlatMin, mod.FlatMax),
    //                    PercentValue = new Vector2(mod.PercentMin, mod.PercentMax),
    //                    RarityWeight = mod.RarityWeight
    //                };

    //                res.Modifiers.Add(modifier);
    //            }
    //        }

    //        // Textures
    //        if (entry.Textures != null) {
    //            foreach (var tex in entry.Textures) {
    //                var path = $"res://assets/textures/{tex.Path}";
    //                var texture = GD.Load<Texture2D>(path);
    //                if (texture != null) {
    //                    res.Textures.Add(new ItemIconEntryResource {
    //                        Context = tex.Context,
    //                        TextureAsset = texture
    //                    });
    //                }
    //                else {
    //                    GD.PrintErr($"Texture not found: {path} for ID: {entry.ID}");
    //                }
    //            }
    //        }

    //        var outPath = $"{outputDir.TrimEnd('/')}/{entry.ID.ToLower()}.tres";
    //        ResourceSaver.Save(res, outPath);
    //        GD.Print($"[ArmorResource] Saved: {res.ID} → {outPath}");
    //    }

    //    GD.Print("Armor generation complete.");
    //} // OnGeneratePressed


    //private class ArmorData {
    //    public string ID { get; set; }
    //    public string ItemName { get; set; }
    //    public string Description { get; set; }
    //    public int RarityWeight { get; set; }
    //    public int Power { get; set; }
    //    public int RequiredLevel { get; set; }
    //    public string CategoryName { get; set; }
    //    public string ArmorSlot { get; set; }
    //    public int Defense { get; set; }
    //    public int Weight { get; set; }
    //    public List<ModifierData> Modifiers { get; set; }
    //    public List<TextureEntry> Textures { get; set; }
    //} // ArmorData


    //private class ModifierData {
    //    public string TargetProperty { get; set; }
    //    public float FlatMin { get; set; }
    //    public float FlatMax { get; set; }
    //    public float PercentMin { get; set; }
    //    public float PercentMax { get; set; }
    //    public int RarityWeight { get; set; }
    //} // ModifierData


    //private class TextureEntry {
    //    public string Context { get; set; }
    //    public string Path { get; set; }
    //} // TextureEntry
} // ArmorResourcesGenerator
