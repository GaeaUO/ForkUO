using System;

namespace Server.Items
{
    public class AshLog : Log
    {
        [Constructable]
        public AshLog() : this(1)
        {
        }

        [Constructable]
        public AshLog(int amount) : base("Ash", amount)
        {
        }

        public AshLog(Serial serial) : base(serial)
        {
        }

        public override Board GetBoard()
        {
            return new AshBoard();
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