using System;

namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class Hides : BaseHides
    {
        [Constructable]
        public Hides() : this(1)
        {
        }

        [Constructable]
        public Hides(int amount) : base("Normal", amount)
        {
        }

        public Hides(Serial serial) : base(serial)
        {
        }

        public override BaseLeather GetLeather()
        {
            return new Leather();
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
