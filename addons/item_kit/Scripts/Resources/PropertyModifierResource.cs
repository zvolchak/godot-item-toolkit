using Gamehound.ItemKit.Editor;
using Gamehound.ItemKit.Interfaces;
using Gamehound.ItemKit.Utils;

using Godot;
using Godot.Collections;

namespace Gamehound.ItemKit.Resources;


[Tool]
[GlobalClass]
public partial class PropertyModifierResource :
Resource,
IIdentifier,
IRarityData,
IPropertyModifier {

    [Export] public string ID { get; set; } = string.Empty;

    [Export] public string Name { get; set; } = string.Empty;

    [Export] public string Category { get; set; } = string.Empty;

    [Export] public string Description { get; set; } = string.Empty;

    // "Attack Speed", "Damage", "Resist", etc
    [Export] public string TargetProperty { get; set; } = "";

    // A range within which a value could be randomized for a Flat bonus.
    [Export] public Vector2 FlatValue { get; set; } = Vector2.Zero;

    // A range within which a percentage value could be randomized for a Percent bonus.
    [Export] public Vector2 PercentValue { get; set; } = Vector2.Zero;

    [Export] public int Weight { get; set; } = 1;

    [Export] public string Tier { get; set; } = "Normal";

    [Export] public Color RarityColor { get; set; } = Colors.White;

    [Export] public int InstanceLimit { get; set; } = -1;


    public PropertyModifierResource CreateResource(
        string path,
        Dictionary<string, Variant> options = null
    ) {
        ItemKitUtils.ValidateResource(this, path);
        // Overwrite the resource ef it exists (by not doing anything with
        // ValidateResource return value), because why not.

        SetScript(ResourceLoader.Load<Script>(
            "res://addons/item_kit/Scripts/Resources/PropertyModifierResource.cs"
        ));

        return this;
    } // CreateResource


    public override string ToString() {
        return $"[{GetType().Name}]: " +
            $"[ID={ID}, Name={Name}, Category={Category}, " +
            $"Description={Description}, TargetProperty={TargetProperty}, " +
            $"FlatValue={FlatValue}, PercentValue={PercentValue}, " +
            $"Weight={Weight}, Tier={Tier}, RarityColor={RarityColor}, " +
            $"InstanceLimit={InstanceLimit}]";
    } // ToString

} // OffensiveModifierResource
