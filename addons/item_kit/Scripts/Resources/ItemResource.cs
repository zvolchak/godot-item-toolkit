using Godot;
using Godot.Collections;
using Gamehound.ItemKit.Interfaces;

namespace Gamehound.ItemKit.Resources;


public partial class ItemResource :
    ItemResourceBase<ItemResource>,
    IItemData,
    ITradeableData {

    /// <summary>
    /// Use this description array if you need a context-based description an Item, example:
    /// "short description" and "full description".
    /// </summary>
    [Export] public Array<StringEntryResource> ContextDescription { get; set; }

    /// <summary>
    /// The icons for different context in which this item could be shown at.
    /// </summary>
    [Export] public Array<ItemImageResource> Textures { get; set; } = new();

    [Export] public int Power { get; set; } = 0;
    [Export] public int Weight { get; set; } = 0;

    /// <summary>
    /// Minimum level of the character to use this item.
    /// </summary>
    [Export] public int RequiredLevel { get; set; } = 1;

    /// <summary>
    /// What minimum stats values are needed to use this item.
    /// </summary>
    [Export] public Array<PropertyModifierResource> StatRequirements { get; set; } = new();

    [Export] public Array<PropertyModifierResource> Modifiers { get; set; } = new();

    [Export] public Array<string> Tags { get; set; } = new();

    [Export] public Array<StringEntryResource> TradeValue { get; set; } = new();

    [Export] public bool IsCanSell { get; set; } = true;

    [Export] public bool IsCanBuy { get; set; } = true;

    [Export] public ItemShapeResource InventoryShape { get; set; } = new();

    [Export] public RarityResource Rarety { get; set; } = new();

} // ItemResource
