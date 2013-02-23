using System;

namespace Server.Crafting
{
    public class BonusResourceInfo
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

        public BonusResourceInfo() { }

        public BonusResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired) :
            this(number, name, hue, skillRequired, 100.0) { }

        public BonusResourceInfo(Int32 number, String name, Int32 hue, Double skillRequired, Double chanceToObtain)
        {
            _number = number;
            _name = name;
            _hue = hue;
            _skillRequired = skillRequired;
            _chanceToObtain = chanceToObtain;
        }
    }
}
