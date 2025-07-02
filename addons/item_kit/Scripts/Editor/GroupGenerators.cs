using Godot;
using Godot.Collections;

namespace Gamehound.ItemKit.Editor;


public partial class GroupGenerators : ResourceFromJson {

    protected Array<ResourceFromJson> _generators = new Array<ResourceFromJson>();


    protected GroupGenerators() { }


    public GroupGenerators(
        Array<ResourceFromJson> generators
    ) {
        _generators = new Array<ResourceFromJson>(generators);
    } // ctor


    protected override void init() {
        GD.Print($"- {this.GetType()} initialized");

        AddThemeConstantOverride("separation", 24);
        foreach (ResourceFromJson gen in _generators) {
            AddChild(gen);
        }

        _generateButton = new Button { Text = "Generate All" };
        _generateButton.Pressed += () => OnGeneratePressed();

        AddChild(_generateButton);
    } // init


    public override void OnGeneratePressed() {
        foreach(ResourceFromJson gen in _generators) {
            gen.OnGeneratePressed();
        }
    } // OnGeneratePressed


} // class
