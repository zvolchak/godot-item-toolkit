using Gamehound.ItemKit.Interfaces;
using Godot;
using Godot.Collections;

namespace Gamehound.ItemKit.Resources;


public partial class WeaponResource :
    ItemResourceBase,
    IWeaponData,
    IEquippableData {

    // -- IWeaponData -- //

    /// <summary>
    /// "Melee", "Ranged", "TwoHanded", "Finesse", etc.
    /// </summary>
    [Export] public WeaponClassResource WeaponClass { get; set; }


    [Export] public Array<DamageData> Damages { get; set; }

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
