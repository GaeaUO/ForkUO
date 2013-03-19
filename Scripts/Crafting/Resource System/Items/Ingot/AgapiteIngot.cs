using System;

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class AgapiteIngot : BaseIngot
    {
        [Constructable]
        public AgapiteIngot() : this(1)
        {
        }

        [Constructable]
        public AgapiteIngot(Int32 amount) : base("Agapite", amount)
        {
        }

        public AgapiteIngot(Serial serial) : base(serial)
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
