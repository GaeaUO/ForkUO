using System;
using System.Collections.Generic;

namespace Server.Crafting
{
    public class BonusResourceType
    {
        private String _name;
        public String Name { get { return _name; } }

        private List<String> _propertiesRequired = new List<String>();
        public List<String> PropertiesRequired { get { return _propertiesRequired; } }

        private List<BonusResourceInfo> _bonusResourceInfos = new List<BonusResourceInfo>();
        public List<BonusResourceInfo> BonusResources { get { return _bonusResourceInfos; } }

        public BonusResourceType(String name, params BonusResourceInfo[] bonusResources)
        {
            _name = name;
            _bonusResourceInfos = new List<BonusResourceInfo>(bonusResources);
        }

        public void DefineRequiredProperties(params String[] propertiesRequired)
        {
            _propertiesRequired = new List<String>(propertiesRequired);
        }
    }
}
