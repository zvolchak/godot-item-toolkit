using Gamehound.ItemKit.Editor;
using Gamehound.ItemKit.Interfaces;
using Gamehound.ItemKit.Utils;

using Godot;
using Godot.Collections;

namespace Gamehound.ItemKit.Resources;


[GlobalClass]
[ScriptPath("res://addons/item_kit/Scripts/Resources/PropertyModifierResource.cs")]
public partial class PropertyModifierResource :
    ItemResourceBase<PropertyModifierResource>,
    IRarityData,
    IPropertyModifier {

    // "Attack Speed", "Damage", "Resist", etc
    [Export] public string TargetProperty { get; set; } = "";

    // A range within which a value could be randomized for a Flat bonus.
    [Export] public Vector2 FlatValue { get; set; } = Vector2.Zero;

    // A range within which a percentage value could be randomized for a Percent bonus.
    [Export] public Vector2 PercentValue { get; set; } = Vector2.Zero;

    // Randomizer factor. Or anything else that makes sense in a context of a game.
    [Export] public int Weight { get; set; } = 1;

    [Export] public string Tier { get; set; } = "Normal";

    [Export] public Color RarityColor { get; set; } = Colors.White;

    [Export] public int InstanceLimit { get; set; } = -1;

} // PropertyModifierResource
