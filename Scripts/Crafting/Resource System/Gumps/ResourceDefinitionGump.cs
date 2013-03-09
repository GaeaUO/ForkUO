using System;
using System.Collections.Generic;
using System.Linq;

using Server;
using Server.Gumps;

namespace Server.Resources
{
    public class ResourceDefinitionGump : Gump
    {
        private const Int32 HUE_ValidEntry = 0;
        private const Int32 HUE_InvalidEntry = 0x22;

        private Int32 _GroupIndex;
        private Int32 _ResourceIndex;

        private List<String> _DefinedGroups = new List<string>();
        private Dictionary<String, List<ResourceInfo>> _DefinedResources = new Dictionary<string, List<ResourceInfo>>();

        private String _GroupName;
        private List<ResourceInfo> _Resources;
        private ResourceInfo _Resource;

        private CraftInfoGumpling _CraftInfoGumping;
        private Dictionary<String, String> _Values;
        private ResourceType _Type;
        private Boolean _UseRaw;

        public ResourceDefinitionGump(List<String> definedGroups, Dictionary<String, List<ResourceInfo>> definedResources, Int32 groupIndex, String groupName, List<ResourceInfo> resources, Int32 resourceIndex, Dictionary<String, String> values, ResourceType type, Boolean useRaw, Boolean validate) : base(150, 100)
        {
            _GroupIndex = groupIndex;
            _DefinedGroups = definedGroups;
            _DefinedResources = definedResources;
            _GroupName = groupName;
            _Resources = resources;
            _ResourceIndex = resourceIndex;

            if (_ResourceIndex < _Resources.Count)
                _Resource = _Resources[_ResourceIndex];
            else
                _Resource = new ResourceInfo();

            _Values = values;
            _Type = type;
            _UseRaw = useRaw;

            if (_GroupIndex < 0)
                _GroupIndex = 0;

            if (_ResourceIndex < 0)
                _ResourceIndex = 0;

            bool sendCraftAttributes = false;

            if (_Values == null)
            {
                _Values = new Dictionary<String, String>();

                _Values["Resource Cliloc"] = _Resource.ResourceNumber.ToString();
                _Values["Resource Name"] = _Resource.ResourceName != null ? _Resource.ResourceName : "";
                _Values["Harvest Message"] = _Resource.HarvestMessage != null ? _Resource.HarvestMessage.ToString() : "";
                _Values["Resource Hue"] = _Resource.Hue.ToString();
                _Values["Harvest Skill"] = _Resource.HarvestSkill != null ? _Resource.HarvestSkill : "";
                _Values["Harvest Chance"] = _Resource.HarvestChance.ToString();
                _Values["Minimum Skill Required"] = _Resource.MinimumSkillRequired.ToString();
                _Values["Raw Type"] = _Resource.RawResource != null ? _Resource.RawResource.Name : "";
                _Values["Raw Cliloc"] = _Resource.RawNumber.ToString();
                _Values["Raw Name"] = _Resource.RawName != null ? _Resource.RawName : "";
                _Values["Processed Type"] = _Resource.ProcessedResource != null ? _Resource.ProcessedResource.Name : "";
                _Values["Processed Cliloc"] = _Resource.ProcessedNumber.ToString();
                _Values["Processed Name"] = _Resource.ProcessedName != null ? _Resource.ProcessedName : "";

                _Type = _Resource.TypeofResource;
                _UseRaw = _Resource.RawResourceUseableForCrafting;
                sendCraftAttributes = true;
            }

            Add(new StoneyBackground(500, 455));
            Add(new GumpLabel(10, 4, 0x0, "Resource Type:"));
            Add(new RadioGumpling(125, 4, "Harvested", _Type == ResourceType.Harvested, "Harvested"));
            Add(new RadioGumpling(225, 4, "Skinned", _Type == ResourceType.Skinned, "Skinned"));
            Add(new RadioGumpling(310, 4, "Bonus", _Type == ResourceType.Bonus, "Bonus"));
            Add(new LabelEntryField(10, 24, 110, 95, "Resource Cliloc", !validate || IsInteger(_Values["Resource Cliloc"], false) ? HUE_ValidEntry : HUE_InvalidEntry, _Values["Resource Cliloc"]));
            Add(new LabelEntryField(10, 46, 110, 365, "Resource Name", !validate || _Values["Resource Name"] != "" ? HUE_ValidEntry : HUE_InvalidEntry, _Values["Resource Name"]));
            Add(new LabelEntryField(10, 68, 110, 365, "Harvest Message", 0x0, _Values["Harvest Message"]));
            Add(new LabelEntryField(10, 90, 110, 60, "Resource Hue", !validate || IsInteger(_Values["Resource Hue"], true) ? HUE_ValidEntry : HUE_InvalidEntry, _Values["Resource Hue"]));
            Add(new LabelEntryField(10, 112, 110, 150, "Harvest Skill", !validate || IsValidSkill(_Values["Harvest Skill"]) ? HUE_ValidEntry : HUE_InvalidEntry, _Values["Harvest Skill"]));
            Add(new LabelEntryField(10, 134, 110, 60, "Harvest Chance", !validate || IsDouble(_Values["Harvest Chance"]) ? HUE_ValidEntry : HUE_InvalidEntry, _Values["Harvest Chance"]));
            Add(new LabelEntryField(280, 112, 145, 60, "Minimum Skill Required", !validate || IsDouble(_Values["Minimum Skill Required"]) ? HUE_ValidEntry : HUE_InvalidEntry, _Values["Minimum Skill Required"]));
            Add(new CheckboxGumpling(255, 137, "UseRaw", _UseRaw, "Raw Resource Useable in Crafting"));

            Add(new TypeGumpling(10, 160, 235, 101, "Raw", new List<int>() 
            {
                !validate || IsValidResourceType(_Values["Raw Type"], false) ? HUE_ValidEntry : HUE_InvalidEntry,
                !validate || IsInteger(_Values["Raw Cliloc"], true) ? HUE_ValidEntry : HUE_InvalidEntry,
                !validate || (_Values["Raw Name"] != "" || _Values["Raw Cliloc"] != "") ? HUE_ValidEntry : HUE_InvalidEntry
            }, new List<string>()
            {
                _Values["Raw Type"],
                _Values["Raw Cliloc"],
                _Values["Raw Name"]
            }));

            Add(new TypeGumpling(255, 160, 235, 101, "Processed", new List<int>() 
            {
                !validate || IsValidResourceType(_Values["Processed Type"], false) ? HUE_ValidEntry : HUE_InvalidEntry,
                !validate || IsInteger(_Values["Processed Cliloc"], true) ? HUE_ValidEntry : HUE_InvalidEntry,
                !validate || (_Values["Processed Name"] != "" || _Values["Processed Cliloc"] != "") ? HUE_ValidEntry : HUE_InvalidEntry
            }, new List<string>()
            {
                _Values["Processed Type"],
                _Values["Processed Cliloc"],
                _Values["Processed Name"]
            }));

            Add(new ApplyCancelGumpling(330, 425, ApplyButtonPressed, CancelButtonPressed));

            if (sendCraftAttributes)
                Add(_CraftInfoGumping = new CraftInfoGumpling(10, 265, _Resource.AttributeInfo, validate));
            else
                Add(_CraftInfoGumping = new CraftInfoGumpling(10, 265, _Values, validate));
        }

        public override void OnAddressChange()
        {
            if (Address != null)
            {
                Address.CloseGump(typeof(ResourceServiceGump));
                Address.CloseGump(typeof(ResourceGroupGump));
                Address.CloseGump(typeof(ResourceDefinitionGump));
            }
        }

        private void ApplyButtonPressed(IGumpComponent sender, object param)
        {
            foreach (IGumpComponent gc in Entries)
                if (gc is GumpTextEntry)
                    _Values[((GumpTextEntry)gc).Name] = ((GumpTextEntry)gc).InitialText;

            if (GetCheck("Harvested"))
                _Type = ResourceType.Harvested;
            else if (GetCheck("Skinned"))
                _Type = ResourceType.Skinned;
            else
                _Type = ResourceType.Bonus;

            _UseRaw = GetCheck("UseRaw");

            if (Address != null)
            {
                if (IsValid)
                {
                    object harvestMessage = GetTextEntry("Harvest Message");

                    if (IsInteger(harvestMessage.ToString(), false))
                        harvestMessage = Int32.Parse(harvestMessage.ToString());

                    _Resource = new ResourceInfo(_Type, harvestMessage, GetInt32("Resource Cliloc"), GetTextEntry("Resource Name"), GetInt32("Resource Hue"),
                        GetTextEntry("Harvest Skill"), GetDouble("Minimum Skill Required"), GetDouble("Harvest Chance"), _CraftInfoGumping.GetCraftInfo(),
                        GetTypeValue("Raw Type"), GetInt32("Raw Cliloc"), GetTextEntry("Raw Name"), GetTypeValue("Processed Type"),
                        GetInt32("Processed Cliloc"), GetTextEntry("Processed Name"), _UseRaw);

                    if (_ResourceIndex < _Resources.Count)
                        _Resources[_ResourceIndex] = _Resource;
                    else
                        _Resources.Add(_Resource);

                    Address.SendGump(new ResourceGroupGump(_DefinedGroups, _DefinedResources, _GroupIndex, _GroupName, _Resources, _ResourceIndex / 10, true));
                }
                else
                    Address.SendGump(new ResourceDefinitionGump(_DefinedGroups, _DefinedResources, _GroupIndex, _GroupName, _Resources, _ResourceIndex, _Values, _Type, _UseRaw, true));
            }

        }

        private void CancelButtonPressed(IGumpComponent sender, object param)
        {
            Address.SendGump(new ResourceGroupGump(_DefinedGroups, _DefinedResources, _GroupIndex, _GroupName, _Resources, _ResourceIndex / 10, true));
        }

        private Boolean IsValid
        {
            get
            {
                Boolean result = true;

                result &= IsInteger(GetTextEntry("Resource Cliloc"), false);
                result &= (GetTextEntry("Resource Name") != "");
                result &= IsInteger(GetTextEntry("Resource Hue"), true);
                result &= IsValidSkill(GetTextEntry("Harvest Skill"));
                result &= IsDouble(GetTextEntry("Harvest Chance"));
                result &= IsDouble(GetTextEntry("Minimum Skill Required"));
                result &= IsValidResourceType(GetTextEntry("Raw Type"), false);
                result &= IsInteger(GetTextEntry("Raw Cliloc"), true);
                result &= (GetTextEntry("Raw Name") != "" || GetTextEntry("Raw Cliloc") != "");
                result &= IsValidResourceType(GetTextEntry("Processed Type"), true);
                result &= IsInteger(GetTextEntry("Processed Cliloc"), true);
                result &= (GetTextEntry("Processed Type") != "" && (GetTextEntry("Processed Name") != "" || GetTextEntry("Processed Cliloc") != ""));
                result &= _CraftInfoGumping.IsValid;

                return result;
            }
        }

        public static Boolean IsInteger(String value, Boolean allowBlank)
        {
            Int32 val;

            return (allowBlank && value == "" ? true : Int32.TryParse(value, out val));
        }

        private Boolean IsValidSkill(String value)
        {
            SkillName val;

            return value == "" || Enum.TryParse(value, out val);
        }

        private Boolean IsDouble(String value)
        {
            Double val;

            return value == "" || Double.TryParse(value, out val);
        }

        private Boolean IsValidResourceType(String value, Boolean allowBlank)
        {
            Type type = ScriptCompiler.FindTypeByName(value);
            Type typeCheck = typeof(IResource);

            Boolean valid = type != null || (value == "" && !allowBlank);

            if (typeCheck != null && type != null)
                valid &= typeCheck.IsAssignableFrom(type);

            return valid;
        }

        private Int32 GetInt32(String textentry)
        {
            Int32 value = 0;
            Int32.TryParse(GetTextEntry(textentry), out value);

            return value;
        }

        private Double GetDouble(String textentry)
        {
            Double value = 0;
            Double.TryParse(GetTextEntry(textentry), out value);

            return value;
        }

        private Type GetTypeValue(String textentry)
        {
            return ScriptCompiler.FindTypeByName(GetTextEntry(textentry), true);
        }
    }
}
