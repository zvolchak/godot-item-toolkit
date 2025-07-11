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

    [Export] public bool CanOverlap { get; set; }

    public virtual int Height => Layout.Count;



    public virtual int GetIndex(int col, int row, int width = -1) {
        if (width < 0)
            width = Width;

        int index = row * width + col;
        return Layout[index];
    } // Get


    public virtual Vector2I GetCoords(int index, int width = -1, int height = -1) {
        width = width < 0 ? Width : width;
        height = height < 0 ? Height : height;
        return new Vector2I(index * width, index * height);
    } // Get

} // ItemShape
