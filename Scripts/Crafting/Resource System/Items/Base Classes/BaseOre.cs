using System;
using System.Collections;
using System.Linq;

using Server.Resources;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public abstract class BaseOre : Item, IResource
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

        private OreSize _size = OreSize.Large;
        public OreSize Size { get { return _size; } }

        public static String ResourceGroupName { get { return "Metal"; } }
        public String ResourceGroup { get { return ResourceGroupName; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public ResourceString Resource
        {
            get { return new ResourceString(ResourceGroup, _info.ResourceName); }
            set { SetResource = value.Value; }
        }

        private String SetResource { set { Info = ResourceService.Service.LookupResource(ResourceGroup, value); } }

        public BaseOre(String resource) : this(resource, 1, OreSize.Large) { }
        public BaseOre(String resource, Int32 amount) : this(resource, amount, OreSize.Large) { }
        public BaseOre(String resource, OreSize size) : this(resource, 1, size) { }

        public BaseOre(String resource, Int32 amount, OreSize size)
        {
            SetResource = resource;
            _size = size;

            Stackable = true;
            Amount = amount;
            Hue = _info.Hue;

            switch (size)
            {
                case OreSize.Large:
                ItemID = 0x19B9;
                    break;
                case OreSize.Medium:
                ItemID = 0x19B8 + (Utility.RandomBool() ? 2 : 0);
                    break;
                case OreSize.Small:
                ItemID = 0x19B7;
                    break;
            }
        }

        public BaseOre(Serial serial) : base(serial)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(1050039, "{0}\t#{1}", this.Amount, 1026583); // ~1_NUMBER~ ~2_ITEMNAME~
            else
                list.Add(1026583); // ore
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

        public abstract BaseIngot GetIngot();

        public override void OnDoubleClick(Mobile from)
        {
            if (!this.Movable)
                return;

            if (this.RootParent is BaseCreature)
            {
                from.SendLocalizedMessage(500447); // That is not accessible
            }
            else if (from.InRange(this.GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(501971); // Select the forge on which to smelt the ore, or another pile of ore with which to combine it.
                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(501976); // The ore is too far away.
            }
        }

        public void Resize(OreSize size)
        {
            _size = size;

            switch (size)
            {
                case OreSize.Large:
                    ItemID = 0x19B9;
                    break;
                case OreSize.Medium:
                    ItemID = 0x19B8 + (Utility.RandomBool() ? 2 : 0);
                    break;
                case OreSize.Small:
                    ItemID = 0x19B7;
                    break;
            }
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
                    _PreloadResource = new object[] { "OI", reader.ReadInt() };

                    break;
            }

            if (version < 2)
            {
                if (ItemID == 0x19B9)
                    _size = OreSize.Large;
                else if (ItemID == 0x19B7)
                    _size = OreSize.Small;
                else
                    _size = OreSize.Medium;
            }
        }

        #region InternalTarget
        private class InternalTarget : Target
        {
            private readonly BaseOre _Ore;

            public InternalTarget(BaseOre ore)
                : base(2, false, TargetFlags.None)
            {
                _Ore = ore;
            }

            private bool IsForge(object obj)
            {
                if (Core.ML && obj is Mobile && ((Mobile)obj).IsDeadBondedPet)
                    return false;

                if (obj.GetType().IsDefined(typeof(ForgeAttribute), false)) // TO DO : MOVE ForgeAttribute
                    return true;

                Int32 itemID = 0;

                if (obj is Item)
                    itemID = ((Item)obj).ItemID;
                else if (obj is StaticTarget)
                    itemID = ((StaticTarget)obj).ItemID;

                return (itemID == 4017 || (itemID >= 6522 && itemID <= 6569));
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (_Ore.Deleted)
                    return;

                if (!from.InRange(_Ore.GetWorldLocation(), 2))
                {
                    from.SendLocalizedMessage(501976); // The ore is too far away.
                    return;
                }

                #region Combine Ore
                if (targeted is BaseOre)
                {
                    BaseOre ore = (BaseOre)targeted;

                    if (!ore.Movable)
                    {
                        return;
                    }
                    else if (_Ore == ore)
                    {
                        from.SendLocalizedMessage(501972); // Select another pile or ore with which to combine this.
                        from.Target = new InternalTarget(ore);
                        return;
                    }
                    else if (ore.Resource != _Ore.Resource)
                    {
                        from.SendLocalizedMessage(501979); // You cannot combine ores of different metals.
                        return;
                    }

                    Int32 worth = ore.Amount;

                    if (ore.Size == OreSize.Large)
                        worth *= 8;
                    else if (ore.Size == OreSize.Medium)
                        worth *= 4;
                    else
                        worth *= 2;

                    Int32 sourceWorth = _Ore.Amount;

                    if (_Ore.Size == OreSize.Large)
                        sourceWorth *= 8;
                    else if (_Ore.Size == OreSize.Medium)
                        sourceWorth *= 4;
                    else
                        sourceWorth *= 2;

                    worth += sourceWorth;

                    Int32 plusWeight = 0;
                    Int32 newID = ore.ItemID;

                    if (ore.DefaultWeight != _Ore.DefaultWeight)
                    {
                        if (ore.ItemID == 0x19B7 || _Ore.ItemID == 0x19B7)
                        {
                            newID = 0x19B7;
                        }
                        else if (ore.ItemID == 0x19B9)
                        {
                            newID = _Ore.ItemID;
                            plusWeight = ore.Amount * 2;
                        }
                        else
                        {
                            plusWeight = _Ore.Amount * 2;
                        }
                    }

                    if ((ore.ItemID == 0x19B9 && worth > 120000) || ((ore.ItemID == 0x19B8 || ore.ItemID == 0x19BA) && worth > 60000) || (ore.ItemID == 0x19B7 && worth > 30000))
                    {
                        from.SendLocalizedMessage(1062844); // There is too much ore to combine.
                        return;
                    }
                    else if (ore.RootParent is Mobile && (plusWeight + ((Mobile)ore.RootParent).Backpack.TotalWeight) > ((Mobile)ore.RootParent).Backpack.MaxWeight)
                    {
                        from.SendLocalizedMessage(501978); // The weight is too great to combine in a container.
                        return;
                    }

                    ore.ItemID = newID;

                    if (ore.ItemID == 0x19B9)
                        ore.Amount = worth / 8;
                    else if (ore.ItemID == 0x19B7)
                        ore.Amount = worth / 2;
                    else
                        ore.Amount = worth / 4;

                    _Ore.Delete();
                    return;
                }
                #endregion

                #region Smelting
                if (this.IsForge(targeted))
                {
                    double difficulty = _Ore.Info.MinimumSkillRequired;

                    double minSkill = difficulty - 25.0;
                    double maxSkill = difficulty + 25.0;

                    if (difficulty > 50.0 && difficulty > from.Skills[SkillName.Mining].Value)
                    {
                        from.SendLocalizedMessage(501986); // You have no idea how to smelt this strange ore!
                        return;
                    }

                    if (_Ore.ItemID == 0x19B7 && _Ore.Amount < 2)
                    {
                        from.SendLocalizedMessage(501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        return;
                    }

                    if (from.CheckTargetSkill(SkillName.Mining, targeted, minSkill, maxSkill))
                    {
                        Int32 toConsume = _Ore.Amount;

                        if (toConsume <= 0)
                        {
                            from.SendLocalizedMessage(501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        }
                        else
                        {
                            if (toConsume > 30000)
                                toConsume = 30000;

                            Int32 ingotAmount;

                            if (_Ore.ItemID == 0x19B7)
                            {
                                ingotAmount = toConsume / 2;

                                if (toConsume % 2 != 0)
                                    --toConsume;
                            }
                            else if (_Ore.ItemID == 0x19B9)
                            {
                                ingotAmount = toConsume * 2;
                            }
                            else
                            {
                                ingotAmount = toConsume;
                            }

                            BaseIngot ingot = _Ore.GetIngot();
                            ingot.Amount = ingotAmount;

                            _Ore.Consume(toConsume);
                            from.AddToBackpack(ingot);

                            from.SendLocalizedMessage(501988); // You smelt the ore removing the impurities and put the metal in your backpack.
                        }
                    }
                    else
                    {
                        if (_Ore.Amount < 2)
                        {
                            if (_Ore.ItemID == 0x19B9)
                                _Ore.ItemID = 0x19B8;
                            else
                                _Ore.ItemID = 0x19B7;
                        }
                        else
                        {
                            _Ore.Amount /= 2;
                        }

                        from.SendLocalizedMessage(501990); // You burn away the impurities but are left with less useable metal.
                    }
                }
                #endregion
            }
        }
        #endregion
    }
}
