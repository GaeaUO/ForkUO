using System;
using Server.Network;

namespace Server.Gumps
{
    public class GumpImageTileButton : GumpEntry, IInputEntry
    {
        private static readonly byte[] _LayoutName = Gump.StringToBuffer("buttontileart");
        private static readonly byte[] _LayoutTooltip = Gump.StringToBuffer(" }{ tooltip");
        private int _ButtonID;
        private object _Callback;
        private object _CallbackParam;
        private int _Height;
        private int _Hue;
        private int _ID1, _ID2;
        private int _ItemID;
        private int _LocalizedTooltip;
        private string _Name;
        private int _Param;
        private GumpButtonType _Type;
        private int _Width;
        private int _X, _Y;

        public GumpImageTileButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type, int param, int itemID, int hue, int width, int height, ButtonResponse callback, object callbackParam, int localizedTooltip = -1, string name = "")
        {
            this._X = x;
            this._Y = y;
            this._ID1 = normalID;
            this._ID2 = pressedID;
            this._ButtonID = buttonID;
            this._Type = type;
            this._Param = param;

            this._ItemID = itemID;
            this._Hue = hue;
            this._Width = width;
            this._Height = height;

            this._LocalizedTooltip = localizedTooltip;

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
                if (this._Type != value)
                {
                    this._Type = value;

                    Gump parent = this.Parent;

                    if (parent != null)
                    {
                        parent.Invalidate();
                    }
                }
            }
        }

        public int Param
        {
            get { return this._Param; }
            set { this.Delta(ref this._Param, value); }
        }

        public int ItemID
        {
            get { return this._ItemID; }
            set { this.Delta(ref this._ItemID, value); }
        }

        public int Hue
        {
            get { return this._Hue; }
            set { this.Delta(ref this._Hue, value); }
        }

        public int Width
        {
            get { return this._Width; }
            set { this.Delta(ref this._Width, value); }
        }

        public int Height
        {
            get { return this._Height; }
            set { this.Delta(ref this._Height, value); }
        }

        public int LocalizedTooltip
        {
            get { return this._LocalizedTooltip; }
            set { this.Delta(ref this._LocalizedTooltip, value); }
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
            return this._LocalizedTooltip > 0
                       ? String.Format(
                           "{{ buttontileart {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} }}{{ tooltip {11} }}",
                           this._X, this._Y, this._ID1, this._ID2, (int) this._Type, this._Param, this._ButtonID,
                           this._ItemID, this._Hue, this._Width, this._Height, this._LocalizedTooltip)
                       : String.Format("{{ buttontileart {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} }}", this._X,
                                       this._Y, this._ID1, this._ID2, (int) this._Type, this._Param,
                                       this._ButtonID, this._ItemID, this._Hue, this._Width, this._Height);
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

            disp.AppendLayout(this._ItemID);
            disp.AppendLayout(this._Hue);
            disp.AppendLayout(this._Width);
            disp.AppendLayout(this._Height);

            if (this._LocalizedTooltip > 0)
            {
                disp.AppendLayout(_LayoutTooltip);
                disp.AppendLayout(this._LocalizedTooltip);
            }
        }
    }
}