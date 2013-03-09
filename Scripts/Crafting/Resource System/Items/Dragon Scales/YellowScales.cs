using System;

namespace Server.Items
{
    public class YellowScales : BaseScales
    {
        [Constructable]
        public YellowScales() : this(1)
        {
        }

        [Constructable]
        public YellowScales(Int32 amount) : base("Yellow Scales", amount)
        {
        }

        public YellowScales(Serial serial) : base(serial)
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
