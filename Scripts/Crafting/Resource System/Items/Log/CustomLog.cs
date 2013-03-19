using System;

namespace Server.Items
{
    public class CustomLog : Log, ICustomItem
    {
        [Constructable]
        public CustomLog(String resource) : this(resource, 1)
        {
        }

        [Constructable]
        public CustomLog(String resource, int amount) : base(resource, amount)
        {
        }

        public CustomLog(Serial serial) : base(serial)
        {
        }

        public override Board GetBoard()
        {
            return new CustomBoard(Info.ResourceName);
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