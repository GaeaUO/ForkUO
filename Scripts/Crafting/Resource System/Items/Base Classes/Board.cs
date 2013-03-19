using System;
using System.Linq;

using Server.Resources;

namespace Server.Items
{
    [FlipableAttribute(0x1BD7, 0x1BDA)]
    public class Board : Item, IResource, ICommodity
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

        public static String ResourceGroupName { get { return "Wood"; } }
        public String ResourceGroup { get { return ResourceGroupName; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public ResourceString Resource
        {
            get { return new ResourceString(ResourceGroup, _info.ResourceName); }
            set { SetResource = value.Value; }
        }

        private String SetResource { set { Info = ResourceService.Service.LookupResource(ResourceGroup, value); } }

        [Constructable]
        public Board() : this("Normal", 1) { }

        [Constructable]
        public Board(Int32 amount) : this("Normal", amount) { }

        [Constructable]
        public Board(String resource) : this(resource, 1) { }

        [Constructable]
        public Board(String resource, Int32 amount) : base(0x1BD7)
        {
            SetResource = resource;

            Stackable = true;
            this.Weight = 2.0;
            Amount = amount;
            Hue = _info.Hue;
        }

        public Board(Serial serial) : base(serial)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(1050039, "{0}\t#{1}", this.Amount, 1027128); // ~1_NUMBER~ ~2_ITEMNAME~
            else
                list.Add(1027127); // boards
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (_info.Hue != 0x0)
            {
                if (_info.ResourceNumber > 0)
                    list.Add(_info.ResourceNumber);
                else
                    list.Add(_info.ResourceName);
            }
        }

        public override Int32 LabelNumber { get { return _info.ProcessedNumber; } }
        Int32 ICommodity.DescriptionNumber { get { return this.LabelNumber; } }
        Boolean ICommodity.IsDeedable { get { return true; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)4); // version

            writer.Write((String)_info.ResourceName);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 4:
                    _PreloadResource = reader.ReadString();

                    break;
                case 3:
                case 2:
                    _PreloadResource = new object[] { "OR", reader.ReadInt() };

                    break;
                case 1:
                case 0:
                    _PreloadResource = "Normal";
                    break;
            }
        }
    }
}