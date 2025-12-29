using Mafi;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;

namespace Tajlo4ekMod.voidMashines
{
    public class VoidGeneratorPrototype : LayoutEntityProto
    {
        public VoidGeneratorPrototype(ID id, Str strings, EntityLayout layout, EntityCosts costs, Gfx graphics, Duration? constructionDurationPerProduct = null, Upoints? boostCost = null, bool cannotBeBuiltByPlayer = false, bool isUnique = false, bool cannotBeReflected = false, bool autoBuildMiniZippers = false, bool doNotStartConstructionAutomatically = false, bool doNotCheckVehicleGoalHeightRange = false, Percent? collapseRubbleScale = null, ThicknessTilesF? customBuriedTolerance = null, IEnumerable<Tag> tags = null)
            : base(id, strings, layout, costs, graphics, constructionDurationPerProduct, boostCost, cannotBeBuiltByPlayer, isUnique, cannotBeReflected, autoBuildMiniZippers, doNotStartConstructionAutomatically, doNotCheckVehicleGoalHeightRange, collapseRubbleScale, customBuriedTolerance, tags)
        {
        }

        public override Type EntityType => typeof(VoidGenerator);

    }
}
