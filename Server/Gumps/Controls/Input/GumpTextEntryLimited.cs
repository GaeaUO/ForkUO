using System;
using Server.Network;

namespace Server.Gumps
{
    public class GumpTextEntryLimited : GumpEntry, IInputEntry
    {
        private static readonly byte[] _LayoutName = Gump.StringToBuffer("textentrylimited");
        private object _Callback;
        private object _CallbackParam;
        private int _EntryID;
        private int _Height;
        private int _Hue;
        private string _InitialText;
        private string _Name;
        private int _Size;
        private int _Width;
        private int _X, _Y;

        public GumpTextEntryLimited(int x, int y, int width, int height, int hue, int entryID, string initialText, int size, TextResponse callback, object callbackParam, string name = "")
        {
            this._X = x;
            this._Y = y;
            this._Width = width;
            this._Height = height;
            this._Hue = hue;
            this._EntryID = entryID;
            this._InitialText = initialText;
            this._Size = size;
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

        public int Hue
        {
            get { return this._Hue; }
            set { this.Delta(ref this._Hue, value); }
        }

        public int EntryID
        {
            get { return this._EntryID; }
            set { this.Delta(ref this._EntryID, value); }
        }

        public string InitialText
        {
            get { return this._InitialText; }
            set { this.Delta(ref this._InitialText, value); }
        }

        public int Size
        {
            get { return this._Size; }
            set { this.Delta(ref this._Size, value); }
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
            TextResponse callback = this._Callback as TextResponse;

            if (callback != null)
                callback(this.InitialText);
            else
            {
                TextParamResponse response = this._CallbackParam as TextParamResponse;

                if (response != null)
                    response(this.InitialText, this._CallbackParam);
            }
        }

        public override string Compile()
        {
            return String.Format("{{ textentrylimited {0} {1} {2} {3} {4} {5} {6} {7} }}", this._X, this._Y, this._Width,
                                 this._Height, this._Hue, this._EntryID, this.Parent.Intern(this._InitialText),
                                 this._Size);
        }

        public override void AppendTo(IGumpWriter disp)
        {
            disp.AppendLayout(_LayoutName);
            disp.AppendLayout(this._X);
            disp.AppendLayout(this._Y);
            disp.AppendLayout(this._Width);
            disp.AppendLayout(this._Height);
            disp.AppendLayout(this._Hue);
            disp.AppendLayout(this._EntryID);
            disp.AppendLayout(this.Parent.Intern(this._InitialText));
            disp.AppendLayout(this._Size);

            disp.TextEntries++;
        }
    }
}