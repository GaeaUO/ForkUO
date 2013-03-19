﻿using System;

namespace Server.Items
{
    public class ShadowIronOre : BaseOre
    {
        [Constructable]
        public ShadowIronOre() : this(1, OreSize.Large)
        {
        }

        [Constructable]
        public ShadowIronOre(Int32 amount) : this(amount, OreSize.Large)
        {
        }

        [Constructable]
        public ShadowIronOre(OreSize size) : this(1, size)
        {
        }

        [Constructable]
        public ShadowIronOre(Int32 amount, OreSize size) : base("Shadow Iron", amount, size)
        {
        }

        public ShadowIronOre(Serial serial) : base(serial)
        {
        }

        public override BaseIngot GetIngot()
        {
            return new ShadowIronIngot();
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
