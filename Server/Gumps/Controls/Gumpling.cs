using System;
using System.Collections.Generic;

namespace Server.Gumps
{
    public abstract class Gumpling : IGumpContainer, IGumpComponent
    {
        private int _X = 0;
        private int _Y = 0;

        public int X
        {
            get { return _X; }
            set
            {
                int offset = value - X;
                _X = value;

                foreach (GumpEntry g in _Entries)
                    g.X += offset;
            }
        }

        public int Y
        {
            get { return _Y; }
            set
            {
                int offset = value - Y;
                _Y = value;

                foreach (GumpEntry g in _Entries)
                    g.Y += offset;
            }
        }

        private readonly List<GumpEntry> _Entries;

        private IGumpContainer m_Parent;

        public IGumpContainer Parent
        {
            get { return this.m_Parent; }
            set
            {
                if (this.m_Parent != value)
                {
                    if (this.m_Parent != null)
                        this.m_Parent.Remove(this);

                    this.m_Parent = value;

                    if (this.m_Parent != null)
                        this.m_Parent.Add(this);
                }
            }
        }

        public Gumpling(int x, int y)
        {
            _X = x;
            _Y = y;

            this._Entries = new List<GumpEntry>();
        }

        public Gump RootParent { get { return Parent.RootParent; } }

        public void Add(IGumpComponent g)
        {
            if (g.Parent == null)
                g.Parent = this;

            if (g is GumpEntry)
            {
                if (!this._Entries.Contains((GumpEntry)g))
                {
                    g.X += _X;
                    g.Y += _Y;

                    this._Entries.Add((GumpEntry)g);
                    this.Invalidate();
                }
            }
        }

        public void Remove(IGumpComponent g)
        {
            if (g is GumpEntry)
            {
                this._Entries.Remove((GumpEntry)g);
                g.Parent = null;
                this.Invalidate();
            }
        }

        public virtual void Invalidate()
        {
        }

        public void AddToGump(Gump gump)
        {
            foreach (GumpEntry g in _Entries)
                if (!gump.Entries.Contains(g))
                    gump.Add(g);
        }

        public void RemoveFromGump(Gump gump)
        {
            foreach (GumpEntry g in _Entries)
                gump.Remove(g);
        }
    }
}