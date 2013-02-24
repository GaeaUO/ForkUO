using System;

namespace Server.Gumps
{
    public class ApplyCancelGumpling : Gumpling
    {
        public ApplyCancelGumpling(Int32 x, Int32 y, ButtonResponse applyCallback, ButtonResponse cancelCallback) : base(x, y)
        {
            Add(new GumpButton(x, y, 0x1454, 0x1455, -1, GumpButtonType.Reply, 0, applyCallback, null));
            Add(new GumpButton(x + 85, y, 0x1450, 0x1451, 0, GumpButtonType.Reply, 0, cancelCallback, null));
        }
    }
}
