using Gamehound.ItemKit.Interfaces;
using Gamehound.ItemKit.Utils;
using Godot;

namespace Gamehound.ItemKit.Resources;


[Tool]
[GlobalClass]
public partial class RarityResource : Resource, IRarityData, IIdentifier {
    [Export] public string ID { get; set; } = string.Empty;

    [Export] public string Name { get; set; } = string.Empty;

    public string Category => throw new System.NotImplementedException();

    [Export(PropertyHint.MultilineText)]
    public string Description { get; set; } = string.Empty;

    // Higher rarity value - less chance of a drop.
    [Export] public int Weight { get; set; } = 1;
    [Export] public string Tier { get; set; } = string.Empty;
    [Export] public Color RarityColor { get; set; } = Colors.White;
    // Default - unlimited
    [Export] public int InstanceLimit { get; set; } = -1;


    public Resource CreateResource(string path) {
        RarityResource existing = ItemKitUtils.ValidateResource(this, path) as RarityResource;
        if (existing != null)
            return existing;

        RarityResource resource = new RarityResource {
            ID = ID,
            Name = Name,
            Description = Description,
            Weight = Weight,
            Tier = Tier,
            RarityColor = RarityColor,
            InstanceLimit = InstanceLimit,
        };

        resource.SetScript(ResourceLoader.Load<Script>(
            "res://addons/item_kit/Scripts/Resources/RarityResource.cs"
        ));

        return resource;
    } // CreateResource

} // class
