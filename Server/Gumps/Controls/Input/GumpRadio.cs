using System;
using Server.Network;

namespace Server.Gumps
{
    public delegate void RadioResponse(GumpEntry entry, bool switched, object param);

    public class GumpRadio : GumpEntry, IInputEntry
    {
        private static readonly byte[] _LayoutName = Gump.StringToBuffer("radio");
        private object _Callback;
        private object _CallbackParam;
        private int _ID1, _ID2;
        private bool _InitialState;
        private string _Name;
        private int _EntryID;
        private int _X, _Y;

        public GumpRadio(int x, int y, int inactiveID, int activeID, bool initialState, string name)
            : this(x, y, inactiveID, activeID, initialState, -1, null, null, name) { }

        public GumpRadio(int x, int y, int inactiveID, int activeID, bool initialState, RadioResponse callback, string name)
            : this(x, y, inactiveID, activeID, initialState, -1, callback, null, name) { }

        public GumpRadio(int x, int y, int inactiveID, int activeID, bool initialState, RadioResponse callback, object callbackParam, string name)
            : this(x, y, inactiveID, activeID, initialState, -1, callback, callbackParam, name) { }

        public GumpRadio(int x, int y, int inactiveID, int activeID, bool initialState, int switchID)
            : this(x, y, inactiveID, activeID, initialState, switchID, null, null, "") { }

        public GumpRadio(int x, int y, int inactiveID, int activeID, bool initialState, int switchID, RadioResponse callback)
            : this(x, y, inactiveID, activeID, initialState, switchID, callback, null, "") { }

        public GumpRadio(int x, int y, int inactiveID, int activeID, bool initialState, int switchID, RadioResponse callback, object callbackParam)
            : this(x, y, inactiveID, activeID, initialState, switchID, callback, callbackParam, "") { }

        public GumpRadio(int x, int y, int inactiveID, int activeID, bool initialState, int switchID, RadioResponse callback, object callbackParam, string name)
        {
            this._X = x;
            this._Y = y;
            this._ID1 = inactiveID;
            this._ID2 = activeID;
            this._InitialState = initialState;
            this._EntryID = switchID;
            this._Callback = callback;
            this._CallbackParam = callbackParam;
            this._Name = (name != null ? name : "");
        }

        public int X
        {
            get { return this._X; }
            set { this.Delta(ref this._X, value); }
        }

        public int Y
        {
            get { return this._Y; }
            set { this.Delta(ref this._Y, value); }
        }

        public int InactiveID
        {
            get { return this._ID1; }
            set { this.Delta(ref this._ID1, value); }
        }

        public int ActiveID
        {
            get { return this._ID2; }
            set { this.Delta(ref this._ID2, value); }
        }

        public bool InitialState
        {
            get { return this._InitialState; }
            set { this.Delta(ref this._InitialState, value); }
        }

        public int EntryID
        {
            get { return this._EntryID; }
            set { this.Delta(ref this._EntryID, value); }
        }

        public string Name
        {
            get { return this._Name; }
            set { this.Delta(ref this._Name, value); }
        }

        public object Callback
        {
            get { return this._Callback; }
            set { this.Delta(ref this._Callback, value); }
        }

        public object CallbackParam
        {
            get { return this._CallbackParam; }
            set { this.Delta(ref this._CallbackParam, value); }
        }

        public void Invoke()
        {
            RadioResponse callback = this._Callback as RadioResponse;

            if (callback != null)
                callback(this, this.InitialState, this._CallbackParam);
        }

        public override string Compile()
        {
            return String.Format("{{ radio {0} {1} {2} {3} {4} {5} }}", this._X, this._Y, this._ID1, this._ID2,
                                 this._InitialState ? 1 : 0, this._EntryID);
        }

        public override void AppendTo(IGumpWriter disp)
        {
            disp.AppendLayout(_LayoutName);
            disp.AppendLayout(this._X);
            disp.AppendLayout(this._Y);
            disp.AppendLayout(this._ID1);
            disp.AppendLayout(this._ID2);
            disp.AppendLayout(this._InitialState);
            disp.AppendLayout(this._EntryID);

            disp.Switches++;
        }
    }
}