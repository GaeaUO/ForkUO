using System;

namespace Server.Items
{
    public class FrostwoodBoard : Board
    {
        [Constructable]
        public FrostwoodBoard() : this(1)
        {
        }

        [Constructable]
        public FrostwoodBoard(int amount) : base("Frostwood", amount)
        {
        }

        public FrostwoodBoard(Serial serial) : base(serial)
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
