using Godot;

namespace Gamehound.ItemKit.Resources;

/* Something that could be used for a contextual string values. For example,
* ShortDescription, FullDescription and Tooltip - fields of an Item description
* in different context.
*/
[GlobalClass]
public partial class StringEntryResource : Resource {
    [Export] public string FieldName { get; set; } = "Field";
    [Export] public string Value { get; set; } = string.Empty;
} // StatModifierResource
