using System;
using System.Linq;

using Server.Crafting;

namespace Server.Items
{
    public abstract class BaseScales : Item, IResource, ICommodity
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

        public static String ResourceGroupName { get { return "Scales"; } }
        public String ResourceGroup { get { return ResourceGroupName; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public ResourceString Resource
        {
            get { return new ResourceString(ResourceGroup, _info.Name); }
            set { SetResource = value.Value; }
        }

        private String SetResource { set { Info = Resources.LookupResource(ResourceGroup, value); } }

        public BaseScales(String resource) : this(resource, 1) { }

        public BaseScales(String resource, Int32 amount) : base(0x26B4)
        {
            SetResource = resource;
            Stackable = true;
            Amount = amount;
            Hue = _info.Hue;
        }

        public BaseScales(Serial serial) : base(serial)
        {
        }

        public override Double DefaultWeight { get { return 0.1; } }
        public override Int32 LabelNumber { get { return 1053139; } }
        Int32 ICommodity.DescriptionNumber { get { return _info.Number; } }
        Boolean ICommodity.IsDeedable { get { return true; } }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(1050039, "{0}\t#{1}", this.Amount, 1053139); // ~1_NUMBER~ ~2_ITEMNAME~
            else
                list.Add(1053112); // dragon scales
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (_info.Hue != 0x0)
            {
                if (_info.Number > 0)
                    list.Add(_info.Number);
                else
                    list.Add(_info.Name);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((Int32)1); // version

            writer.Write((String)_info.Name);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            Int32 version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    SetResource = reader.ReadString();

                    break;
                case 0:
                    Info = Resources.ConvertResource(reader.ReadInt());

                    break;
            }
        }
    }
}