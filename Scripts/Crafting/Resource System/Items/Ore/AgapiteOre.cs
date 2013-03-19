using System;

namespace Server.Items
{
    public class AgapiteOre : BaseOre
    {
        [Constructable]
        public AgapiteOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public AgapiteOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public AgapiteOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public AgapiteOre(Int32 amount, OreSize size) : base("Agapite", amount, size)
        {
        }

        public AgapiteOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new AgapiteIngot();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((Int32)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            Int32 version = reader.ReadInt();
        }
    }
}
