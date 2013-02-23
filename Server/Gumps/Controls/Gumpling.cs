using System;
using System.Collections.Generic;

namespace Server.Gumps
{
    public abstract class Gumpling
    {
        private int _X, _Y;

        private readonly List<GumpEntry> _Entries;

        public Gumpling(int x, int y)
        {
            _X = x;
            _Y = y;

            this._Entries = new List<GumpEntry>();
        }

        public void Add(GumpEntry g)
        {
            if (!this._Entries.Contains(g))
                this._Entries.Add(g);
        }

        public void AddToGump(Gump gump)
        {
            foreach (GumpEntry g in _Entries)
            {
                if (g.Parent != gump)
                {
                    g.Parent = gump;
                }
                else if (!gump.Entries.Contains(g))
                {
                    if (g is GumpAlphaRegion)
                    {
                        ((GumpAlphaRegion)g).X += _X;
                        ((GumpAlphaRegion)g).Y += _Y;
                    }
                    else if (g is GumpBackground)
                    {
                        ((GumpBackground)g).X += _X;
                        ((GumpBackground)g).Y += _Y;
                    }
                    else if (g is GumpHtml)
                    {
                        ((GumpHtml)g).X += _X;
                        ((GumpHtml)g).Y += _Y;
                    }
                    else if (g is GumpHtmlLocalized)
                    {
                        ((GumpHtmlLocalized)g).X += _X;
                        ((GumpHtmlLocalized)g).Y += _Y;
                    }
                    else if (g is GumpImage)
                    {
                        ((GumpImage)g).X += _X;
                        ((GumpImage)g).Y += _Y;
                    }
                    else if (g is GumpImageTiled)
                    {
                        ((GumpImageTiled)g).X += _X;
                        ((GumpImageTiled)g).Y += _Y;
                    }
                    else if (g is GumpItem)
                    {
                        ((GumpItem)g).X += _X;
                        ((GumpItem)g).Y += _Y;
                    }
                    else if (g is GumpLabel)
                    {
                        ((GumpLabel)g).X += _X;
                        ((GumpLabel)g).Y += _Y;
                    }
                    else if (g is GumpLabelCropped)
                    {
                        ((GumpLabelCropped)g).X += _X;
                        ((GumpLabelCropped)g).Y += _Y;
                    }

                    gump.Invalidate();
                    gump.Add(g);
                }
            }
        }

        public void RemoveFromGump(Gump gump)
        {
            foreach (GumpEntry g in _Entries)
                gump.Remove(g);
        }
    }
}