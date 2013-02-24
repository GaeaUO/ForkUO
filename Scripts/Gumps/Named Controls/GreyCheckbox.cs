using System;

namespace Server.Gumps
{
    public class GreyCheckbox : GumpCheck
    {
        public GreyCheckbox(Int32 x, Int32 y, String name, Boolean initialState)
            : this(x, y, name, initialState, null, null) { }

        public GreyCheckbox(Int32 x, Int32 y, String name, Boolean initialState, CheckResponse callback)
            : this(x, y, name, initialState, callback, null) { }

        public GreyCheckbox(Int32 x, Int32 y, String name, Boolean initialState, CheckResponse callback, object callbackParam)
            : base(x, y, 0x25F8, 0x25FC, initialState, callback, callbackParam, name)
        {
        }
    }
}
