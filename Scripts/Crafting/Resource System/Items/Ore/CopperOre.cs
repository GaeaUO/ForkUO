using System;

namespace Server.Items
{
    public class CopperOre : BaseOre
    {
        [Constructable]
        public CopperOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public CopperOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public CopperOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public CopperOre(Int32 amount, OreSize size) : base("Copper", amount, size)
        {
        }

        public CopperOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new CopperIngot();
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
