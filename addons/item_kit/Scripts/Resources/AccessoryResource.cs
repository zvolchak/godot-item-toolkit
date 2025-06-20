using Godot;

namespace Gamehound.ItemKit.Resources;

[GlobalClass]
public partial class AccessoryResource : ItemResource {
    // Whether this item grants passive effects while in inventory (e.g. charms)
    [Export] public bool IsPassive { get; set; } = false;

    // Optional: limited to specific slots like Left/Right Ring, Neck
    [Export] public string EquipSlot { get; set; } = "";
} // class
