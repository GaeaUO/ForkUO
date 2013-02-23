using System;
using System.Linq;

using Server.Crafting;

namespace Server.Items
{
    public abstract class BaseHides : Item, IResource, ICommodity, IScissorable
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

        public static String ResourceGroupName { get { return "Leather"; } }
        public String ResourceGroup { get { return ResourceGroupName; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public ResourceString Resource
        {
            get { return new ResourceString(ResourceGroup, _info.Name); }
            set { SetResource = value.Value; }
        }

        private String SetResource { set { Info = Resources.LookupResource(ResourceGroup, value); } }

        public BaseHides(String resource) : this(resource, 1) { }

        public BaseHides(String resource, Int32 amount) : base(0x1079)
        {
            SetResource = resource;

            Stackable = true;
            Weight = 5.0;
            Amount = amount;
            Hue = _info.Hue;
        }

        public BaseHides(Serial serial) : base(serial)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(1050039, "{0}\t#{1}", this.Amount, 1047023); // ~1_NUMBER~ ~2_ITEMNAME~
            else
                list.Add(1024216); // pile of hides
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

        public override Int32 LabelNumber { get { return _info.Label; } }
        Int32 ICommodity.DescriptionNumber { get { return this.LabelNumber; } }
        Boolean ICommodity.IsDeedable { get { return true; } }

        public abstract BaseLeather GetLeather();

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, GetLeather(), 1);

            return true;
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
                    SetResource = "Normal";

                    switch (reader.ReadInt())
                    {
                        case 102:
                            SetResource = "Spined";
                            break;
                        case 103:
                            SetResource = "Horned";
                            break;
                        case 104:
                            SetResource = "Barbed";
                            break;
                    }

                    break;
                case 0:
                    Int32 level = reader.ReadInt();
                    Int32 hue = reader.ReadInt();
                    String oiName = reader.ReadString();

                    SetResource = "Normal";

                    if (oiName.IndexOf("Spined") >= 0)
                        SetResource = "Spined";
                    else if (oiName.IndexOf("Horned") >= 0)
                        SetResource = "Horned";
                    else if (oiName.IndexOf("Barbed") >= 0)
                        SetResource = "Barbed";

                    break;
            }
        }
    }
}