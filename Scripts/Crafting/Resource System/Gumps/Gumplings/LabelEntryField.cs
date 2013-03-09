using System;
using System.Collections.Generic;

using Server.Gumps;

namespace Server.Resources
{
    public class LabelEntryField : Gumpling
    {
        public LabelEntryField(Int32 x, Int32 y, Int32 labelWidth, Int32 entryWidth, String name, Int32 entryHue, object initialValue) : base(x, y)
        {
            Add(new GumpLabelCropped(0, 3, labelWidth, 18, 0x0, name));
            Add(new EntryField(labelWidth + 5, 0, entryWidth, name, entryHue, (initialValue != null ? initialValue.ToString() : "")));
        }
    }
}
