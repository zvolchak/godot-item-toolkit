using Godot;
using Gamehound.ItemKit.Interfaces;

namespace Gamehound.ItemKit.Resources;


[Tool]
public partial class DamageData : Resource, IDamageData {

   [Export] public Vector3 Amount { get; set; } = Vector3.Zero;

   [Export] public float AttackSpeed { get; set; } = 0.0f;

   [Export] public float Cooldown { get; set; } = 0.0f;

   [Export] public float Range { get; set; } = 0.0f;

   [Export] public ItemResourceBase DamageType { get; set; }

   [Export] public ItemResourceBase AttackType { get; set; }


    public override string ToString() {
        string result = string.Empty;
        foreach (var property in GetType().GetProperties()) {
            if (property != null && property.CanRead && property.CanWrite) {
                result += $"{property.Name}: {property.GetValue(this)}, ";
            }
        }

        return $"{GetType().Name}: [{result}]";
    } // ToString

} // class
