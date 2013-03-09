using System;

namespace Server.Items
{
    public class WhiteScales : BaseScales
    {
        [Constructable]
        public WhiteScales() : this(1)
        {
        }

        [Constructable]
        public WhiteScales(Int32 amount) : base("White Scales", amount)
        {
        }

        public WhiteScales(Serial serial) : base(serial)
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
