using Mafi;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Ports;
using Mafi.Core.Ports.Io;
using Mafi.Core.Products;
using Mafi.Serialization;
using System;
using static Mafi.Core.Entities.EntityProto;

namespace Tajlo4ekMod.voidMashines
{
    public class VoidGenerator : LayoutEntity, IEntityWithSimUpdate, IEntityWithPorts, IEntityWithCloneableConfig
    {
        private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;
        private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

        readonly LayoutEntityProto proto;
        ProductQuantity generateProd;
        EntityContext context;


        public VoidGenerator(EntityId id, LayoutEntityProto proto, TileTransform transform, EntityContext context)
            : base(id, proto, transform, context)
        {
            this.proto = proto;
            this.context = context;
            generateProd = ProductQuantity.None;
        }

        public override bool CanBePaused => true;

        public Quantity ReceiveAsMuchAsFromPort(ProductQuantity pq, IoPortToken sourcePort)
        {
            return pq.Quantity;
        }

        public void SetProduct(ProductProto prod)
        {
            generateProd = new ProductQuantity(prod, 1000.Quantity());
        }

        public ProductProto GetProduct()
        {
            return generateProd.Product;
        }

        public void SimUpdate()
        {
            if (IsPaused)
            {
                return;
            }

            if (generateProd != ProductQuantity.None)
            {
                SendToOuputPort(generateProd);
            }
        }

        public void SendToOuputPort(ProductQuantity quantity)
        {
            foreach (var port in ConnectedOutputPorts)
            {
                port.SendAsMuchAs(quantity);
            }

        }

        public static void Serialize(VoidGenerator value, BlobWriter writer)
        {
            if (writer.TryStartClassSerialization(value))
            {
                writer.EnqueueDataSerialization(value, s_serializeDataDelayedAction);
            }
        }

        protected override void SerializeData(BlobWriter writer)
        {
            base.SerializeData(writer);
            writer.WriteGeneric(proto);
            writer.WriteGeneric(generateProd);
        }

        public static VoidGenerator Deserialize(BlobReader reader)
        {
            if (reader.TryStartClassDeserialization(out VoidGenerator obj, null))
            {
                reader.EnqueueDataDeserialization(obj, s_deserializeDataDelayedAction);
            }
            return obj;
        }

        protected override void DeserializeData(BlobReader reader)
        {
            base.DeserializeData(reader);
            reader.SetField(this, "proto", reader.ReadGenericAs<VoidGeneratorPrototype>());
            reader.SetField(this, "generateProd", reader.ReadGenericAs<ProductQuantity>());
        }

        public void AddToConfig(EntityConfigData data)
        {
            data.SetString("generateProd", generateProd.Product.Id.ToString());
        }

        public void ApplyConfig(EntityConfigData data)
        {
            var strId = data.GetString("generateProd");
            if (strId.HasValue)
            {
                var id = new ID(strId.Value);
                var proto = context.ProtosDb.Get<ProductProto>(id);
                if (proto.HasValue)
                {
                    SetProduct(proto.Value);
                }
            }
        }

        static VoidGenerator()
        {
            s_serializeDataDelayedAction = delegate (object obj, BlobWriter writer)
            {
                ((VoidGenerator)obj).SerializeData(writer);
            };
            s_deserializeDataDelayedAction = delegate (object obj, BlobReader reader)
            {
                ((VoidGenerator)obj).DeserializeData(reader);
            };
        }

    }
}
