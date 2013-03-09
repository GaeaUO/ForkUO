using System;

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DullCopperIngot : BaseIngot
    {
        [Constructable]
        public DullCopperIngot() : this(1)
        {
        }

        [Constructable]
        public DullCopperIngot(Int32 amount) : base("Dull Copper", amount)
        {
        }

        public DullCopperIngot(Serial serial) : base(serial)
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
