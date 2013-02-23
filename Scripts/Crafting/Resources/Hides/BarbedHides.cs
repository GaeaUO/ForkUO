using System;

namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class BarbedHides : BaseHides
    {
        [Constructable]
        public BarbedHides() : this(1)
        {
        }

        [Constructable]
        public BarbedHides(int amount) : base("Barbed", amount)
        {
        }

        public BarbedHides(Serial serial) : base(serial)
        {
        }

        public override BaseLeather GetLeather()
        {
            return new BarbedLeather();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}