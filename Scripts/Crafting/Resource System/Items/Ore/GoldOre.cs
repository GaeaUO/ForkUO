using System;

namespace Server.Items
{
    public class GoldOre : BaseOre
    {
        [Constructable]
        public GoldOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public GoldOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public GoldOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public GoldOre(Int32 amount, OreSize size) : base("Golden", amount, size)
        {
        }

        public GoldOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new GoldIngot();
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
