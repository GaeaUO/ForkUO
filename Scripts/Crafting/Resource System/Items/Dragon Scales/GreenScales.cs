using System;

namespace Server.Items
{
    public class GreenScales : BaseScales
    {
        [Constructable]
        public GreenScales() : this(1)
        {
        }

        [Constructable]
        public GreenScales(Int32 amount) : base("Green Scales", amount)
        {
        }

        public GreenScales(Serial serial) : base(serial)
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
