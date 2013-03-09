using System;

namespace Server.Items
{
    public class DullCopperOre : BaseOre
    {
        [Constructable]
        public DullCopperOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public DullCopperOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public DullCopperOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public DullCopperOre(Int32 amount, OreSize size) : base("Dull Copper", amount, size)
        {
        }

        public DullCopperOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new DullCopperIngot();
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
