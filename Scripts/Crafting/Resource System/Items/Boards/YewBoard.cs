using System;

namespace Server.Items
{
    public class YewBoard : Board
    {
        [Constructable]
        public YewBoard() : this(1)
        {
        }

        [Constructable]
        public YewBoard(int amount) : base("Yew", amount)
        {
        }

        public YewBoard(Serial serial) : base(serial)
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
