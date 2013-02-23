using System;
using Server.Network;

namespace Server.Gumps
{
    public delegate void ButtonResponse();

    public delegate void ButtonParamResponse(object obj);

    public enum GumpButtonType
    {
        Page = 0,
        Reply = 1
    }

    public class GumpButton : GumpEntry, IInputEntry
    {
        private static readonly byte[] _LayoutName = Gump.StringToBuffer("button");
        private int _ButtonID;
        private object _Callback;
        private object _CallbackParam;
        private int _ID1, _ID2;
        private string _Name;
        private int _Param;
        private GumpButtonType _Type;
        private int _X, _Y;

        public GumpButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type, int param, ButtonResponse callback, object callbackParam, string name = "")
        {
            this._X = x;
            this._Y = y;
            this._ID1 = normalID;
            this._ID2 = pressedID;
            this._ButtonID = buttonID;
            this._Type = type;
            this._Param = param;
            this._Callback = callback;
            this._CallbackParam = callbackParam;
            this._Name = name;
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

        public int NormalID
        {
            get { return this._ID1; }
            set { this.Delta(ref this._ID1, value); }
        }

        public int PressedID
        {
            get { return this._ID2; }
            set { this.Delta(ref this._ID2, value); }
        }

        public int ButtonID
        {
            get { return this._ButtonID; }
            set { this.Delta(ref this._ButtonID, value); }
        }

        public GumpButtonType Type
        {
            get { return this._Type; }
            set
            {
                if (this._Type == value) return;

                this._Type = value;
                Gump parent = this.Parent;

                if (parent != null)
                {
                    parent.Invalidate();
                }
            }
        }

        public int Param
        {
            get { return this._Param; }
            set { this.Delta(ref this._Param, value); }
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
            ButtonResponse callback = this._Callback as ButtonResponse;

            if (callback != null)
                callback();
            else
            {
                ButtonParamResponse response = this._CallbackParam as ButtonParamResponse;

                if (response != null)
                    response(this._CallbackParam);
            }
        }

        public override string Compile()
        {
            return String.Format("{{ button {0} {1} {2} {3} {4} {5} {6} }}", this._X, this._Y, this._ID1, this._ID2,
                                 (int) this._Type, this._Param, this._ButtonID);
        }

        public override void AppendTo(IGumpWriter disp)
        {
            disp.AppendLayout(_LayoutName);
            disp.AppendLayout(this._X);
            disp.AppendLayout(this._Y);
            disp.AppendLayout(this._ID1);
            disp.AppendLayout(this._ID2);
            disp.AppendLayout((int) this._Type);
            disp.AppendLayout(this._Param);
            disp.AppendLayout(this._ButtonID);
        }
    }
}