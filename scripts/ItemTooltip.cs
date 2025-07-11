using Gamehound.ItemKit.Resources;

using Godot;

namespace Gamehound.ItemKit.Examples;


[Tool]
public partial class ItemTooltip : Control {

    [Export] public Label Title { get; set; }
    [Export] public Label DmgOrDefTitle { get; set; }
    [Export] public ItemWeapon DebugWeapon { get; set; }


    public override void _Ready() {
        UpdateDisplay();
    } // _Ready



    public override void _Notification(int what) {
        if (what == NotificationEditorPostSave) {
            assingLabelVars(GetParent());
            UpdateDisplay();
        }
    } // _Notification


    protected void assingLabelVars(Node parent) {
        foreach (Node child in parent.GetChildren()) {
            SetLabelByNodeName(child);
            // Recursively process this child's children
            assingLabelVars(child);
        }
    } // findAndSetLabels


    public void SetLabelByNodeName(Node node) {
        string nodeName = node.Name.ToString().ToLower();
        switch (nodeName) {
            case "title":
                Title = node as Label;
                break;
            case "dmgordeftitle":
                DmgOrDefTitle = node as Label;
                break;
            default: return;
        }
    } // GetLableFromName


    public void SetLableText(Label target, string value) {
        if (target == null)
            return;

        target.Text = value;
    } // SetLableText


    private void UpdateDisplay() {
        if (DebugWeapon == null || DebugWeapon.WeaponProps == null)
            return;

        DebugWeapon.ShowTooltip(this);
    } // UpdateDisplay

} // class
