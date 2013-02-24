using System;

namespace Server.Gumps
{
    public class SaveCancelGumpling : Gumpling
    {
        public SaveCancelGumpling(Int32 x, Int32 y, ButtonResponse saveCallback, ButtonResponse cancelCallback) : base(x, y)
        {
            Add(new GumpButton(x, y, 0x1452, 0x1453, -1, GumpButtonType.Reply, 0, saveCallback, null));
            Add(new GumpButton(x + 85, y, 0x1450, 0x1451, 0, GumpButtonType.Reply, 0, cancelCallback, null));
        }
    }
}
