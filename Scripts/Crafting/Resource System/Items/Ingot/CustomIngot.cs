using System;

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class CustomIngot : BaseIngot, ICustomItem
    {
        [Constructable]
        public CustomIngot(String resource) : this(resource, 1)
        {
        }

        [Constructable]
        public CustomIngot(String resource, Int32 amount) : base(resource, amount)
        {
        }

        public CustomIngot(Serial serial) : base(serial)
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
