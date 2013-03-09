using System;
using System.Linq;

using Server.Resources;

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

        private object _PreloadResource = null;
        public object PreloadResource { get { return _PreloadResource; } }

        public static String ResourceGroupName { get { return "Sand"; } }
        public String ResourceGroup { get { return ResourceGroupName; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public ResourceString Resource
        {
            get { return new ResourceString(ResourceGroup, _info.ResourceName); }
            set { SetResource = value.Value; }
        }

        private String SetResource { set { Info = ResourceService.Service.LookupResource(ResourceGroup, value); } }

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

        public override Int32 LabelNumber { get { return _info.ResourceNumber; } }
        Int32 ICommodity.DescriptionNumber { get { return this.LabelNumber; } }
        Boolean ICommodity.IsDeedable { get { return true; } }

        public Sand(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.Write((String)_info.ResourceName);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    _PreloadResource = reader.ReadString();

                    break;
                case 1:
                    _PreloadResource = "Sand";
                    break;
                case 0:
                    if (Name == "sand")
                        Name = null;

                    _PreloadResource = "Sand";
                    break;
            }
        }
    }
}
