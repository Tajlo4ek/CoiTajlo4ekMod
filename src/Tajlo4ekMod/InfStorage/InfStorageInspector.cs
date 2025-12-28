using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Products;
using Mafi.Unity.Ui;
using Mafi.Unity.Ui.Library.Inspectors;
using Mafi.Unity.UiToolkit.Library;
using System.Linq;

namespace Tajlo4ekMod.InfStorage
{
    public class InfStorageInspector : BaseInspector<InfStorage>
    {
        readonly Dropdown<ProductProto> storedProduct;

        public InfStorageInspector(UiContext context) : base(context)
        {
            storedProduct = new Dropdown<ProductProto>(delegate (ProductProto pickProto, int _, bool _)
            {
                Row row = new(2.pt())
                {
                    new Icon(pickProto.IconPath),
                    new Label(pickProto.Strings.Name)
                };
                return row;
            }, null, null, false)
                .SetOptions((from proto in context.ProtosDb.All<ProductProto>()
                             where proto.IsInitialized
                             where InfStorageRegistrator.ProductFilter(proto)
                             orderby proto.Strings.Name.ToString()
                             select proto).ToImmutableArray()).SetValueIndex(0, false).OnValueChanged(delegate (ProductProto changedProto, int _)
                             {
                                 Entity?.SetProduct(changedProto);
                             });

            Panel UTPanel = AddPanel();
            UTPanel.Add(storedProduct);

            HeaderPanel.Body.Add(UTPanel);
        }


        protected override void OnActivated()
        {
            storedProduct.SetValue(Entity.GetProduct());
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
