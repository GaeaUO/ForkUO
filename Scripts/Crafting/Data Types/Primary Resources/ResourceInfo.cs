using System;
using System.Collections.Generic;

namespace Server.Crafting
{
    public class ResourceInfo
    {
        private Int32 _number = 0;
        public Int32 Number { get { return _number; } }

        private String _name = "Unknown Resource";
        public String Name { get { return _name; } }

        private Int32 _hue = 0;
        public Int32 Hue { get { return _hue; } }

        private Double _skillRequired = 0.0;
        public Double SkillRequired { get { return _skillRequired; } }

        private Double _chanceToObtain = 100.0;
        public Double ChanceToObtain { get { return _chanceToObtain; } }

        private CraftAttributeInfo _attributeInfo = CraftAttributeInfo.Blank;
        public CraftAttributeInfo AttributeInfo { get { return _attributeInfo; } }

        private Int32 _label = 0;
        public Int32 Label { get { return _label; } }

        private Type _rawResource;
        public Type RawResource { get { return _rawResource; } }

        private Type _processedResource;
        public Type ProcessedResource { get { return _processedResource; } }

        private Boolean _useRawForCrafting = false;
        public Boolean UseRawForCrafting { get { return _useRawForCrafting; } }

        public ResourceInfo() { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, CraftAttributeInfo attributeInfo, Type rawResource) :
            this(number, name, hue, skillRequired, 100.0, attributeInfo, rawResource, null, true, 0) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, Double chanceToObtain, CraftAttributeInfo attributeInfo, Type rawResource) :
            this(number, name, hue, skillRequired, chanceToObtain, attributeInfo, rawResource, null, true, 0) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, CraftAttributeInfo attributeInfo, Type rawResource, Int32 label) :
            this(number, name, hue, skillRequired, 100.0, attributeInfo, rawResource, null, true, label) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, Double chanceToObtain, CraftAttributeInfo attributeInfo, Type rawResource, Int32 label) :
            this(number, name, hue, skillRequired, chanceToObtain, attributeInfo, rawResource, null, true, label) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, CraftAttributeInfo attributeInfo, Type rawResource, Type processedResource) :
            this(number, name, hue, skillRequired, 100.0, attributeInfo, rawResource, processedResource, false, 0) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, Double chanceToObtain, CraftAttributeInfo attributeInfo, Type rawResource, Type processedResource) :
            this(number, name, hue, skillRequired, chanceToObtain, attributeInfo, rawResource, processedResource, false, 0) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, CraftAttributeInfo attributeInfo, Type rawResource, Type processedResource, Int32 label) :
            this(number, name, hue, skillRequired, 100.0, attributeInfo, rawResource, processedResource, false, label) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, Double chanceToObtain, CraftAttributeInfo attributeInfo, Type rawResource, Type processedResource, Int32 label) :
            this(number, name, hue, skillRequired, chanceToObtain, attributeInfo, rawResource, processedResource, false, label) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, CraftAttributeInfo attributeInfo, Type rawResource, Type processedResource, Boolean useRawForCrafting) :
            this(number, name, hue, skillRequired, 100.0, attributeInfo, rawResource, processedResource,useRawForCrafting, 0) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, Double chanceToObtain, CraftAttributeInfo attributeInfo, Type rawResource, Type processedResource, Boolean useRawForCrafting) :
            this(number, name, hue, skillRequired, chanceToObtain, attributeInfo, rawResource, processedResource, useRawForCrafting, 0) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, CraftAttributeInfo attributeInfo, Type rawResource, Type processedResource, Boolean useRawForCrafting, Int32 label) :
            this(number, name, hue, skillRequired, 100.0, attributeInfo, rawResource, processedResource, useRawForCrafting, label) { }

        public ResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, Double chanceToObtain, CraftAttributeInfo attributeInfo, Type rawResource, Type processedResource, Boolean useRawForCrafting, Int32 label)
        {
            _number = number;
            _name = name;
            _hue = hue;
            _skillRequired = skillRequired;
            _chanceToObtain = chanceToObtain;
            _attributeInfo = attributeInfo;
            _rawResource = rawResource;
            _processedResource = processedResource;
            _useRawForCrafting = useRawForCrafting;
            _label = label;
        }
    }
}
