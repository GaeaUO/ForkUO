using System;

namespace Server.Gumps
{
    public class GreyLeftArrow : GumpButton
    {
        public GreyLeftArrow(Int32 x, Int32 y, ButtonResponse callback)
            : this(x, y, callback, null) { }

        public GreyLeftArrow(Int32 x, Int32 y, ButtonResponse callback, object callbackParams)
            : base(x, y, 0x25EA, 0x25EB, -1, GumpButtonType.Reply, 0, callback, callbackParams)
        {
        }
    }
}
