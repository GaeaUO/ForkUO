using System;
using Server.Network;

namespace Server.Gumps
{
    public delegate void CheckResponse(bool selected);

    public delegate void CheckParamResponse(bool selected, object obj);

    public class GumpCheck : GumpEntry, IInputEntry
    {
        private static readonly byte[] _LayoutName = Gump.StringToBuffer("checkbox");
        private object _Callback;
        private object _CallbackParam;
        private int _ID1, _ID2;
        private bool _InitialState;
        private string _Name;
        private int _SwitchID;
        private int _X, _Y;

        public GumpCheck(int x, int y, int inactiveID, int activeID, bool initialState, int switchID, string name,
                         CheckResponse callback, object callbackParam)
        {
            this._X = x;
            this._Y = y;
            this._ID1 = inactiveID;
            this._ID2 = activeID;
            this._InitialState = initialState;
            this._SwitchID = switchID;
            this._Name = name;
            this._Callback = callback;
            this._CallbackParam = callbackParam;
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

        public int SwitchID
        {
            get { return this._SwitchID; }
            set { this.Delta(ref this._SwitchID, value); }
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

        public void Invoke(bool switched)
        {
            CheckResponse callback = this._Callback as CheckResponse;

            if (callback != null)
                callback(switched);
            else
            {
                CheckParamResponse response = this._CallbackParam as CheckParamResponse;

                if (response != null)
                    response(switched, this._CallbackParam);
            }
        }

        public override string Compile()
        {
            return String.Format("{{ checkbox {0} {1} {2} {3} {4} {5} }}", this._X, this._Y, this._ID1, this._ID2,
                                 this._InitialState ? 1 : 0, this._SwitchID);
        }

        public override void AppendTo(IGumpWriter disp)
        {
            disp.AppendLayout(_LayoutName);
            disp.AppendLayout(this._X);
            disp.AppendLayout(this._Y);
            disp.AppendLayout(this._ID1);
            disp.AppendLayout(this._ID2);
            disp.AppendLayout(this._InitialState);
            disp.AppendLayout(this._SwitchID);

            disp.Switches++;
        }
    }
}