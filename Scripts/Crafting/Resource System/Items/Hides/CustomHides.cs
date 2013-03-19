using System;

namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class CustomHides : BaseHides, ICustomItem
    {
        [Constructable]
        public CustomHides(String resource) : this(resource, 1)
        {
        }

        [Constructable]
        public CustomHides(String resource, int amount) : base(resource, amount)
        {
        }

        public CustomHides(Serial serial) : base(serial)
        {
        }

        public override BaseLeather GetLeather()
        {
            return new CustomLeather(Info.ResourceName);
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
