using System;

namespace Server.Gumps
{
    public class GreyLeftArrow : GumpButton
    {
        public GreyLeftArrow(Int32 x, Int32 y) : this(x, y, null) { }
        public GreyLeftArrow(Int32 x, Int32 y, GumpResponse callback) : base(x, y, 0x25EA, 0x25EB, -1, GumpButtonType.Reply, 0, callback) { }
    }
}
