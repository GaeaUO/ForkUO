using System;
using System.Linq;

using Server.Crafting;

namespace Server.Items
{
    [FlipableAttribute(0x11EA, 0x11EB)]
    public class Sand : Item, IResource, ICommodity
    {
        private ResourceInfo _info;
        public ResourceInfo Info
        {
            get { return _info; }
            set
            {
                _info = value;
                Hue = _info.Hue;

                InvalidateProperties();
            }
        }

        public static String ResourceGroupName { get { return "Sand"; } }
        public String ResourceGroup { get { return ResourceGroupName; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public ResourceString Resource
        {
            get { return new ResourceString(ResourceGroup, _info.Name); }
            set { SetResource = value.Value; }
        }

        private String SetResource { set { Info = Resources.LookupResource(ResourceGroup, value); } }

        [Constructable]
        public Sand() : this(1)
        {
        }

        [Constructable]
        public Sand(int amount) : base(0x11EA)
        {
            SetResource = "Sand";

            this.Stackable = Core.ML;
            this.Weight = 1.0;
            Amount = amount;
            Hue = _info.Hue;
        }

        public override Int32 LabelNumber { get { return _info.Number; } }
        Int32 ICommodity.DescriptionNumber { get { return this.LabelNumber; } }
        Boolean ICommodity.IsDeedable { get { return true; } }

        public Sand(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.Write((String)_info.Name);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    SetResource = reader.ReadString();

                    break;
                case 1:
                    SetResource = "Sand";
                    break;
                case 0:
                    if (Name == "sand")
                        Name = null;

                    SetResource = "Sand";
                    break;
            }
        }
    }
}
