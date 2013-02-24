using System;

namespace Server.Gumps
{
    public class GreyRightArrow : GumpButton
    {
        public GreyRightArrow(Int32 x, Int32 y) : this(x, y, null) { }

        public GreyRightArrow(Int32 x, Int32 y, GumpResponse callback) : base(x, y, 0x25E6, 0x25E7, -1, GumpButtonType.Reply, 0, callback) { }
    }
}
