using Mafi;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Ports;
using Mafi.Core.Ports.Io;
using Mafi.Core.Products;
using Mafi.Serialization;
using System;

namespace Tajlo4ekMod.InfStorage
{
    public class InfStorage : LayoutEntity, IEntityWithSimUpdate, IEntityWithPorts
    {
        private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;
        private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

        readonly LayoutEntityProto proto;
        ProductQuantity storedProd;

        public InfStorage(EntityId id, LayoutEntityProto proto, TileTransform transform, EntityContext context)
            : base(id, proto, transform, context)
        {
            this.proto = proto;
            storedProd = ProductQuantity.None;
        }

        public override bool CanBePaused => true;

        public Quantity ReceiveAsMuchAsFromPort(ProductQuantity pq, IoPortToken sourcePort)
        {
            return pq.Quantity;
        }

        public void SetProduct(ProductProto prod)
        {
            storedProd = new ProductQuantity(prod, 1000.Quantity());
        }

        public ProductProto GetProduct()
        {
            return storedProd.Product;
        }

        public void SimUpdate()
        {
            if (IsPaused)
            {
                return;
            }

            if (storedProd != ProductQuantity.None)
            {
                SendToOuputPort(storedProd);
            }
        }

        public void SendToOuputPort(ProductQuantity quantity)
        {
            foreach (var port in ConnectedOutputPorts)
            {
                port.SendAsMuchAs(quantity);
            }

        }

        public static void Serialize(InfStorage value, BlobWriter writer)
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
            writer.WriteGeneric(storedProd);
        }

        public static InfStorage Deserialize(BlobReader reader)
        {
            if (reader.TryStartClassDeserialization(out InfStorage obj, null))
            {
                reader.EnqueueDataDeserialization(obj, s_deserializeDataDelayedAction);
            }
            return obj;
        }

        protected override void DeserializeData(BlobReader reader)
        {
            base.DeserializeData(reader);
            reader.SetField(this, "proto", reader.ReadGenericAs<InfStoragePrototype>());
            reader.SetField(this, "storedProd", reader.ReadGenericAs<ProductQuantity>());
        }

        static InfStorage()
        {
            s_serializeDataDelayedAction = delegate (object obj, BlobWriter writer)
            {
                ((InfStorage)obj).SerializeData(writer);
            };
            s_deserializeDataDelayedAction = delegate (object obj, BlobReader reader)
            {
                ((InfStorage)obj).DeserializeData(reader);
            };
        }
    }
}
