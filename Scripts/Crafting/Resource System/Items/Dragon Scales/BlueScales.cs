using System;

namespace Server.Items
{
    public class BlueScales : BaseScales
    {
        [Constructable]
        public BlueScales() : this(1)
        {
        }

        [Constructable]
        public BlueScales(Int32 amount) : base("Blue Scales", amount)
        {
        }

        public BlueScales(Serial serial) : base(serial)
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
