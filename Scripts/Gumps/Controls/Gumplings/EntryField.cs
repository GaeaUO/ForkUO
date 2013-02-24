using System;

namespace Server.Gumps
{
    public class EntryField : Gumpling
    {
        public EntryField(Int32 x, Int32 y, Int32 width, String name, Int32 textEntryHue, String defaultValue) : base(x, y)
        {
            Add(new GumpImageTiled(x, y, width, 23, 0xA40));
            Add(new GumpImageTiled(x + 1, y + 1, width - 2, 21, 0xBBC));
            Add(new GumpTextEntry(x + 5, y + 1, width - 5, 21, textEntryHue, defaultValue, name));
        }
    }
}
