using System;
using System.Collections.Generic;

using Server.Gumps;

namespace Server.Resources
{
    public class TypeGumpling : GoldRimmedFrame
    {
        public TypeGumpling(Int32 x, Int32 y, Int32 width, Int32 height, String preceding, List<Int32> hues, List<String> values) : base(x, y, width, height)
        {
            Add(new CenteredLabel(0, 3, width, 0x0, String.Format("{0} Definition", preceding)));
            Add(new GumpLabel(5, 28, 0x0, "Type"));
            Add(new GumpLabel(5, 50, 0x0, "Cliloc"));
            Add(new GumpLabel(5, 72, 0x0, "Name"));
            Add(new EntryField(55, 25, 175, String.Format("{0} Type", preceding), (hues.Count > 0 ? hues[0] : 0x0), (values.Count > 0 ? values[0] : "")));
            Add(new EntryField(55, 47, 95, String.Format("{0} Cliloc", preceding), (hues.Count > 1 ? hues[1] : 0x0), (values.Count > 1 ? values[1] : "")));
            Add(new EntryField(55, 69, 175, String.Format("{0} Name", preceding), (hues.Count > 2 ? hues[2] : 0x0), (values.Count > 2 ? values[2] : "")));
        }
    }
}
