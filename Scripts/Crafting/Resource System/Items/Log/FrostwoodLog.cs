using System;

namespace Server.Items
{
    public class FrostwoodLog : Log
    {
        [Constructable]
        public FrostwoodLog() : this(1)
        {
        }

        [Constructable]
        public FrostwoodLog(int amount) : base("Frostwood", amount)
        {
        }

        public FrostwoodLog(Serial serial) : base(serial)
        {
        }

        public override Board GetBoard()
        {
            return new FrostwoodBoard();
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