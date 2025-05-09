using Godot;

namespace Gamehound.ItemKit.Editor;


public partial class SubResourcesGenerator : ResourceFromJson {

    protected ItemShapeResourceGenerator _shapeGenerator;
    protected PropertyModResourceGenerator _propModGenerator;
    protected TexturesResourceGenerator _texturesGenerator;


    protected SubResourcesGenerator() {}


    public SubResourcesGenerator(
        ItemShapeResourceGenerator shapeGenerator = null,
        PropertyModResourceGenerator propModGenerator = null,
        TexturesResourceGenerator texturesGenerator = null
    ) {
        _shapeGenerator = shapeGenerator;
        _propModGenerator = propModGenerator;
        _texturesGenerator = texturesGenerator;
    } // ctor


    protected override void init() {
        GD.Print($"- {this.GetType()} initialized");

        _generateButton = new Button { Text = "Generate All" };
        _generateButton.Pressed += () => OnGeneratePressed();

        AddChild(_generateButton);
    } // init


    public override void OnGeneratePressed() {
        _shapeGenerator.OnGeneratePressed();
        _propModGenerator.OnGeneratePressed();
        _texturesGenerator.OnGeneratePressed();
    } // OnGeneratePressed


} // class
