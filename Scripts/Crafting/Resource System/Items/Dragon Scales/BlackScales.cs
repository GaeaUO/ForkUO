using System;

namespace Server.Items
{
    public class BlackScales : BaseScales
    {
        [Constructable]
        public BlackScales() : this(1)
        {
        }

        [Constructable]
        public BlackScales(Int32 amount) : base("Black Scales", amount)
        {
        }

        public BlackScales(Serial serial) : base(serial)
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
