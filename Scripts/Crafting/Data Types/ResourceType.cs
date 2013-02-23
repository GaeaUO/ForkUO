using System;
using System.Collections.Generic;

namespace Server.Crafting
{
    public class ResourceType
    {
        private String _name;
        public String Name { get { return _name; } }

        private List<ResourceInfo> _resourceInfos = new List<ResourceInfo>();
        public List<ResourceInfo> Resources { get { return _resourceInfos; } }

        public ResourceType(String name, params ResourceInfo[] resources)
        {
            _name = name;
            _resourceInfos = new List<ResourceInfo>(resources);
        }
    }
}
