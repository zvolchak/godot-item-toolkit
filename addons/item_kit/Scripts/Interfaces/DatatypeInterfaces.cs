using System.Collections.Generic;

using Gamehound.ItemKit.Resources;

using Godot;
using Godot.Collections;

namespace Gamehound.ItemKit.Interfaces;


public interface IIdentifier {
    public string ID { get; }
    public string Name { get; }
    public string Category { get; }
    public string Description { get; }
} // IIdentifier


public interface IItemShape {
    public int Width { get; }
    public Array<int> Layout { get; }

    /// <summary>
    /// Return a cell value at position [col, row].
    /// </summary>
    public int Get(int col, int row);
} // IInventoryShape


public interface IRarityData {
    // Higher rarity value - less chance of a drop.
    public int Weight { get; }

    // Could be a UI or a Name color of the item.
    public Color RarityColor { get; }

    // How many copies of the item can there be in the World, owned by a player
    // and etc. Set it to 1 means a Unique item that can be owned by a player once.
    // Minus 1 or 0 would mean "unlimited"
    public int InstanceLimit { get; }
} // IRarity


public interface IImageData {
    public string ImagePath { get; }
    public Texture2D TextureAsset { get; }
} // IImageData


public interface IEquippableData {
    /// <summary>
    /// Slot to which this item can be equiped to (e.g. Chest, Left Hand, Belt, etc)
    /// </summary>
    public Array<string> CompatibleSlots { get; }

    /// <summary>
    /// Helps resolve slot conflicts or UI ordering
    /// </summary>
    public int SlotPriority { get; }
} // IEquippable


public interface IStackableData {
    public int MaxStackSize { get; }

    // Initial stack amount(e.g.potions spawn with 3)
    public int DefaultStackSize { get; }

    // Whether stack must be full to use or consume
    public bool AllowPartialStack { get; }
} // IStackableData


public interface IDurableData {
    public int MaxDurability { get; }

    public bool IsRepairable { get; }

    // Whether item is destroyed at 0 durability, or just becomes unusable
    public bool BreaksPermanently { get; }

    // Allows disabling damage logic for “indestructible” items
    public bool IsDamageable { get; }

    // 	Multiplier used to scale how fast durability depletes
    public float DegradeRateModifier { get; }
} // IDurable


public interface IDamageData {
    public Vector3 Amount { get; }
    public float AttackSpeed { get; }
    public float Cooldown { get; }
    public float Range { get; }
    public DamageTypeResource DamageType { get; }
    public AttackTypeResource AttackType { get; }
} // IDamageData


public interface IWeaponData {
    // Could be different from CategoryName of the IItem base class by further
    // categorization of a weapon: "Melee", "Ranged", "Finesse", etc.
    public WeaponClassResource WeaponClass { get; }
    public Array<DamageData> Damages { get; }

    // e.g. TwoHanded, OneHanded, OffHand, Fist, etc
    public Array<string> HoldingStyles { get; }
} // IWeaponData


public interface IArmorData {
    public int Defense { get; }
    // "Iron", "Leather", etc
    public string ArmorMaterial { get; }
} // IArmorData


/* Modifiers that affect Offensive corresteristics of an equipment item.
* Could be increase to Min attach damage, overall damage, attack speed,
* crit chance or damage and etc.
*/
public interface IPropertyModifier {
    // e.g. "MinDamage", "AttackSpeed", "Resist Cold", "Strength", etc
    public string TargetProperty { get; }
    public Vector2 FlatValue { get; }
    public Vector2 PercentValue { get; }
} // IPropertyModifier
