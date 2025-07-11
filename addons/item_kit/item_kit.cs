#if TOOLS
using Gamehound.ItemKit.Editor;

using Godot;
using Godot.Collections;

using System;


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

        _dock.AddChild(buildBasePropsTab());
        _dock.SetTabTitle(0, "Base Properties");


        _dock.AddChild(buildSubResourcesTab());
        _dock.SetTabTitle(1, "Sub Resources");

        _dock.AddChild(buildItemsTab());
		_dock.SetTabTitle(2, "Item Resources");

		AddControlToDock(DockSlot.RightUl, _dock);
	} // CreateDockPanel


    private GroupGenerators buildBasePropsTab() {
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
        return basePropsTab;
    } // buildBasePropsGenerators


    private GroupGenerators buildSubResourcesTab() {
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
        return subResourceTab;
    } // buildSubResourcesTab


    private GroupGenerators buildItemsTab() {
        string inputDir = "res://data/items";
        string outputDir = "res://resources";
        var itemsTab = new GroupGenerators(
            new Array<ResourceFromJson>() {
                        new WeaponResourcesGenerator($"{inputDir}/weapons.json",  $"{outputDir}/weapons/"),
            }
        );
        return itemsTab;
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
