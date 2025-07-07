using Godot;
using Gamehound.ItemKit.Resources;

namespace Gamehound.ItemKit.Examples;


[Tool]
public partial class ItemWeapon : Node2D {

    [Export] public WeaponResource WeaponProps { get; set; }


    public void ShowTooltip(ItemTooltip tooltip) {
        tooltip.Title.Text = WeaponProps.Name;
    } // ShowTooltip


} // namespace
