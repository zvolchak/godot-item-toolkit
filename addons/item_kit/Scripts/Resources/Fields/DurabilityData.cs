using Godot;
using Gamehound.ItemKit.Interfaces;

namespace Gamehound.ItemKit.Resources;


[Tool]
public partial class DurabilityData : Resource, IDurableData {

    [Export] public int MaxDurability { get; set; } = 0;

    [Export] public bool IsRepairable { get; set; } = true;

    [Export] public bool IsPermaBreak { get; set; } = false;

    [Export] public float DegradeRateModifier { get; set; } = 1;

    [Export] public float CurrentDurability { get; set; } = 0;


    public void SetDurability(float value) {
        CurrentDurability = value;
        CurrentDurability = Mathf.Clamp(CurrentDurability, 0, MaxDurability);
    } // SetDurability


    public void UpdateDurability(float value) {
        CurrentDurability += value;
        CurrentDurability = Mathf.Clamp(CurrentDurability, 0, MaxDurability);
    } // UpdateDurability


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
