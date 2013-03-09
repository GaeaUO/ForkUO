using System;

namespace Server.Items
{
    public class BloodwoodLog : Log
    {
        [Constructable]
        public BloodwoodLog() : this(1)
        {
        }

        [Constructable]
        public BloodwoodLog(int amount) : base("Bloodwood", amount)
        {
        }

        public BloodwoodLog(Serial serial) : base(serial)
        {
        }

        public override Board GetBoard()
        {
            return new BloodwoodBoard();
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