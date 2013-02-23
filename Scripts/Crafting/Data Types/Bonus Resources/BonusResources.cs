using System;
using System.Collections.Generic;
using System.Linq;

using Server.Items;

namespace Server.Crafting
{
    public class BonusResources
    {
        private static List<BonusResourceType> _types = new List<BonusResourceType>();
        public static List<BonusResourceType> BonusResourceTypes { get { return _types; } }

        public static BonusResourceInfo LookupResource(String groupName, String resource)
        {
            BonusResourceType type = BonusResources.BonusResourceTypes.FirstOrDefault(mod => mod.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
            BonusResourceInfo info = null;

            if (type != null)
            {
                info = type.BonusResources.FirstOrDefault(mod => mod.Name.Equals(resource, StringComparison.OrdinalIgnoreCase));

                if (info == null)
                    info = new BonusResourceInfo();
            }
            else
                info = new BonusResourceInfo();

            return info;
        }

        public static BonusResourceInfo RandomBonusResource(String groupName)
        {
            BonusResourceType type = BonusResources.BonusResourceTypes.FirstOrDefault(mod => mod.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
            BonusResourceInfo info = null;

            if (type != null && type.BonusResources.Count > 0)
            {
                if (type.BonusResources.Count == 1)
                    info = type.BonusResources[0];
                else if (type.BonusResources[0].ChanceToObtain == 100.0)
                    info = type.BonusResources[Utility.RandomMinMax(0, type.BonusResources.Count - 1)];
                else
                {
                    Double chance = Utility.RandomDouble() * 100;

                    for (Int32 i = 0; i < type.BonusResources.Count; i++)
                    {
                        if (chance <= type.BonusResources[i].ChanceToObtain)
                        {
                            info = type.BonusResources[i];
                            break;
                        }

                        chance -= type.BonusResources[i].ChanceToObtain;
                    }
                }
            }

            return info;
        }

        public static void Configure()
        {
        }
    }
}
