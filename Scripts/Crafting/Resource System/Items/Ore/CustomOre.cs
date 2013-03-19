using System;

namespace Server.Items
{
    public class CustomOre : BaseOre, ICustomItem
    {
        [Constructable]
        public CustomOre(String resource) : this(resource, 1, OreSize.Large)
        {
        }

        [Constructable]
        public CustomOre(String resource, Int32 amount) : this(resource, amount, OreSize.Large)
        {
        }

        [Constructable]
        public CustomOre(String resource, OreSize size) : this(resource, 1, size)
        {
        }

        [Constructable]
        public CustomOre(String resource, Int32 amount, OreSize size) : base(resource, amount, size)
        {
        }

        public CustomOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new CustomIngot(Info.ResourceName);
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
