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


    protected virtual void SetModifiers(ItemResource res) {
        foreach (var m in Modifiers)
            res.Modifiers.Add(m);
    } // SetModifiers


    protected virtual void SetStatRequirements(ItemResource res) {
        foreach (var r in StatRequirements)
            res.StatRequirements.Add(r);
    } // SetStatRequirements

} // ItemJsonData
