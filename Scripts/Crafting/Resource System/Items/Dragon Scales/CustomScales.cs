using System;

namespace Server.Items
{
    public class CustomScales : BaseScales, ICustomItem
    {
        [Constructable]
        public CustomScales(String resource) : this(resource, 1)
        {
        }

        [Constructable]
        public CustomScales(String resource, Int32 amount) : base(resource, amount)
        {
        }

        public CustomScales(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((Int32)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            Int32 version = reader.ReadInt();
        }
    }
}
