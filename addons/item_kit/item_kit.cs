#if TOOLS
using Gamehound.ItemKit.Editor;

using Godot;

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
		AddToolMenuItem(_menuBtnTitle, Callable.From(ToggleDockPanel));
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

		var shapeGen = new ItemShapeResourceGenerator();
		var texturesGen = new TexturesResourceGenerator();
		var rarityGen = new RarityResourceGenerator();
		var propModGen = new PropertyModResourceGenerator();
		var weaponGen = new WeaponResourcesGenerator();

		var subResourceGen = new SubResourcesGenerator(
			shapeGenerator: shapeGen,
			propModGenerator: propModGen,
			texturesGenerator: texturesGen,
			rarityGenerator: rarityGen
		);

		var subTab = new VBoxContainer();
		subTab.AddThemeConstantOverride("separation", 24);
		subTab.AddChild(shapeGen);
		subTab.AddChild(texturesGen);
		subTab.AddChild(propModGen);
		subTab.AddChild(rarityGen);
		subTab.AddChild(subResourceGen);

		var itemTab = new VBoxContainer();
		itemTab.AddThemeConstantOverride("separation", 24);
		itemTab.AddChild(weaponGen);

		_dock.AddChild(subTab);
		_dock.SetTabTitle(0, "Sub Resources");

		_dock.AddChild(itemTab);
		_dock.SetTabTitle(1, "Item Resources");

		AddControlToDock(DockSlot.RightUl, _dock);
	} // CreateDockPanel


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
