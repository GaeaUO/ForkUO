using System;

namespace Server.Items
{
    public class BronzeOre : BaseOre
    {
        [Constructable]
        public BronzeOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public BronzeOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public BronzeOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public BronzeOre(Int32 amount, OreSize size) : base("Bronze", amount, size)
        {
        }

        public BronzeOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new BronzeIngot();
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
