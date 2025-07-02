using Godot;

namespace Gamehound.ItemKit.Editor;


public partial class SubResourcesGenerator : ResourceFromJson {

    protected ItemShapeResourceGenerator _shapeGenerator;
    protected PropertyModResourceGenerator _propModGenerator;
    protected TexturesResourceGenerator _texturesGenerator;
    protected RarityResourceGenerator _rarityGenerator;


    protected SubResourcesGenerator() {}


    public SubResourcesGenerator(
        ItemShapeResourceGenerator shapeGenerator = null,
        PropertyModResourceGenerator propModGenerator = null,
        TexturesResourceGenerator texturesGenerator = null,
        RarityResourceGenerator rarityGenerator = null
    ) {
        _shapeGenerator = shapeGenerator;
        _propModGenerator = propModGenerator;
        _texturesGenerator = texturesGenerator;
        _rarityGenerator = rarityGenerator;
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
        _rarityGenerator.OnGeneratePressed();
    } // OnGeneratePressed


} // class
