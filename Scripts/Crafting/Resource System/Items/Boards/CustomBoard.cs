using System;

namespace Server.Items
{
    public class CustomBoard : Board, ICustomItem
    {
        [Constructable]
        public CustomBoard(String resource) : this(resource, 1)
        {
        }

        [Constructable]
        public CustomBoard(String resource, int amount) : base(resource, amount)
        {
        }

        public CustomBoard(Serial serial) : base(serial)
        {
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
