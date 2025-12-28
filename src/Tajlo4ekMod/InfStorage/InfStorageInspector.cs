using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Unity;
using Mafi.Unity.Ui;
using Mafi.Unity.Ui.Library;
using Mafi.Unity.Ui.Library.Inspectors;
using Mafi.Unity.UiStatic.Inspectors.Vehicles;
using Mafi.Unity.UiToolkit.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Tajlo4ekMod.InfStorage
{
    public class InfStorageInspector : BaseInspector<InfStorage>
    {
        private readonly UiContext uiContext;
        private readonly LinesFactory linesFactory;
        private readonly LineOverlayRendererHelper goalLineRenderer;

        readonly Dropdown<ProductProto> storedProduct;

        public InfStorageInspector(UiContext context,
                       LinesFactory lineFactory) : base(context)
        {
            uiContext = context;
            linesFactory = lineFactory;

            goalLineRenderer = new LineOverlayRendererHelper(linesFactory);
            goalLineRenderer.SetWidth(0.5f);
            goalLineRenderer.SetColor(Color.white);
            goalLineRenderer.HideLine();

            storedProduct = new Dropdown<ProductProto>(delegate (ProductProto pickProto, int _, bool _)
            {
                Row row = new(2.pt())
                {
                    new Icon(pickProto.IconPath),
                    new Label(pickProto.Strings.Name)
                };
                return row;
            }, null, null, false)
                .SetOptions((from proto in uiContext.ProtosDb.All<ProductProto>()
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
            goalLineRenderer.HideLine();
        }
    }
}
