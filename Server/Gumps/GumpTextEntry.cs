using System;
using Server.Network;

namespace Server.Gumps
{
    public delegate void TextResponse(string text);

    public delegate void TextParamResponse(string text, object obj);

    public class GumpTextEntry : GumpEntry, IInputEntry
    {
        private static readonly byte[] _LayoutName = Gump.StringToBuffer("textentry");
        private object _Callback;
        private object _CallbackParam;
        private int _EntryID;
        private int _Height;
        private int _Hue;
        private string _InitialText;
        private string _Name;
        private int _Width;
        private int _X, _Y;

        public GumpTextEntry(int x, int y, int width, int height, int hue, int entryID, string name,
                             TextResponse callback, object callbackParam, string initialText)
        {
            this._X = x;
            this._Y = y;
            this._Width = width;
            this._Height = height;
            this._Hue = hue;
            this._EntryID = entryID;
            this._Name = name;
            this._Callback = callback;
            this._CallbackParam = callbackParam;
            this._InitialText = initialText;
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

        public void Invoke(string input)
        {
            TextResponse callback = this._Callback as TextResponse;

            if (callback != null)
                callback(input);
            else
            {
                TextParamResponse response = this._CallbackParam as TextParamResponse;

                if (response != null)
                    response(input, this._CallbackParam);
            }
        }

        public override string Compile()
        {
            return String.Format("{{ textentry {0} {1} {2} {3} {4} {5} {6} }}", this._X, this._Y, this._Width,
                                 this._Height, this._Hue, this._EntryID, this.Parent.Intern(this._InitialText));
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

            disp.TextEntries++;
        }
    }
}