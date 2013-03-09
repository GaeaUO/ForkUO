using System;

namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class SpinedHides : BaseHides
    {
        [Constructable]
        public SpinedHides() : this(1)
        {
        }

        [Constructable]
        public SpinedHides(int amount) : base("Spined", amount)
        {
        }

        public SpinedHides(Serial serial) : base(serial)
        {
        }

        public override BaseLeather GetLeather()
        {
            return new SpinedLeather();
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