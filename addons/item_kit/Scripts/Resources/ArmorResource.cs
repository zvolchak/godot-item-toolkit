using Gamehound.ItemKit.Interfaces;
using Godot;

namespace Gamehound.ItemKit.Resources;

[Tool]
[GlobalClass]
public partial class ArmorResource : ItemResource, IArmorData {

    [Export] public int Defense { get; set; } = 0;

    // e.g., "Head", "Chest", "Legs", etc
    [Export] public string ArmorSlot { get; set; } = "Chest";

    public string ArmorMaterial { get; set; } = string.Empty;
} // ArmorResource
