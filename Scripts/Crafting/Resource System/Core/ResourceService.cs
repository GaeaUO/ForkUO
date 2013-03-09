using System;
using System.Collections.Generic;
using System.Linq;

using CustomsFramework;

using Server;
using Server.Commands;
using Server.Gumps;
using Server.Items;
using Server.Resources;

namespace Server.Resources
{
    public class ResourceService : BaseService
    {
        private static ResourceService _Service;
        public static ResourceService Service { get { return _Service; } }

        private Dictionary<String, List<ResourceInfo>> _DefinedResources = new Dictionary<string, List<ResourceInfo>>();
        public Dictionary<String, List<ResourceInfo>> DefinedResources { get { return _DefinedResources; } }

        private Int32 _DefinitionVersion = 0;

        public const String SystemVersion = "1.0";

        public ResourceService() : base()
        {
            ResourceService._Service = this;
        }

        public ResourceService(CustomSerial serial) : base(serial)
        {
            ResourceService._Service = this;
        }

        public override String Name { get { return "Resource Service"; } }
        public override String Description { get { return "Service for the revamped Resource system"; } }
        public override String Version { get { return SystemVersion; } }
        public override AccessLevel EditLevel { get { return AccessLevel.Developer; } }
        public override Gump SettingsGump { get { return null; } }

        public static void Initialize()
        {
            ResourceService service = World.GetData(typeof(ResourceService)) as ResourceService;

            if (service == null)
            {
                service = new ResourceService();
                service.Prep();
            }

            _Service = service;

            service.InitializeDefaults(false);

            foreach (Item item in World.Items.Values)
            {
                if (item is IResource)
                {
                    IResource resource = item as IResource;

                    if (resource.PreloadResource is String)
                    {
                        resource.Resource = new ResourceString(resource.ResourceGroup, resource.PreloadResource.ToString());
                    }
                    else if (resource.PreloadResource is object[])
                    {
                        object[] list = resource.PreloadResource as object[];

                        if (list.Length == 2)
                        {
                            if (list[0] is String && list[0].ToString() == "OR" && list[1] is Int32)
                                resource.Resource = new ResourceString(resource.ResourceGroup, service.ConvertResource((Int32)list[1]).ResourceName);
                            else if (list[0] is String && list[0].ToString() == "OI")
                                resource.Resource = new ResourceString(resource.ResourceGroup, service.ConvertOreInfo(list[1]).ResourceName);
                        }
                    }
                }
            }

            CommandSystem.Register("Resources", AccessLevel.Developer, new CommandEventHandler(Resources_OnCommand));
            CommandSystem.Register("ResourceReset", AccessLevel.Developer, new CommandEventHandler(ResourceReset_OnCommand));
        }

        [Usage("Resources")]
        [Description("Displays the Resource Service configuration gump.")]
        public static void Resources_OnCommand(CommandEventArgs e)
        {
            if (ResourceService.Service == null)
                return;

            e.Mobile.SendGump(new ResourceServiceGump());
        }

        [Usage("ResourceReset")]
        [Description("Resets the Resource Service configuration.")]
        public static void ResourceReset_OnCommand(CommandEventArgs e)
        {
            if (ResourceService.Service == null)
                return;

            e.Mobile.CloseGump(typeof(ResourceServiceGump));
            e.Mobile.CloseGump(typeof(ResourceGroupGump));
            e.Mobile.CloseGump(typeof(ResourceDefinitionGump));

            ResourceService.Service.InitializeDefaults(true);

            e.Mobile.SendMessage("Resource service definitions have been reset to default.");
        }

        public ResourceInfo LookupResource(String groupName, String resource)
        {
            List<ResourceInfo> infoList = DefinedResources.FirstOrDefault(s => s.Key.Equals(groupName, StringComparison.OrdinalIgnoreCase)).Value;
            ResourceInfo info = null;

            if (infoList != null)
            {
                info = infoList.FirstOrDefault(mod => mod.ResourceName.Equals(resource, StringComparison.OrdinalIgnoreCase));

                if (info == null)
                    info = new ResourceInfo();
            }
            else
                info = new ResourceInfo();

            return info;
        }

        public String[] GetResources(String groupName)
        {
            String[] resources = new String[0];

            List<ResourceInfo> infoList = DefinedResources.FirstOrDefault(s => s.Key.Equals(groupName, StringComparison.OrdinalIgnoreCase)).Value;

            if (infoList != null)
            {
                resources = new String[infoList.Count];

                for (Int32 i = 0; i < infoList.Count; i++)
                    resources[i] = infoList[i].ResourceName;
            }

            return resources;
        }

        public ResourceInfo Mutate(String groupName, String resource)
        {
            List<ResourceInfo> infoList = DefinedResources.FirstOrDefault(s => s.Key.Equals(groupName, StringComparison.OrdinalIgnoreCase)).Value;
            ResourceInfo info = null;

            if (infoList != null)
            {
                for (Int32 i = 0; i < infoList.Count; i++)
                {
                    if (infoList[i].ResourceName.Equals(resource, StringComparison.OrdinalIgnoreCase) && info == null)
                    {
                        if (i == infoList.Count - 1)
                            info = infoList[i];
                        else
                            info = infoList[i + 1];

                        break;
                    }
                }
            }

            return info;
        }

        public ResourceInfo DefaultResource(String groupName)
        {
            List<ResourceInfo> infoList = DefinedResources.FirstOrDefault(s => s.Key.Equals(groupName, StringComparison.OrdinalIgnoreCase)).Value;
            ResourceInfo info = null;

            if (infoList != null && infoList.Count > 0)
                info = infoList[0];

            return info;
        }

        public ResourceInfo RandomResource(String groupName)
        {
            List<ResourceInfo> infoList = DefinedResources.FirstOrDefault(s => s.Key.Equals(groupName, StringComparison.OrdinalIgnoreCase)).Value;
            ResourceInfo info = null;

            if (infoList != null && infoList.Count > 0)
            {
                if (infoList.Count == 1 && infoList[0].HarvestChance == 100.0)
                    info = infoList[0];
                else if (infoList[0].HarvestChance == 100.0)
                    info = infoList[Utility.RandomMinMax(0, infoList.Count - 1)];
                else
                {
                    Double chance = Utility.RandomDouble() * 100;

                    for (Int32 i = 0; i < infoList.Count; i++)
                    {
                        if (chance <= infoList[i].HarvestChance)
                        {
                            info = infoList[i];
                            break;
                        }

                        chance -= infoList[i].HarvestChance;
                    }
                }
            }

            return info;
        }

        private ResourceInfo ConvertResource(Int32 oldIndex)
        {
            switch (oldIndex)
            {
                case 1:
                    return LookupResource("Metals", "Iron");
                case 2:
                    return LookupResource("Metals", "Dull Copper");
                case 3:
                    return LookupResource("Metals", "Shadow Iron");
                case 4:
                    return LookupResource("Metals", "Copper");
                case 5:
                    return LookupResource("Metals", "Bronze");
                case 6:
                    return LookupResource("Metals", "Golden");
                case 7:
                    return LookupResource("Metals", "Agapite");
                case 8:
                    return LookupResource("Metals", "Verite");
                case 9:
                    return LookupResource("Metals", "Valorite");
                case 101:
                    return LookupResource("Leather", "Normal");
                case 102:
                    return LookupResource("Leather", "Spined");
                case 103:
                    return LookupResource("Leather", "Horned");
                case 104:
                    return LookupResource("Leather", "Barbed");
                case 201:
                    return LookupResource("Scales", "Red Scales");
                case 202:
                    return LookupResource("Scales", "Yellow Scales");
                case 203:
                    return LookupResource("Scales", "Black Scales");
                case 204:
                    return LookupResource("Scales", "Green Scales");
                case 205:
                    return LookupResource("Scales", "White Scales");
                case 206:
                    return LookupResource("Scales", "Blue Scales");
                case 301:
                    return LookupResource("Wood", "Normal");
                case 302:
                    return LookupResource("Wood", "Oak");
                case 303:
                    return LookupResource("Wood", "Ash");
                case 304:
                    return LookupResource("Wood", "Yew");
                case 305:
                    return LookupResource("Wood", "Heartwood");
                case 306:
                    return LookupResource("Wood", "Bloodwood");
                case 307:
                    return LookupResource("Wood", "Frostwood");
                default:
                    return new ResourceInfo();
            }
        }

        private ResourceInfo ConvertOreInfo(object oldValue)
        {
            if (oldValue is Int32)
            {
                Int32 oldIndex = 0;
                Int32.TryParse(oldValue.ToString(), out oldIndex);

                switch (oldIndex)
                {
                    case 1:
                        return LookupResource("Metals", "Dull Copper");
                    case 2:
                        return LookupResource("Metals", "Shadow Iron");
                    case 3:
                        return LookupResource("Metals", "Copper");
                    case 4:
                        return LookupResource("Metals", "Bronze");
                    case 5:
                        return LookupResource("Metals", "Golden");
                    case 6:
                        return LookupResource("Metals", "Agapite");
                    case 7:
                        return LookupResource("Metals", "Verite");
                    case 8:
                        return LookupResource("Metals", "Valorite");
                    default:
                        return LookupResource("Metals", "Iron");
                }
            }
            else if (oldValue is String)
            {
                String str = oldValue as String;

                if (str.IndexOf("Spined") >= 0)
                    return LookupResource("Leather", "Spined");
                else if (str.IndexOf("Horned") >= 0)
                    return LookupResource("Leather", "Horned");
                else if (str.IndexOf("Barbed") >= 0)
                    return LookupResource("Leather", "Barbed");
                else
                    return LookupResource("Leather", "Normal");
            }
            else
                return new ResourceInfo();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            Utilities.WriteVersion(writer, 0);

            writer.Write(_DefinitionVersion);
            writer.Write(DefinedResources.Keys.Count);

            foreach(KeyValuePair<String, List<ResourceInfo>> pair in DefinedResources)
            {
                writer.Write(pair.Key);
                writer.Write(pair.Value.Count);

                foreach (ResourceInfo info in pair.Value)
                    info.Serialize(writer);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            Int32 version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    _DefinitionVersion = reader.ReadInt();
                    Int32 groupCount = reader.ReadInt();

                    for(Int32 i = 0; i < groupCount; i ++)
                    {
                        String groupName = reader.ReadString();
                        Int32 infoCount = reader.ReadInt();
                        List<ResourceInfo> infoList = new List<ResourceInfo>();

                        for (Int32 j = 0; j < infoCount; j++)
                            infoList.Add(new ResourceInfo(reader));

                        DefinedResources.Add(groupName, infoList);
                    }

                    break;
            }
        }

        public void InitializeDefaults(bool reset)
        {
            if (reset)
            {
                DefinedResources.Clear();
                _DefinitionVersion = 0;
            }

            if (_DefinitionVersion < 1)
            {
                // Metal
                if (DefinedResources.ContainsKey("Metal"))
                    DefinedResources.Remove("Metal");

                DefinedResources.Add("Metal", new List<ResourceInfo>
                {
                    new ResourceInfo(ResourceType.Harvested, 1007072, 1053109, "Iron", 0x0, "Mining", 0.0, 49.6, CraftAttributeInfo.Blank, typeof(IronOre), 1042853, typeof(IronIngot), 1042692, false),
                    new ResourceInfo(ResourceType.Harvested, 1007073, 1053108, "Dull Copper", 0x973, "Mining", 65.0, 11.2, CraftAttributeInfo.DullCopper, typeof(DullCopperOre), 1042845, typeof(DullCopperIngot), 1042684, false),
                    new ResourceInfo(ResourceType.Harvested, 1007074, 1053107, "Shadow Iron", 0x966, "Mining", 70.0, 9.8, CraftAttributeInfo.ShadowIron, typeof(ShadowIronOre), 1042846, typeof(ShadowIronIngot), 1042685, false),
                    new ResourceInfo(ResourceType.Harvested, 1007075, 1053106, "Copper", 0x96D, "Mining", 75.0, 8.4, CraftAttributeInfo.Copper, typeof(CopperOre), 1042847, typeof(CopperIngot), 1042686, false),
                    new ResourceInfo(ResourceType.Harvested, 1007076, 1053105, "Bronze", 0x972, "Mining", 80.0, 7.0, CraftAttributeInfo.Bronze, typeof(BronzeOre), 1042848, typeof(BronzeIngot), 1042687, false),
                    new ResourceInfo(ResourceType.Harvested, 1007077, 1053104, "Golden", 0x8A5, "Mining", 85.0, 5.6, CraftAttributeInfo.Golden, typeof(GoldOre), 1042849, typeof(GoldIngot), 1042688, false),
                    new ResourceInfo(ResourceType.Harvested, 1007078, 1053103, "Agapite", 0x979, "Mining",  90.0, 4.2, CraftAttributeInfo.Agapite, typeof(AgapiteOre), 1042850, typeof(AgapiteIngot), 1042689, false),
                    new ResourceInfo(ResourceType.Harvested, 1007079, 1053102, "Verite", 0x89F, "Mining", 95.0, 2.8, CraftAttributeInfo.Verite, typeof(VeriteOre), 1042851, typeof(VeriteIngot), 1042690, false),
                    new ResourceInfo(ResourceType.Harvested, 1007080, 1053101, "Valorite", 0x8AB, "Mining", 99.0, 1.4, CraftAttributeInfo.Valorite, typeof(ValoriteOre), 1042852, typeof(ValoriteIngot), 1042691, false),
                });

                // Scales
                if (DefinedResources.ContainsKey("Scales"))
                    DefinedResources.Remove("Scales");

                DefinedResources.Add("Scales", new List<ResourceInfo>
                {
                    new ResourceInfo(ResourceType.Skinned, 1079284, 1053129, "Red Scales", 0x66D, CraftAttributeInfo.RedScales, typeof(RedScales)),
                    new ResourceInfo(ResourceType.Skinned, 1079284, 1053130, "Yellow Scales", 0x8A8, CraftAttributeInfo.YellowScales, typeof(YellowScales)),
                    new ResourceInfo(ResourceType.Skinned, 1079284, 1053131, "Black Scales", 0x455, CraftAttributeInfo.BlackScales, typeof(BlackScales)),
                    new ResourceInfo(ResourceType.Skinned, 1079284, 1053132, "Green Scales", 0x851, CraftAttributeInfo.GreenScales, typeof(GreenScales)),
                    new ResourceInfo(ResourceType.Skinned, 1079284, 1053133, "White Scales", 0x8FD, CraftAttributeInfo.WhiteScales, typeof(WhiteScales)),
                    new ResourceInfo(ResourceType.Skinned, 1079284, 1053134, "Blue Scales", 0x8B0, CraftAttributeInfo.BlueScales, typeof(BlueScales))
                });

                // Leather
                if (DefinedResources.ContainsKey("Leather"))
                    DefinedResources.Remove("Leather");

                DefinedResources.Add("Leather", new List<ResourceInfo>
                {
                    new ResourceInfo(ResourceType.Skinned, 500471, 1049353, "Normal", 0x0, 0.0, CraftAttributeInfo.Blank, typeof(Hides), 1047023, typeof(Leather), 1047022, true),
                    new ResourceInfo(ResourceType.Skinned, 500471, 1049354, "Spined", (Core.AOS ? 0x8AC : 0x283), 65.0, CraftAttributeInfo.Spined, typeof(SpinedHides), 1049687, typeof(SpinedLeather), 1049684, true),
                    new ResourceInfo(ResourceType.Skinned, 500471, 1049355, "Horned", (Core.AOS ? 0x845 : 0x227), 80.0, CraftAttributeInfo.Horned, typeof(HornedHides), 1049688, typeof(HornedLeather), 1049685, true),
                    new ResourceInfo(ResourceType.Skinned, 500471, 1049356, "Barbed", (Core.AOS ? 0x851 : 0x1C1), 99.0, CraftAttributeInfo.Barbed, typeof(BarbedHides), 1049689, typeof(BarbedLeather), 1049686, true)
                });

                // Wood
                if (DefinedResources.ContainsKey("Wood"))
                    DefinedResources.Remove("Wood");

                DefinedResources.Add("Wood", new List<ResourceInfo>
                {
                    new ResourceInfo(ResourceType.Harvested, 1072540, 1011542, "Normal", 0x0, "Lumberjacking", 0.0, 49.0, CraftAttributeInfo.Blank, typeof(Log), 1027134, typeof(Board), 1027128, true),
                    new ResourceInfo(ResourceType.Harvested, 1072541, 1072533, "Oak", 0x7DA, "Lumberjacking", 65.0, 30.0, CraftAttributeInfo.OakWood, typeof(OakLog), 1075063, typeof(OakBoard), 1075052, true),
                    new ResourceInfo(ResourceType.Harvested, 1072542, 1072534, "Ash", 0x4A7, "Lumberjacking", 80.0, 10.0, CraftAttributeInfo.AshWood, typeof(AshLog), 1075064, typeof(AshBoard), 1075053, true),
                    new ResourceInfo(ResourceType.Harvested, 1072543, 1072535, "Yew", 0x4A8, "Lumberjacking",  95.0, 5.0, CraftAttributeInfo.YewWood, typeof(YewLog), 1075065, typeof(YewBoard), 1075054, true),
                    new ResourceInfo(ResourceType.Harvested, 1072544, 1072536, "Heartwood", 0x4A9, "Lumberjacking", 100.0, 3.0, CraftAttributeInfo.Heartwood, typeof(HeartwoodLog), 1075066, typeof(HeartwoodBoard), 1075062, true),
                    new ResourceInfo(ResourceType.Harvested, 1072545, 1072538, "Bloodwood", 0x4AA, "Lumberjacking", 100.0, 2.0, CraftAttributeInfo.Bloodwood, typeof(BloodwoodLog), 1075067, typeof(BloodwoodBoard), 1075055, true),
                    new ResourceInfo(ResourceType.Harvested, 1072546, 1072539, "Frostwood", 0x47F, "Lumberjacking", 100.0, 1.0, CraftAttributeInfo.Frostwood, typeof(FrostwoodLog), 1075068, typeof(FrostwoodBoard), 1075056, true)
                });

                // Sand
                if (DefinedResources.ContainsKey("Sand"))
                    DefinedResources.Remove("Sand");

                DefinedResources.Add("Sand", new List<ResourceInfo>
                {
                    new ResourceInfo(ResourceType.Harvested, 1044631, 1044626, "Sand", 0x0, "Mining", 100.0, 100.0, typeof(Sand))
                });

                // Fish
                if (DefinedResources.ContainsKey("Fish"))
                    DefinedResources.Remove("Fish");

                DefinedResources.Add("Fish", new List<ResourceInfo>
                {
                    new ResourceInfo(ResourceType.Harvested, 1043297, 1022508, "Fish", 0x0, "Fishing", 0.0, 100.0, typeof(Fish))
                });
            }

            _DefinitionVersion = 1;
        }
    }
}