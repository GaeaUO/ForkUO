using System;

namespace Server.Items
{
    public class RedScales : BaseScales
    {
        [Constructable]
        public RedScales() : this(1)
        {
        }

        [Constructable]
        public RedScales(Int32 amount) : base("Red Scales", amount)
        {
        }

        public RedScales(Serial serial) : base(serial)
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
