using System;
using System.Collections.Generic;

using Server.Gumps;

namespace Server.Resources
{
    public class CheckboxGumpling : Gumpling
    {
        public CheckboxGumpling(Int32 x, Int32 y, String name, Boolean state, String label) : base(x, y)
        {
            Add(new GumpCheck(0, 2, 0x146E, 0x146F, state, name));
            Add(new GumpLabel(25, 0, 0x0, label));
        }
    }
}
