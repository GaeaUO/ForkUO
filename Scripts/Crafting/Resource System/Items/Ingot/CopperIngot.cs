using System;

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class CopperIngot : BaseIngot
    {
        [Constructable]
        public CopperIngot() : this(1)
        {
        }

        [Constructable]
        public CopperIngot(Int32 amount) : base("Copper", amount)
        {
        }

        public CopperIngot(Serial serial) : base(serial)
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
