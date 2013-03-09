using System;

namespace Server.Items
{
    public class IronOre : BaseOre
    {
        [Constructable]
        public IronOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public IronOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public IronOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public IronOre(Int32 amount, OreSize size) : base("Iron", amount, size)
        {
        }

        public IronOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new IronIngot();
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
