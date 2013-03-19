using System;

namespace Server.Items
{
    public class ValoriteOre : BaseOre
    {
        [Constructable]
        public ValoriteOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public ValoriteOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public ValoriteOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public ValoriteOre(Int32 amount, OreSize size) : base("Valorite", amount, size)
        {
        }

        public ValoriteOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new ValoriteIngot();
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
