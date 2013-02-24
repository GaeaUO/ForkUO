using System;
using System.Collections.Generic;

namespace Server.Gumps
{
    public abstract class Gumpling : IGumpContainer, IGumpComponent
    {
        private int _X, _Y;

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