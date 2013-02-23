using System;
using System.Linq;

using Server.Crafting;

namespace Server.Items
{
    public abstract class BaseIngot : Item, IResource, ICommodity
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

        public static String ResourceGroupName { get { return "Metal"; } }
        public String ResourceGroup { get { return ResourceGroupName; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public ResourceString Resource
        {
            get { return new ResourceString(ResourceGroup, _info.Name); }
            set { SetResource = value.Value; }
        }

        private String SetResource { set { Info = Resources.LookupResource(ResourceGroup, value); } }

        public BaseIngot(String resource) : this(resource, 1) { }

        public BaseIngot(String resource, Int32 amount) : base(0x1BF2)
        {
            SetResource = resource;
            Stackable = true;
            Amount = amount;
            Hue = _info.Hue;
        }

        public BaseIngot(Serial serial) : base(serial)
        {
        }

        public override Double DefaultWeight { get { return 0.1; } }
        public override Int32 LabelNumber { get { return _info.Label - 161; } }
        Int32 ICommodity.DescriptionNumber { get { return this.LabelNumber; } }
        Boolean ICommodity.IsDeedable { get { return true; } }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(1050039, "{0}\t#{1}", this.Amount, 1027154); // ~1_NUMBER~ ~2_ITEMNAME~
            else
                list.Add(1027154); // ingots
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

            writer.Write((Int32)2); // version

            writer.Write((String)_info.Name);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            Int32 version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    SetResource = reader.ReadString();

                    break;
                case 1:
                    SetResource = "Iron";

                    switch (reader.ReadInt())
                    {
                        case 2:
                            SetResource = "Dull Copper";
                            break;
                        case 3:
                            SetResource = "Shadow Iron";
                            break;
                        case 4:
                            SetResource = "Copper";
                            break;
                        case 5:
                            SetResource = "Bronze";
                            break;
                        case 6:
                            SetResource = "Golden";
                            break;
                        case 7:
                            SetResource = "Agapite";
                            break;
                        case 8:
                            SetResource = "Verite";
                            break;
                        case 9:
                            SetResource = "Valorite";
                            break;
                    }

                    break;
                case 0:
                    SetResource = "Iron";

                    switch (reader.ReadInt())
                    {
                        case 1:
                            SetResource = "Dull Copper";
                            break;
                        case 2:
                            SetResource = "Shadow Iron";
                            break;
                        case 3:
                            SetResource = "Copper";
                            break;
                        case 4:
                            SetResource = "Bronze";
                            break;
                        case 5:
                            SetResource = "Golden";
                            break;
                        case 6:
                            SetResource = "Agapite";
                            break;
                        case 7:
                            SetResource = "Verite";
                            break;
                        case 8:
                            SetResource = "Valorite";
                            break;
                    }

                    break;
            }
        }
    }
}