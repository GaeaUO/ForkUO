using System;
using System.Collections.Generic;

using Server.Gumps;

namespace Server.Resources
{
    public class RadioGumpling : Gumpling
    {
        public RadioGumpling(Int32 x, Int32 y, String name, Boolean state, String label) : base(x, y)
        {
            Add(new GumpRadio(0, 2, 0x146E, 0x146F, state, name));
            Add(new GumpLabel(25, 0, 0x0, label));
        }
    }
}
