using System;
using System.Linq;

using Server.Crafting;

namespace Server
{
    public class ResourceString : IDynamicEnum
    {
        private String _group = "";
        public String Group { get { return _group; } set { _group = value; } }

        private String _value = "";
        public String Value { get { return _value; } set { _value = value; } }

        public ResourceString(String group, String value)
        {
            _group = group;
            _value = value;
        }

        public String[] Values { get { return Resources.GetResources(_group); } }
        public Boolean IsValid { get { return Values.FirstOrDefault(mod => mod.Equals(_value, StringComparison.OrdinalIgnoreCase)) != null; } }
    }
}
