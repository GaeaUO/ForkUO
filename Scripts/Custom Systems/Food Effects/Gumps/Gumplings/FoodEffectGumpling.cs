using System;

using Server.Gumps;

namespace CustomsFramework.Systems.FoodEffects
{
    public class FoodEffectGumpling : Gumpling
    {
        public FoodEffectGumpling(Int32 x, Int32 y, Int32 width, String name, Int32 textEntryHue, String defaultValue) : base(x, y)
        {
            Add(new GumpLabel(x, y + 3, 0x0, name));
            Add(new GumpImageTiled(x + 100, y, width, 23, 0xA40));
            Add(new GumpImageTiled(x + 101, y + 1, width - 2, 21, 0xBBC));
            Add(new GumpTextEntry(x + 105, y + 1, width - 5, 21, textEntryHue, defaultValue, name));
        }
    }
}