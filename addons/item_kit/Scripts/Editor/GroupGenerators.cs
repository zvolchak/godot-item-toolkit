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
            AddChild(buildSection(gen));
        }

        _generateButton = new Button { Text = "Generate All" };
        _generateButton.Pressed += () => OnGeneratePressed();

        AddChild(_generateButton);
    } // init


    private Control buildSection(ResourceFromJson gen) {
        var wrapper = new VBoxContainer();
        wrapper.AddThemeConstantOverride("separation", 6);

        var title = new Label {
            Text = gen.SettingName,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        var titleMargin = buildMarginContainer(title, 20, "margin_bottom");

        var line = new ColorRect {
            Color = new Color(0.4f, 0.4f, 0.4f, 1),
            CustomMinimumSize = new Vector2(200, 1),
            SizeFlagsHorizontal = SizeFlags.Expand
        };
        line.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        var lineWithMargin = buildMarginContainer(line, 10, "margin_top");

        wrapper.AddChild(titleMargin);
        wrapper.AddChild(gen);
        wrapper.AddChild(lineWithMargin);

        return wrapper;
    } // buildSection


    private Control buildMarginContainer(Control target, int margin, string name) {
        var container = new MarginContainer();
        container.AddThemeConstantOverride(name, margin);
        container.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        container.AddChild(target);
        return container;
    }


    public override void OnGeneratePressed() {
        foreach(ResourceFromJson gen in _generators) {
            gen.OnGeneratePressed();
        }
    } // OnGeneratePressed


} // class
