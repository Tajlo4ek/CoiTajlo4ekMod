using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;

namespace Tajlo4ekMod.voidMashines
{
    public class VoidGeneratorRegistrator : IModData
    {
        public static readonly MachineProto.ID IdVoidGenerator = Ids.Machines.CreateId("TAJLO4EK_voidGenerator");

        public static bool ProductFilter(ProductProto prod)
        {
            return prod.Id == Ids.Products.Coal
                || prod.Id == Ids.Products.IronOre
                || prod.Id == Ids.Products.CopperOre
                || prod.Id == Ids.Products.GoldOre
                || prod.Id == Ids.Products.Dirt
                || prod.Id == Ids.Products.Rock
                || prod.Id == Ids.Products.Limestone
                || prod.Id == Ids.Products.Sand;
        }

        public void RegisterData(ProtoRegistrator registrator)
        {
            EntityLayout entryLayout = registrator.LayoutParser.ParseLayoutOrThrow(
               "   [3][3][3][3][3]   ",
               "   [3][3][3][3][3]>~X",
               "   [3][3][3][3][3]   ");

            EntityCostsTpl entryCost = new EntityCostsTpl.Builder().CP(50);

            LayoutEntityProto.Gfx gfx =
                new("Assets/Base/Machines/Waste/Compactor.prefab",
                    customIconPath: "Assets/Unity/Generated/Icons/LayoutEntity/Compactor.png",
                    categories: new ImmutableArray<ToolbarEntryData>?(registrator.GetCategoriesProtos(Ids.ToolbarCategories.Storages)));

            Proto.Str protoString = Proto.CreateStr(IdVoidGenerator, "Void generator", "Void generator");

            VoidGeneratorPrototype proto = new(IdVoidGenerator, protoString, entryLayout, entryCost.MapToEntityCosts(registrator), gfx);

            proto.SetAvailability(true);
            registrator.PrototypesDb.Add(proto);
        }
    }
}
