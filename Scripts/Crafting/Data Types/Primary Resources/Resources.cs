using System;
using System.Collections.Generic;
using System.Linq;

using Server.Items;

namespace Server.Crafting
{
    public class Resources
    {
        private static List<ResourceType> _types = new List<ResourceType>();
        public static List<ResourceType> ResourceTypes { get { return _types; } }

        public static ResourceInfo LookupResource(String groupName, String resource)
        {
            ResourceType type = Resources.ResourceTypes.FirstOrDefault(mod => mod.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
            ResourceInfo info = null;

            if (type != null)
            {
                info = type.Resources.FirstOrDefault(mod => mod.Name.Equals(resource, StringComparison.OrdinalIgnoreCase));

                if (info== null)
                    info = new ResourceInfo();
            }
            else
                info = new ResourceInfo();

            return info;
        }

        public static String[] GetResources(String groupName)
        {
            String[] resources= new String[0];

            ResourceType type = Resources.ResourceTypes.FirstOrDefault(mod => mod.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));

            if (type != null)
            {
                resources = new String[type.Resources.Count];

                for (Int32 i = 0; i < type.Resources.Count; i++)
                    resources[i] = type.Resources[i].Name;
            }

            return resources;
        }

        public static ResourceInfo Mutate(String groupName, String resource)
        {
            ResourceType type = Resources.ResourceTypes.FirstOrDefault(mod => mod.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
            ResourceInfo info = null;

            if (type != null)
            {
                for (Int32 i = 0; i < type.Resources.Count; i++)
                {
                    if (type.Resources[i].Name.Equals(resource, StringComparison.OrdinalIgnoreCase) && info == null)
                    {
                        if (i == type.Resources.Count - 1)
                            info = type.Resources[i];
                        else
                            info = type.Resources[i + 1];

                        break;
                    }
                }
            }

            return info;
        }

        public static ResourceInfo DefaultResource(String groupName)
        {
            ResourceType type = Resources.ResourceTypes.FirstOrDefault(mod => mod.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
            ResourceInfo info = null;

            if (type != null && type.Resources.Count > 0)
                info = type.Resources[0];

            return info;
        }

        public static ResourceInfo RandomResource(String groupName)
        {
            ResourceType type = Resources.ResourceTypes.FirstOrDefault(mod => mod.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
            ResourceInfo info = null;

            if (type != null && type.Resources.Count > 0)
            {
                if (type.Resources.Count == 1)
                    info = type.Resources[0];
                else if (type.Resources[0].ChanceToObtain == 100.0)
                    info = type.Resources[Utility.RandomMinMax(0, type.Resources.Count - 1)];
                else
                {
                    Double chance = Utility.RandomDouble() * 100;

                    for (Int32 i = 0; i < type.Resources.Count; i++)
                    {
                        if (chance <= type.Resources[i].ChanceToObtain)
                        {
                            info = type.Resources[i];
                            break;
                        }

                        chance -= type.Resources[i].ChanceToObtain;
                    }
                }
            }

            return info;
        }

        public static ResourceInfo ConvertResource(Int32 oldIndex)
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

        public static ResourceInfo ConvertOreInfo(object oldValue)
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

        public static void Configure()
        {
            // Metal
            ResourceTypes.Add(new ResourceType("Metal",
                new ResourceInfo(1053109, "Iron", 0x0, 0.0, 49.6, CraftAttributeInfo.Blank, typeof(IronOre), typeof(IronIngot), false, 1042853),
                new ResourceInfo(1053108, "Dull Copper", 0x973, 65.0, 11.2, CraftAttributeInfo.DullCopper, typeof(DullCopperOre), typeof(DullCopperIngot), false, 1042845),
                new ResourceInfo(1053107, "Shadow Iron", 0x966, 70.0, 9.8, CraftAttributeInfo.ShadowIron, typeof(ShadowIronOre), typeof(ShadowIronIngot), false, 1042846),
                new ResourceInfo(1053106, "Copper", 0x96D, 75.0, 8.4, CraftAttributeInfo.Copper, typeof(CopperOre), typeof(CopperIngot), false, 1042847),
                new ResourceInfo(1053105, "Bronze", 0x972, 80.0, 7.0, CraftAttributeInfo.Bronze, typeof(BronzeOre), typeof(BronzeIngot), false, 1042848),
                new ResourceInfo(1053104, "Golden", 0x8A5, 85.0, 5.6, CraftAttributeInfo.Golden, typeof(GoldOre), typeof(GoldIngot), false, 1042849),
                new ResourceInfo(1053103, "Agapite", 0x979, 90.0, 4.2, CraftAttributeInfo.Agapite, typeof(AgapiteOre), typeof(AgapiteIngot), false, 1042850),
                new ResourceInfo(1053102, "Verite", 0x89F, 95.0, 2.8, CraftAttributeInfo.Verite, typeof(VeriteOre), typeof(VeriteIngot), false, 1042851),
                new ResourceInfo(1053101, "Valorite", 0x8AB, 99.0, 1.4, CraftAttributeInfo.Valorite, typeof(ValoriteOre), typeof(ValoriteIngot), false, 1042852)));

            // Scales
            ResourceTypes.Add(new ResourceType("Scales",
                new ResourceInfo(1053129, "Red Scales", 0x66D, 0.0, CraftAttributeInfo.RedScales, typeof(RedScales)),
                new ResourceInfo(1053130, "Yellow Scales", 0x8A8, 0.0, CraftAttributeInfo.YellowScales, typeof(YellowScales)),
                new ResourceInfo(1053131, "Black Scales", 0x455, 0.0, CraftAttributeInfo.BlackScales, typeof(BlackScales)),
                new ResourceInfo(1053132, "Green Scales", 0x851, 0.0, CraftAttributeInfo.GreenScales, typeof(GreenScales)),
                new ResourceInfo(1053133, "White Scales", 0x8FD, 0.0, CraftAttributeInfo.WhiteScales, typeof(WhiteScales)),
                new ResourceInfo(1053134, "Blue Scales", 0x8B0, 0.0, CraftAttributeInfo.BlueScales, typeof(BlueScales))));

            // Leather
            ResourceTypes.Add(new ResourceType("Leather",
                new ResourceInfo(1049353, "Normal", 0x0, 0.0, CraftAttributeInfo.Blank, typeof(Hides), typeof(Leather), true, 1047023),
                new ResourceInfo(1049354, "Spined", (Core.AOS ? 0x8AC : 0x283), 65.0, CraftAttributeInfo.Spined, typeof(SpinedHides), typeof(SpinedLeather), true, 1049687),
                new ResourceInfo(1049355, "Horned", (Core.AOS ? 0x845 : 0x227), 80.0, CraftAttributeInfo.Horned, typeof(HornedHides), typeof(HornedLeather), true, 1049688),
                new ResourceInfo(1049356, "Barbed", (Core.AOS ? 0x851 : 0x1C1), 99.0, CraftAttributeInfo.Barbed, typeof(BarbedHides), typeof(BarbedLeather), true, 1049689)));

            // Wood
            ResourceTypes.Add(new ResourceType("Wood",
                new ResourceInfo(1011542, "Normal", 0x0, 0.0, 49.0, CraftAttributeInfo.Blank, typeof(Log), typeof(Board), true, 1027134),
                new ResourceInfo(1072533, "Oak", 0x7DA, 65.0, 30.0, CraftAttributeInfo.OakWood, typeof(OakLog), typeof(OakBoard), true, 1075063),
                new ResourceInfo(1072534, "Ash", 0x4A7, 80.0, 10.0, CraftAttributeInfo.AshWood, typeof(AshLog), typeof(AshBoard), true, 1075064),
                new ResourceInfo(1072535, "Yew", 0x4A8, 95.0, 5.0, CraftAttributeInfo.YewWood, typeof(YewLog), typeof(YewBoard), true, 1075065),
                new ResourceInfo(1072536, "Heartwood", 0x4A9, 100.0, 3.0, CraftAttributeInfo.Heartwood, typeof(HeartwoodLog), typeof(HeartwoodBoard), true, 1075066),
                new ResourceInfo(1072538, "Bloodwood", 0x4AA, 100.0, 2.0, CraftAttributeInfo.Bloodwood, typeof(BloodwoodLog), typeof(BloodwoodBoard), true, 1075067),
                new ResourceInfo(1072539, "Frostwood", 0x47F, 100.0, 1.0, CraftAttributeInfo.Frostwood, typeof(FrostwoodLog), typeof(FrostwoodBoard), true, 1075068)));

            // Sand
            ResourceTypes.Add(new ResourceType("Sand",
                new ResourceInfo(1044626, "Sand", 0x0, 100.0, CraftAttributeInfo.Blank, typeof(Sand))));
        }
    }
}
