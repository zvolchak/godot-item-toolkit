using Gamehound.ItemKit.Interfaces;
using Godot;

namespace Gamehound.ItemKit.Resources;

[Tool]
public partial class RarityResource :
    ItemResourceBase,
    IRarityData {

    /// <summary>
    /// Weighted chance of the item do be dropped. Higher value could mean lower chance of
    /// dropping. Though it might depend on the implementation per game.
    /// </summary>
    [Export] public int Weight { get; set; } = 1;

    /// <summary>
    /// Color of this rarity type.
    /// </summary>
    [Export] public Color RarityColor { get; set; } = Colors.White;

    /// <summary>
    /// How many instances of this could be created in the world or on the item.
    /// </summary>
    [Export] public int InstanceLimit { get; set; } = -1;

} // class
