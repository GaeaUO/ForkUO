using System;
using System.Collections;
using System.Reflection;

using Server.Items;

namespace Server.Resources
{
    public class ResourceInfo
    {
        private ResourceType _TypeofResource = ResourceType.Harvested;
        public ResourceType TypeofResource { get { return _TypeofResource; } }

        private object _HarvestMessage;
        public object HarvestMessage { get { return _HarvestMessage; } }

        private Int32 _ResourceNumber = 0;
        public Int32 ResourceNumber { get { return _ResourceNumber; } }

        private String _ResourceName = "Unknown Resource";
        public String ResourceName { get { return _ResourceName; } }

        private Int32 _Hue = 0x0;
        public Int32 Hue { get { return _Hue; } }

        private String _HarvestSkill;
        public String HarvestSkill { get { return _HarvestSkill; } }

        private Double _MinimumSkillRequired = 0.0;
        public Double MinimumSkillRequired { get { return _MinimumSkillRequired; } }

        private Double _HarvestChance = 100.0;
        public Double HarvestChance { get { return _HarvestChance; } }

        private CraftAttributeInfo _AttributeInfo = CraftAttributeInfo.Blank;
        public CraftAttributeInfo AttributeInfo { get { return _AttributeInfo; } }

        private Type _RawResource;
        public Type RawResource { get { return _RawResource; } }

        private Int32 _RawNumber = 0;
        public Int32 RawNumber { get { return _RawNumber; } }

        private String _RawName = "Undefined Raw Resource";
        public String RawName { get { return _RawName; } }

        private Type _ProcessedResource;
        public Type ProcessedResource { get { return _ProcessedResource; } }

        private Int32 _ProcessedNumber = 0;
        public Int32 ProcessedNumber { get { return _ProcessedNumber; } }

        private String _ProcessedName = "Undefined Processed Resource";
        public String ProcessedName { get { return _ProcessedName; } }

        private Boolean _RawResourceUseableForCrafting = false;
        public Boolean RawResourceUseableForCrafting { get { return _RawResourceUseableForCrafting; } }

        public ResourceInfo() { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, CraftAttributeInfo craftInfo, Type raw)
            : this(type, harvestMessage, resourceNumber, name, hue, null, 0.0, 100.0, craftInfo, raw, 0, "", null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, CraftAttributeInfo craftInfo, Type raw, Int32 rawNumber)
            : this(type, harvestMessage, resourceNumber, name, hue, null, 0.0, 100.0, craftInfo, raw, rawNumber, "", null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, CraftAttributeInfo craftInfo, Type raw, String rawName)
            : this(type, harvestMessage, resourceNumber, name, hue, null, 0.0, 100.0, craftInfo, raw, 0, rawName, null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, String skill, Double skillReq, Double chance, Type raw)
            : this(type, harvestMessage, resourceNumber, name, hue, skill, skillReq, chance, CraftAttributeInfo.Blank, raw, resourceNumber, "", null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, Double chance, Type raw, Int32 rawNumber)
            : this(type, harvestMessage, resourceNumber, name, hue, null, 0.0, chance, CraftAttributeInfo.Blank, raw, rawNumber, "", null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, Double chance, Type raw, String rawName)
            : this(type, harvestMessage, resourceNumber, name, hue, null, 0.0, chance, CraftAttributeInfo.Blank, raw, 0, rawName, null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, Double chance, Type raw)
            : this(type, harvestMessage, resourceNumber, name, hue, null, 0.0, chance, CraftAttributeInfo.Blank, raw, resourceNumber, "", null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, String skill, Double skillReq, Double chance, Type raw, Int32 rawNumber)
            : this(type, harvestMessage, resourceNumber, name, hue, skill, skillReq, chance, CraftAttributeInfo.Blank, raw, rawNumber, "", null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, String skill, Double skillReq, Double chance, Type raw, String rawName)
            : this(type, harvestMessage, resourceNumber, name, hue, skill, skillReq, chance, CraftAttributeInfo.Blank, raw, 0, rawName, null, 0, "", true) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, Double chance, CraftAttributeInfo craftInfo, Type raw, Int32 rawNumber, Type processed, Int32 processedNumber, Boolean useRaw)
            : this(type, harvestMessage, resourceNumber, name, hue, null, 0.0, chance, craftInfo, raw, rawNumber, "", processed, processedNumber, "", useRaw) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, Double chance, CraftAttributeInfo craftInfo, Type raw, String rawName, Type processed, String processedName, Boolean useRaw)
            : this(type, harvestMessage, resourceNumber, name, hue, null, 0.0, chance, craftInfo, raw, 0, rawName, processed, 0, processedName, useRaw) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, String skill, Double skillReq, Double chance, CraftAttributeInfo craftInfo, Type raw, Int32 rawNumber, Type processed, Int32 processedNumber, Boolean useRaw)
            : this(type, harvestMessage, resourceNumber, name, hue, skill, skillReq, chance, craftInfo, raw, rawNumber, "", processed, processedNumber, "", useRaw) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, String skill, Double skillReq, Double chance, CraftAttributeInfo craftInfo, Type raw, String rawName, Type processed, String processedName, Boolean useRaw)
            : this(type, harvestMessage, resourceNumber, name, hue, skill, skillReq, chance, craftInfo, raw, 0, rawName, processed, 0, processedName, useRaw) { }

        public ResourceInfo(ResourceType type, object harvestMessage, Int32 resourceNumber, String name, Int32 hue, String skill, Double skillReq, Double chance, CraftAttributeInfo craftInfo, Type raw, Int32 rawNumber, String rawName, Type processed, Int32 processedNumber, String processedName, Boolean useRaw)
        {
            _TypeofResource = type;
            _HarvestMessage = harvestMessage;
            _ResourceNumber = resourceNumber;
            _ResourceName = name;
            _Hue = hue;
            _HarvestSkill = skill;
            _MinimumSkillRequired = skillReq;
            _HarvestChance = chance;
            _AttributeInfo = (craftInfo != null ? craftInfo : CraftAttributeInfo.Blank);
            _RawResource = raw;
            _RawNumber = rawNumber;
            _RawName = rawName;
            _ProcessedResource = processed;
            _ProcessedNumber = processedNumber;
            _ProcessedName = processedName;
            _RawResourceUseableForCrafting = useRaw;
        }

        public ResourceInfo Clone()
        {
            ResourceInfo info = new ResourceInfo();

            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo fi in fields)
            {
                if (fi.Name != "_AttributeInfo")
                    fi.SetValue(info, fi.GetValue(this));
                else
                    info._AttributeInfo = _AttributeInfo.Clone();
            }

            return info;
        }

        public ResourceInfo(GenericReader reader)
        {
            Int32 version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    _TypeofResource = (ResourceType)reader.ReadInt();

                    if (reader.ReadBool())
                        _HarvestMessage = reader.ReadInt();
                    else
                        _HarvestMessage = reader.ReadString();

                    _ResourceNumber = reader.ReadInt();
                    _ResourceName = reader.ReadString();
                    _Hue = reader.ReadInt();
                    _HarvestSkill = reader.ReadString();
                    _MinimumSkillRequired = reader.ReadDouble();
                    _HarvestChance = reader.ReadDouble();

                    String type = reader.ReadString();

                    if (type != null)
                        _RawResource = ScriptCompiler.FindTypeByName(type);

                    _RawNumber = reader.ReadInt();
                    _RawName = reader.ReadString();

                    type = reader.ReadString();

                    if (type != null)
                        _ProcessedResource = ScriptCompiler.FindTypeByName(type);

                    _ProcessedNumber = reader.ReadInt();
                    _ProcessedName = reader.ReadString();
                    _RawResourceUseableForCrafting = reader.ReadBool();
                    _AttributeInfo = new CraftAttributeInfo(reader);

                    break;
            }
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);    // Version

            writer.Write((Int32)_TypeofResource);

            if (_HarvestMessage is Int32)
            {
                writer.Write(true);
                writer.Write((Int32)_HarvestMessage);
            }
            else
            {
                writer.Write(false);

                String harvestMessage = null;

                if (_HarvestMessage != null)
                    harvestMessage = _HarvestMessage.ToString();

                writer.Write(harvestMessage);
            }

            writer.Write(_ResourceNumber);
            writer.Write(_ResourceName);
            writer.Write(_Hue);
            writer.Write(_HarvestSkill);
            writer.Write(_MinimumSkillRequired);
            writer.Write(_HarvestChance);

            String type = null;

            if (_RawResource != null)
                type = _RawResource.Name;

            writer.Write(type);
            writer.Write(_RawNumber);
            writer.Write(_RawName);

            type = null;

            if (_ProcessedResource != null)
                type = _ProcessedResource.Name;

            writer.Write(type);
            writer.Write(_ProcessedNumber);
            writer.Write(_ProcessedName);
            writer.Write(_RawResourceUseableForCrafting);
            _AttributeInfo.Serialize(writer);
        }
    }
}
