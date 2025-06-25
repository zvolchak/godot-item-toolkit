using Gamehound.ItemKit.Interfaces;
using Godot;
using Godot.Collections;

namespace Gamehound.ItemKit.Resources;


public partial class WeaponResource :
    ItemResourceBase,
    IWeaponData,
    IEquippableData {

    // -- IWeaponData -- //

    [Export] public Vector2 DamageAmount { get; set; } = Vector2.One;
    [Export] public float AttackSpeed { get; set; } = 1.0f;

    /// <summary>
    /// Less or equal to 0 could mean - unlimited distance.
    /// </summary>
    [Export] public float Range { get; set; } = 1.0f;

    /// <summary>
    /// "Melee", "Ranged", "TwoHanded", "Finesse", etc.
    /// </summary>
    [Export] public string WeaponClass { get; set; } = string.Empty;

    /// <summary>
    /// Physical, Magic, Arcane, etc.
    /// </summary>
    [Export] public Array<string> DamageTypes { get; set; } = new();

    /// <summary>
    /// Slash, Stab, Spin, etc.
    /// </summary>
    [Export] public Array<string> DamageStyles { get; set; } = new();

    /// <summary>
    /// e.g. TwoHanded, OneHanded, OffHand, Fist, etc
    /// </summary>
    [Export] public Array<string> HoldingStyles { get; set; } = new();


    // -- IEquippableData -- //

    [Export] public Array<string> CompatibleSlots { get; set; } = new();

    [Export] public int SlotPriority { get; set; } = 1;


    // -- Other -- //

    [Export] public ItemShapeResource InventoryShape { get; set; }

    [Export] public RarityResource Rarity { get; set; }

    [Export] public Array<PropertyModifierResource> StatRequirements { get; set; }

    [Export] public Array<ItemImageResource> Images { get; set; }

} // WeaponResource
