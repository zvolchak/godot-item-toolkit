#if TOOLS
using Gamehound.ItemKit.Editor;
using Godot;

[Tool]
public partial class item_kit : EditorPlugin {
    private WeaponResourcesGenerator _weaponGen;

    private ItemShapeResourceGenerator _shapeGen;
    private TexturesResourceGenerator _texturesGen;
    private PropertyModResourceGenerator _propModGen;
    private SubResourcesGenerator _subResourceGen;

    private TabContainer _propertiesTab;
    private TabContainer _itemsTab;
    private VBoxContainer _propsContainer;
    private VBoxContainer _itemsContainer;


    public override void _EnterTree() {
        _weaponGen = new WeaponResourcesGenerator();
        _shapeGen = new ItemShapeResourceGenerator();
        _texturesGen = new TexturesResourceGenerator();
        _propModGen = new PropertyModResourceGenerator();

        _subResourceGen = new SubResourcesGenerator(
            shapeGenerator: _shapeGen,
            propModGenerator: _propModGen,
            texturesGenerator: _texturesGen
        );

        _propsContainer = new VBoxContainer();
        _propsContainer.AddThemeConstantOverride("separation", 24);
        _propsContainer.AddChild(_shapeGen);
        _propsContainer.AddChild(_texturesGen);
        _propsContainer.AddChild(_propModGen);
        _propsContainer.AddChild(_subResourceGen);

        _itemsContainer = new VBoxContainer();
        _itemsContainer.AddThemeConstantOverride("separation", 24);
        _itemsContainer.AddChild(_weaponGen);

        _propertiesTab = new TabContainer();
        AddTabWithContainer(_propertiesTab, _propsContainer, "Sub Resources");
        AddTabWithContainer(_propertiesTab, _itemsContainer, "Item Resources");

        AddControlToDock(DockSlot.RightUl, _propertiesTab);
    } // _EnterTree


    public override void _ExitTree() {
        cleanup(_propertiesTab);
        cleanup(_itemsTab);
        cleanup(_propsContainer);
        cleanup(_itemsContainer);

        cleanup(_weaponGen);
        cleanup(_shapeGen);
        cleanup(_texturesGen);
        cleanup(_propModGen);
        cleanup(_subResourceGen);

        base._ExitTree();
    } // _ExitTree


    private void AddTabWithContainer(TabContainer tabContainer, VBoxContainer container, string tabName) {
        container.AddThemeConstantOverride("separation", 24);
        tabContainer.AddChild(container);
        tabContainer.SetTabTitle(tabContainer.GetChildCount() - 1, tabName);
    }


    protected virtual void cleanup(Container target) {
        RemoveControlFromDocks(target);
        target?.QueueFree();
    } // cleanup

} // class
#endif
