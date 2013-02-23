using System;

namespace Server.Items
{
    public class YewLog : Log
    {
        [Constructable]
        public YewLog() : this(1)
        {
        }

        [Constructable]
        public YewLog(int amount) : base("Yew", amount)
        {
        }

        public YewLog(Serial serial) : base(serial)
        {
        }

        public override Board GetBoard()
        {
            return new YewBoard();
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