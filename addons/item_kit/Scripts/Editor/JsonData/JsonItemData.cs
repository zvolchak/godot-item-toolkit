using Gamehound.ItemKit.Editor;
using Gamehound.ItemKit.Resources;
using Godot;
using System.Collections.Generic;
using System;
using Godot.Collections;
using System.Text.Json.Serialization;


namespace Gamehound.ItemKit.Editor;


internal class JsonItemData {

    public string ID { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }

    public List<StringEntryResource> ContextDescription { get; set; } = new();
    public List<JsonItemTextureData> Textures { get; set; } = new();

    public int Power { get; set; }
    public int Weight { get; set; }
    public int RequiredLevel { get; set; }

    public List<string> Tags { get; set; } = new();
    public List<PropertyModifierResource> StatRequirements { get; set; } = new();
    public List<PropertyModifierResource> Modifiers { get; set; } = new();

    public int RarityWeight { get; set; }
    public string RarityTier { get; set; }

    public Color RarityColor { get; set; }

    public int InstanceLimit { get; set; }

    public List<StringEntryResource> TradeValue { get; set; } = new();
    public bool IsCanSell { get; set; }
    public bool IsCanBuy { get; set; }
    public JsonItemShapeData InventoryShape { get; set; } = new();


    /// <summary>
    /// Set primitive or simple values that doesn't require any extra processing
    /// beyond a simple assignment of values from json to a resource.
    /// </summary>
    protected virtual void SetPrimitives(ItemResource res) {
        res.ID = ID;
        res.Name = Name;
        res.Category = Category;
        res.Description = Description;
        res.Power = Power;
        res.Weight = Weight;
        res.RequiredLevel = RequiredLevel;
        res.Weight = RarityWeight;
        res.Tags = new Array<string>(Tags);
        res.ContextDescription = new Array<StringEntryResource>(ContextDescription);
    } // SetPrimitives


    //protected virtual void SetTextures(ItemResource res) {
    //    foreach (JsonItemTextureData textureData in Textures) {
    //        string texturePath = $"res://{textureData.Path}";
    //        if (ResourceLoader.Exists(texturePath, "Texture2D")) {
    //            var textureAsset = ResourceLoader.Load(texturePath, "Texture2D") as Texture2D;
    //            ItemImageResource textureResource = new ItemImageResource {
    //                ID = textureData.ID ?? Guid.NewGuid().ToString("N").Substring(0, 5),
    //                Context = textureData.Context,
    //                TextureAsset = textureAsset
    //            };
    //            res.Textures.Add(textureResource);
    //        }
    //        else {
    //            GD.PrintErr($"Missing texture: {texturePath}");
    //        }
    //    } // foreach
    //} // SetTextures


    protected virtual void SetModifiers(ItemResource res) {
        foreach (var m in Modifiers)
            res.Modifiers.Add(m);
    } // SetModifiers


    protected virtual void SetStatRequirements(ItemResource res) {
        foreach (var r in StatRequirements)
            res.StatRequirements.Add(r);
    } // SetStatRequirements


    //protected virtual void SetInventoryShape(ItemResource res) {
    //    res.InventoryShape = new();
    //    res.InventoryShape.ID = InventoryShape.ID;
    //    res.InventoryShape.Width = InventoryShape.Width;
    //    res.InventoryShape.Layout = new Array<int>(InventoryShape.Layout);
    //} // SetInventoryShape


    protected virtual void Populate(ItemResource res) {
        SetPrimitives(res);
        //SetTextures(res);
        SetModifiers(res);
        //SetStatRequirements(res);
        //SetInventoryShape(res);
    } // Populate


    public virtual ItemResource ToResource() {
        var res = new ItemResource();
        Populate(res);
        return res;
    } // ToResource
} // ItemJsonData
