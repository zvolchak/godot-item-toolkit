using Godot;
using Godot.Collections;
using Gamehound.ItemKit.Interfaces;
using System;
using Gamehound.ItemKit.Utils;

namespace Gamehound.ItemKit.Resources;


[GlobalClass]
public partial class ItemResource :
    ItemResourceBase<ItemResource>,
    // IIdentifier,
    IItemData,
    ITradeableData {

    // [Export] public string ID { get; set; }

    // [Export] public virtual string Name { get; set; }

    // [Export] public virtual string Category { get; set; }

    // [Export(PropertyHint.MultilineText)] public virtual string Description { get; set; }

    // Use this description array if you need a context-based description
    // for an Item, example: "short description" and "full description".
    [Export] public Array<StringEntryResource> ContextDescription { get; set; }

    // The icons for different context in which this item could be shown at.
    [Export] public Array<ItemImageResource> Textures { get; set; } = new();

    [Export] public int Power { get; set; } = 0;
    [Export] public int Weight { get; set; } = 0;

    // Minimum level of the character to use this item.
    [Export] public int RequiredLevel { get; set; } = 1;

    // What minimum stats values are needed to use this item.
    [Export] public Array<PropertyModifierResource> StatRequirements { get; set; } = new();

    [Export] public Array<PropertyModifierResource> Modifiers { get; set; } = new();

    [Export] public Array<string> Tags { get; set; } = new();

    [Export] public Array<StringEntryResource> TradeValue { get; set; } = new();

    [Export] public bool IsCanSell { get; set; } = true;

    [Export] public bool IsCanBuy { get; set; } = true;

    [Export] public ItemShapeResource InventoryShape { get; set; } = new();

    [Export] public RarityResource Rarety { get; set; } = new();


    // public virtual ItemResource CreateResource(
    //     string path,
    //     Dictionary<string, Variant> options = null
    // ) {
    //     ItemKitUtils.ValidateResource(this, path);
    //     // Overwrite the resource ef it exists (by not doing anything with
    //     // ValidateResource return value), because why not.

    //     ItemResource resource = new ItemResource {
    //         ID = ID,
    //         Name = Name,
    //         Description = Description,
    //         Category = Category,
    //         ContextDescription = ContextDescription,
    //         Textures = Textures,
    //         Power = Power,
    //         Weight = Weight,
    //         RequiredLevel = RequiredLevel,
    //         StatRequirements = StatRequirements,
    //         Modifiers = Modifiers,
    //         Tags = Tags,
    //         TradeValue = TradeValue,
    //         IsCanSell = IsCanSell,
    //         IsCanBuy = IsCanBuy,
    //         InventoryShape = InventoryShape,
    //         Rarety = Rarety
    //     };

    //     resource.SetScript(ResourceLoader.Load<Script>(
    //         "res://addons/item_kit/Scripts/Resources/ItemResource.cs"
    //     ));

    //     return resource;
    // } // CreateResouceFile


    // public void CopyFrom(ItemResource target) {
    //     foreach (var property in typeof(ItemResource).GetProperties()) {
    //         if (property.CanRead && property.CanWrite) {
    //             property.SetValue(target, property.GetValue(this));
    //         }
    //     } // foreach
    // } // Copy


    // public override string ToString() {
    //     string result = string.Empty;
    //     foreach (var property in typeof(ItemResource).GetProperties()) {
    //         if (property.CanRead && property.CanWrite) {
    //             result += $"{property.Name}: {property.GetValue(this)}, ";
    //         }
    //     } // foreach

    //     return $"{GetType().Name}: [{result}]";
    //     // return $"[{GetType().Name}]: " +
    //     //     $"[ID={ID}, Name={Name}, Category={Category}, " +
    //     //     $"Description={Description}, Power={Power}, " +
    //     //     $"Weight={Weight}, RequiredLevel={RequiredLevel}]";
    // } // ToString

} // ItemResource
