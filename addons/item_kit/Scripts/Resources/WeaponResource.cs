using Gamehound.ItemKit.Interfaces;

using Godot;
using Godot.Collections;

namespace Gamehound.ItemKit.Resources;


[Tool]
[GlobalClass]
public partial class WeaponResource :
    ItemResourceBase<WeaponResource>,
    IWeaponData,
    IEquippableData {

    [Export] public Vector2 DamageAmount { get; set; } = Vector2.One;
    [Export] public float AttackSpeed { get; set; } = 1.0f;

    // Less or equal to 0 could mean - unlimited distance.
    [Export] public float Range { get; set; } = 1.0f;

    // "Melee", "Ranged", "TwoHanded", "Finesse", etc.
    [Export] public string WeaponClass { get; set; } = string.Empty;

    // Physical, Magic, Arcane, etc.
    [Export] public Array<string> DamageTypes { get; set; } = new();

    // Slash, Stab, Spin, etc.
    [Export] public Array<string> DamageStyles { get; set; } = new();

    [Export] public Array<string> CompatibleSlots { get; set; } = new();

    [Export] public int SlotPriority { get; set; } = 1;

    // e.g. TwoHanded, OneHanded, OffHand, Fist, etc
    [Export] public Array<string> HoldingStyles { get; set; } = new();

    [Export] public ItemShapeResource InventoryShape {get; set; }

    // public override string ToString() {
    //     return $"""
    //     {GetType().Name}:: [
    //         DamageAmount: {DamageAmount}, AttackSpeed: {AttackSpeed},
    //         Range: {Range}, WeaponClass: {WeaponClass},
    //         DamageTypes: {DamageTypes},
    //         DamageStyles: {DamageStyles},
    //         CompatibleSlots: {CompatibleSlots},
    //         SlotPriority: {SlotPriority},
    //         HoldingStyles: {HoldingStyles}
    //     ]
    //     """;
    // }


    // WeaponResource IResourceCreator<WeaponResource>.CreateResource(
    //     string destination,
    //     Dictionary<string, Variant> options
    // ) {
    //     options ??= new Dictionary<string, Variant>();
    //     ItemResource item = base.CreateResource(destination, options);
    //     WeaponResource weapon = new WeaponResource();
    //     weapon.CopyFrom(item);
    //     weapon.CopyFrom(this);

    //     weapon.SetScript(ResourceLoader.Load<Script>(
    //         "res://addons/item_kit/Scripts/Resources/WeaponResource.cs"
    //     ));

    //     return weapon;
    // } // CreateResource


    // public override string ToString() {
    //     return $"""
    //         {base.ToString()}
    //         {GetType().Name}:: [
    //             DamageAmount: {DamageAmount}, AttackSpeed: {AttackSpeed},
    //             Range: {Range}, WeaponClass: {WeaponClass},
    //             DamageTypes: {DamageTypes},
    //             DamageStyles: {DamageStyles},
    //             CompatibleSlots: {CompatibleSlots},
    //             SlotPriority: {SlotPriority},
    //             HoldingStyles: {HoldingStyles}
    //         ]
    //     """;

    // }


    // [Export] public override string ScriptPath => "res://addons/item_kit/Scripts/Resources/WeaponResource.cs";
} // WeaponResource
