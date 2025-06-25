using Gamehound.ItemKit.Editor;
using Gamehound.ItemKit.Interfaces;

using Godot;
using Godot.Collections;

using System.Text.Json.Serialization;

namespace Gamehound.ItemKit.Resources;

[Tool]
public partial class ItemShapeResource :
    ItemResourceBase,
    IItemShape {

    [Export] public int Width { get; set; } = 1;

    [JsonConverter(typeof(ListToGodotArraySerializer<int>))]
    [Export] public Array<int> Layout { get; set; } = new();


    public virtual int Get(int col, int row) {
        int index = row * Width + col;
        return Layout[index];
    } // Get

} // ItemShape
