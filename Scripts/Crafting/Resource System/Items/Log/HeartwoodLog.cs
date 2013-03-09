using System;

namespace Server.Items
{
    public class HeartwoodLog : Log
    {
        [Constructable]
        public HeartwoodLog() : this(1)
        {
        }

        [Constructable]
        public HeartwoodLog(int amount) : base("Heartwood", amount)
        {
        }

        public HeartwoodLog(Serial serial) : base(serial)
        {
        }

        public override Board GetBoard()
        {
            return new HeartwoodBoard();
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