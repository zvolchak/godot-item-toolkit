using Godot;

namespace Gamehound.ItemKit.Resources;

public partial class AccessoryResource : ItemResource {
    /// <summary>
    /// Whether this item grants passive effects while in inventory (e.g. charms)
    /// </summary>
    [Export] public bool IsPassive { get; set; } = false;

    /// <summary>
    /// Optional: limited to specific slots like Left/Right Ring, Neck
    /// </summary>
    [Export] public string EquipSlot { get; set; } = "";
} // class
