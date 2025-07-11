#if TOOLS
using System;

using Gamehound.ItemKit.Editor;

using Godot;
using Godot.Collections;


[Tool]
public partial class item_kit : EditorPlugin {
	private TabContainer _dock;

	private double _lastScriptWriteTime;
	/// <summary>
	/// Delay before the next update scan on the files is done.
	/// </summary>
	private double _refreshThrottleTime = 0.5f;
	private double _currRefreshTime = 0;
	private const string _watchDir = "res://addons/item_kit/";
	private string _menuBtnTitle = "Item Kit Panel";


	public override void _EnterTree() {
        var button = new Button();
        button.Text = "Item Kit";
        button.Pressed += ToggleDockPanel;
        AddControlToContainer(CustomControlContainer.Toolbar, button);

        _lastScriptWriteTime = GetLatestScriptWriteTime();
		SetProcess(true);
	} // _EnterTree


	public override void _ExitTree() {
		RemoveToolMenuItem(_menuBtnTitle);

		if (_dock != null && _dock.IsInsideTree()) {
			RemoveControlFromDocks(_dock);
			_dock.QueueFree();
		}

		_dock = null;

		SetProcess(false);
		base._ExitTree();
	} // _ExitTree


	public override void _Process(double delta) {
        // No need to check for files update in the directory if the docker is not open, since
        // it is already unloaded from the editor's runtime.
        if (_dock == null || _dock.IsQueuedForDeletion()) {
		    _currRefreshTime = 0;
            return;
        }

        _currRefreshTime += delta;
        // Throttle how oftern directories are checked for updates.
		if (_currRefreshTime < _refreshThrottleTime)
            return;

		_currRefreshTime = 0;

		double latest = GetLatestScriptWriteTime();
		if (latest > _lastScriptWriteTime) {
			CloseDock();
			_lastScriptWriteTime = latest;
		}
	} // _Process


	private void ToggleDockPanel() {
		if (_dock != null) {
			CloseDock();
			return;
		}

		CreateDockPanel();
	} // ToggleDockPanel


    private void CreateDockPanel() {
        _dock = new TabContainer();
        _dock.SizeFlagsVertical = Control.SizeFlags.Expand;
        _dock.SizeFlagsHorizontal = Control.SizeFlags.Expand;

        _dock.AddChild(buildTypesResourcesTab());
        _dock.SetTabTitle(_dock.GetTabCount() - 1, "Types");

        _dock.AddChild(buildInventoryResourcesTab());
        _dock.SetTabTitle(_dock.GetTabCount() - 1, "Inventory");

        _dock.AddChild(buildAttributeResourceTab());
        _dock.SetTabTitle(_dock.GetTabCount() - 1, "Attributes");

        _dock.AddChild(buildItemsTab());
        _dock.SetTabTitle(_dock.GetTabCount() - 1, "Items");

        AddControlToDock(DockSlot.RightUl, _dock);
    } // CreateDockPanel


    private Control buildScrollContainer(Control content) {
        var scroll = new ScrollContainer {
            SizeFlagsVertical = Control.SizeFlags.ExpandFill,
            SizeFlagsHorizontal = Control.SizeFlags.ExpandFill,
            VerticalScrollMode = ScrollContainer.ScrollMode.Auto,
        };

        content.SizeFlagsVertical = Control.SizeFlags.ExpandFill;
        content.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;

        scroll.AddChild(content);
        return scroll;
    }


    private Control buildTypesResourcesTab() {
        string inputDir = "res://data/base_properties";
        string outputDir = "res://resources";
        var basePropsTab = new GroupGenerators(
            new Array<ResourceFromJson>() {
                new ItemBaseGenerator(
                    $"{inputDir}/weapon_classes.json",
                    $"{outputDir}/weapon_classes/",
                    settingName: "WeaponClassResource",
                    generateBtnText: "Weapon Class Resources"
                ),
                new ItemBaseGenerator(
                    $"{inputDir}/attack_types.json",
                    $"{outputDir}/attack_types/",
                    settingName: $"AttackTypeResource",
                    generateBtnText: "Attack Type Resources"
                ),
                new ItemBaseGenerator(
                    $"{inputDir}/damage_types.json",
                    $"{outputDir}/damage_types/",
                    settingName: "DamageTypeResource",
                    generateBtnText: "Damage Type Resources"
                ),
                new ItemBaseGenerator(
                    $"{inputDir}/holding_types.json",
                    $"{outputDir}/holding_types/",
                    settingName: "HoldingTypeResource",
                    generateBtnText: "Holding Type Resources"
                )
            }
        );
        return buildScrollContainer(basePropsTab);
    } // buildBasePropsGenerators


    private Control buildInventoryResourcesTab() {
        string inputDir = "res://data/sub_resources";
        string outputDir = "res://resources";
        var subResourceTab = new GroupGenerators(
            new Array<ResourceFromJson>() {
                new ItemShapeResourceGenerator(
                    $"{inputDir}/item_shapes.json",
                    $"{outputDir}/shapes/",
                    settingName: "ItemShapeResource"
                ),
                new TexturesResourceGenerator(
                    $"{inputDir}/item_images.json",
                    $"{outputDir}/images/",
                    settingName: "ItemImageResource"

                ),
            }
        );
        return buildScrollContainer(subResourceTab);
    } // buildSubResourcesTab


    private Control buildAttributeResourceTab() {
        string inputDir = "res://data/sub_resources";
        string outputDir = "res://resources";
        var attribTab = new GroupGenerators(
            new Array<ResourceFromJson>() {
                new RarityResourceGenerator(
                    $"{inputDir}/rarities.json",
                    $"{outputDir}/rarities/",
                    settingName: "RarityResource"
                ),
                new PropertyModResourceGenerator(
                    $"{inputDir}/modifiers.json",
                    $"{outputDir}/modifiers/",
                    settingName: "PropertyModResource"
                ),
            }
        );
        return buildScrollContainer(attribTab);
    } // buildAttributeResourceTab


    private Control buildItemsTab() {
        string inputDir = "res://data/items";
        string outputDir = "res://resources";
        var itemsTab = new GroupGenerators(
            new Array<ResourceFromJson>() {
                        new WeaponResourcesGenerator($"{inputDir}/weapons.json",  $"{outputDir}/weapons/"),
            }
        );
        return buildScrollContainer(itemsTab);
    } // buildSubResourcesTab


    private void CloseDock() {
		if (_dock != null && _dock.IsInsideTree()) {
			RemoveControlFromDocks(_dock);
			_dock.QueueFree();
		}

		_dock = null;
	} // CloseDock
	

	private double GetLatestScriptWriteTime() {
		return ScanDirectoryForModifiedTime(_watchDir);
	} // GetLatestScriptWriteTime


	private double ScanDirectoryForModifiedTime(string path) {
		double latest = 0;

		var dir = DirAccess.Open(path);
		if (dir == null)
			return latest;

		dir.ListDirBegin();

		while (true) {
			var entry = dir.GetNext();
			if (string.IsNullOrEmpty(entry))
				break;

			if (entry == "." || entry == "..")
				continue;

			var fullPath = path + entry;

			if (dir.CurrentIsDir()) {
				latest = Math.Max(latest, ScanDirectoryForModifiedTime(fullPath + "/"));
			} else if (entry.EndsWith(".cs")) {
				var time = FileAccess.GetModifiedTime(fullPath);
				latest = Math.Max(latest, time);
			}
		}

		dir.ListDirEnd();
		return latest;
	} // ScanDirectoryForModifiedTime

} // class
#endif
