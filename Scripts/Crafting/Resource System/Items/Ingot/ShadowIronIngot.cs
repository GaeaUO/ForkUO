using System;

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class ShadowIronIngot : BaseIngot
    {
        [Constructable]
        public ShadowIronIngot() : this(1)
        {
        }

        [Constructable]
        public ShadowIronIngot(Int32 amount) : base("Shadow Iron", amount)
        {
        }

        public ShadowIronIngot(Serial serial) : base(serial)
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
