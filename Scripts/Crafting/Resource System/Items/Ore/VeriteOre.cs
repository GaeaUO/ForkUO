using System;

namespace Server.Items
{
    public class VeriteOre : BaseOre
    {
        [Constructable]
        public VeriteOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public VeriteOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public VeriteOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public VeriteOre(Int32 amount, OreSize size) : base("Verite", amount, size)
        {
        }

        public VeriteOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new VeriteIngot();
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
