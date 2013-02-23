using System;

using Server.Items;

namespace Server.Crafting.Harvest
{
    public class Vein
    {
        private ResourceInfo _info;
        public ResourceInfo Info { get { return _info; } }

        private Double _defaultChance = 0.5;
        public Double DefaultChance { get { return _defaultChance; } }

        private Int32 _quantity = 0;
        public Int32 Quantity { get { return _quantity; } }

        private Int32 _partialQuantity = 0;
        public Int32 PartialQuantity { get { return _partialQuantity; } }

        private Int32 _remaining = 0;
        public Int32 Remaining { get { return _remaining; } }

        private DateTime _respawnTime = DateTime.Now;
        public DateTime RespawnTime { get { return RespawnTime; } }

        private Int32 _respawnPeriod = 10;
        public Int32 RespawnPeriod { get { return _respawnPeriod; } }

        public Vein(ResourceInfo info, Int32 quantity, Int32 respawnPeriod)
        {
            _info = info;
            _defaultChance = Utility.RandomDouble();
            _quantity = quantity;
            _partialQuantity = (Utility.RandomDouble() > 0.25 ? 1 : 0) + (Utility.RandomDouble() > 0.5 ? 1 : 0) + (Utility.RandomDouble() > 0.75 ? 1 : 0);

            if (Utility.RandomDouble() < 0.05)
                _quantity = _quantity / 2;
            else if (Utility.RandomDouble() > 0.99)
                _quantity = _quantity * 3;
            else if (Utility.RandomDouble() > 0.95)
                _quantity = _quantity * 2;

            _remaining = _quantity;
            _respawnPeriod = respawnPeriod;
        }

        public IResource Consume(Boolean mutate, Boolean FeluccaRules, Race race)
        {
            if (!CheckRespawn)
                return null;

            ResourceInfo info = (!mutate ? _info : Resources.Mutate("BLAH BLAH", _info.Name));

            if (Utility.RandomDouble() <= _defaultChance - (race == Race.Elf ? 0.03 : 0))
                info = Resources.DefaultResource("BLAH BLAH");

            Type t = (info != null ? info.RawResource : null);

            IResource resource = null;

            if (t != null)
            {
                try { resource = Activator.CreateInstance(t) as IResource; }
                catch { }
            }

            if (resource != null)
            {
                if (resource is Item)
                {
                    Item item = (Item)resource;
                    item.Amount = (FeluccaRules ? 2 : 1);

                    if (item is BaseOre)
                    {
                        if (_remaining == 1 && _partialQuantity > 0)
                            ((BaseOre)item).Resize(OreSize.Small);
                        else if (_remaining <= _partialQuantity)
                            ((BaseOre)item).Resize(OreSize.Medium);
                    }
                }

                _remaining--;
            }

            return resource;
        }

        private Boolean CheckRespawn
        {
            get
            {
                if (_remaining == _quantity)
                {
                    _respawnTime = DateTime.Now.AddMinutes(_respawnPeriod);
                }
                else if (_respawnTime < DateTime.Now)
                {
                    _respawnTime = DateTime.Now.AddMinutes(_respawnPeriod);
                    _remaining = _quantity;

                    if (1 == 1) // TO DO - Randomize check
                        _info = Resources.RandomResource("BLAH BLAH");
                }

                return _remaining > 0;
            }
        }

        public Vein(GenericReader reader)
        {
            Deserialize(reader);
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0); // version

            writer.Write(_info.Name);
            writer.Write(_defaultChance);
            writer.Write(_quantity);
            writer.Write(_partialQuantity);
            writer.Write(_respawnPeriod);
        }

        private void Deserialize(GenericReader reader)
        {
            Int32 version = reader.ReadInt();

            switch(version)
            {
                case 0:
                    _info = Resources.LookupResource("BLAH BLAH", reader.ReadString());
                    _defaultChance = reader.ReadDouble();
                    _quantity = reader.ReadInt();
                    _partialQuantity = reader.ReadInt();
                    _respawnPeriod = reader.ReadInt();
                    
                    break;
            }

            _remaining = _quantity;
        }
    }
}
