using Gamehound.ItemKit.Resources;

namespace Gamehound.ItemKit.Editor;


public partial class ItemBaseGenerator
    : Generator<ItemResourceBase, ItemResourceBase> {

    public ItemBaseGenerator(
        string inputPath,
        string outputPath,
        string settingName = null,
        string generateBtnText = null
    )
        : base(inputPath, outputPath, settingName: settingName, generateBtnText: generateBtnText) {
    }


    protected override ItemResourceBase ExtractResource(ItemResourceBase item) {
        return item;
    }

} // class
