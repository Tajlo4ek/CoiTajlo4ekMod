using Mafi;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;

namespace Tajlo4ekMod.InfStorage
{
    public class InfStoragePrototype : StorageProto
    {
        public InfStoragePrototype(ID id, Str strings, EntityLayout layout,
            Func<ProductProto, bool> productsFilter, ProductType? productType, Quantity capacity,
            EntityCosts costs, Quantity transferLimit, Duration transferLimitDuration,
            Electricity powerConsumedForProductsExchange, Gfx graphics, IEnumerable<Tag> tags = null)
            : base(id, strings, layout, productsFilter, productType, capacity, costs, transferLimit, transferLimitDuration, powerConsumedForProductsExchange, graphics, tags)
        {
        }

        public override Type EntityType => typeof(InfStorage);
    }
}
