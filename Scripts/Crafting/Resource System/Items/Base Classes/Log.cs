using System;
using System.Linq;

using Server.Resources;

namespace Server.Items
{
    [FlipableAttribute(0x1bdd, 0x1be0)]
    public class Log : Item, IResource, ICommodity, IAxe
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
        public Log() : this("Normal", 1) { }

        [Constructable]
        public Log(Int32 amount) : this("Normal", amount) { }

        [Constructable]
        public Log(String resource) : this(resource, 1) { }

        [Constructable]
        public Log(String resource, Int32 amount) : base(0x1BDD)
        {
            SetResource = resource;

            Stackable = true;
            Weight = 2.0;
            Amount = amount;
            Hue = _info.Hue;
        }

        public Log(Serial serial) : base(serial)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(1050039, "{0}\t#{1}", this.Amount, 1027134); // ~1_NUMBER~ ~2_ITEMNAME~
            else
                list.Add(1027133); // logs
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

        public virtual Board GetBoard()
        {
            return new Board();
        }

        public bool TryCreateBoards(Mobile from, double skill, Item item)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;
            else if (from.Skills.Carpentry.Value < skill && from.Skills.Lumberjacking.Value < skill)
            {
                item.Delete();
                from.SendLocalizedMessage(1072652); // You cannot work this strange and unusual wood.
                return false;
            }

            base.ScissorHelper(from, item, 1, false);
            return true;
        }

        public bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, _info.MinimumSkillRequired, GetBoard()))
                return false;

            return true;
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
                    _PreloadResource = new object[] { "OR", reader.ReadInt() };

                    break;
                case 0:
                    _PreloadResource = "Normal";
                    break;
            }
        }
    }
}
