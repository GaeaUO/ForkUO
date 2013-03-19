using System;
using System.Linq;

using Server.Resources;

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

        private object _PreloadResource = null;
        public object PreloadResource { get { return _PreloadResource; } }

        public static String ResourceGroupName { get { return "Leather"; } }
        public String ResourceGroup { get { return ResourceGroupName; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public ResourceString Resource
        {
            get { return new ResourceString(ResourceGroup, _info.ResourceName); }
            set { SetResource = value.Value; }
        }

        private String SetResource { set { Info = ResourceService.Service.LookupResource(ResourceGroup, value); } }

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
                if (_info.ResourceNumber > 0)
                    list.Add(_info.ResourceNumber);
                else
                    list.Add(_info.ResourceName);
            }
        }

        public override Int32 LabelNumber { get { return _info.RawNumber; } }
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

            writer.Write((String)_info.ResourceName);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            Int32 version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    _PreloadResource = reader.ReadString();

                    break;
                case 1:
                    _PreloadResource = new object[] { "OR", reader.ReadInt() };

                    break;
                case 0:
                    Int32 level = reader.ReadInt();
                    Int32 hue = reader.ReadInt();

                    _PreloadResource = new object[] { "OI", reader.ReadString() };

                    break;
            }
        }
    }
}