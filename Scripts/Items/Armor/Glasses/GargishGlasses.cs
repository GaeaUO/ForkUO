using System;

namespace Server.Items
{
    [FlipableAttribute(0x4644, 0x4645)]     
    public class GargishGlasses : Glasses
    {
        [Constructable]
        public AnniversaryRobe() : this(0x4644)
        {
        }        
        public GargishGlasses() : base()
        {
        }

        public GargishGlasses(Serial serial) : base(serial)
        {
        }

        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

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