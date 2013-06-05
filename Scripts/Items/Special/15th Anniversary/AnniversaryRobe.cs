using System;

namespace Server.Items
{
    [FlipableAttribute(0x4B9D, 0x4B9E)]
    public class AnniversaryRobe : BaseOuterTorso
    {
        [Constructable]
        public AnniversaryRobe() : this( 0x4B9D )
        {
            this.Name = "15th Anniversary Commemorative Robe";
            this.LootType = LootType.Blessed;
            this.Weight = 1.0;
        }

        public AnniversaryRobe(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}