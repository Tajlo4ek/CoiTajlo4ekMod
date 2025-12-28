using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;

namespace Tajlo4ekMod.InfStorage
{
    public class InfStorageRegistrator : IModData
    {
        public static readonly MachineProto.ID IdInfLoose = Ids.Machines.CreateId("TAJLO4EK_infLoose");

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
                 "   [6][6][6][6][6]   ",
                 "A~>[6][6][6][6][6]>~X",
                 "   [6][6][6][6][6]   ",
                 "B~>[6][6][6][6][6]>~Y",
                 "   [6][6][6][6][6]   ");

            EntityCostsTpl entryCost = new EntityCostsTpl.Builder().CP(50);

            LayoutEntityProto.Gfx gfx =
                new("Assets/Base/Buildings/Storages/LooseT2.prefab",
                    customIconPath: "Assets/Unity/Generated/Icons/LayoutEntity/StorageLooseT2.png",
                    categories: new ImmutableArray<ToolbarEntryData>?(registrator.GetCategoriesProtos(Ids.ToolbarCategories.Storages)));

            Proto.Str protoString = Proto.CreateStr(IdInfLoose, "Inf loose", "Inf loose");

            InfStoragePrototype bp = new(IdInfLoose, protoString, entryLayout, ProductFilter, LooseProductProto.ProductType, 500.Quantity(), entryCost.MapToEntityCosts(registrator), 1000.Quantity(), 1.Ticks(), Electricity.Zero, gfx);
            registrator.PrototypesDb.Add(bp);
        }
    }
}
