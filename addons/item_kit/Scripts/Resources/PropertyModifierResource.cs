using Gamehound.ItemKit.Interfaces;

using Godot;

namespace Gamehound.ItemKit.Resources;

[Tool]
public partial class PropertyModifierResource :
    ItemResourceBase,
    IPropertyModifier {

    /// <summary>
    /// "Attack Speed", "Damage", "Resist", etc
    /// </summary>
    [Export] public string TargetProperty { get; set; } = "";

    /// <summary>
    /// A range within which a value could be randomized for a Flat bonus.
    /// </summary>
    public Vector2 FlatValue { get; set; } = Vector2.Zero;

    /// <summary>
    /// A range within which a percentage value could be randomized for a Percent bonus.
    /// </summary>
    [Export] public Vector2 PercentValue { get; set; } = Vector2.Zero;

} // PropertyModifierResource
