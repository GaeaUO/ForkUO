using System;

namespace Server.Items
{
    public class AshBoard : Board
    {
        [Constructable]
        public AshBoard() : this(1)
        {
        }

        [Constructable]
        public AshBoard(int amount) : base("Ash", amount)
        {
        }

        public AshBoard(Serial serial) : base(serial)
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
