using System;

namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class HornedHides : BaseHides
    {
        [Constructable]
        public HornedHides() : this(1)
        {
        }

        [Constructable]
        public HornedHides(int amount) : base("Horned", amount)
        {
        }

        public HornedHides(Serial serial) : base(serial)
        {
        }

        public override BaseLeather GetLeather()
        {
            return new HornedLeather();
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