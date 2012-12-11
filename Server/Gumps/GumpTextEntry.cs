/***************************************************************************
*                              GumpTextEntry.cs
*                            -------------------
*   begin                : May 1, 2002
*   copyright            : (C) The RunUO Software Team
*   email                : info@runuo.com
*
*   $Id: GumpTextEntry.cs 4 2006-06-15 04:28:39Z mark $
*
***************************************************************************/








/***************************************************************************
*
*   This program is free software; you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation; either version 2 of the License, or
*   (at your option) any later version.
*
***************************************************************************/
using System;
using Server.Network;

namespace Server.Gumps
{
    public class GumpTextEntry : GumpEntry
    {
        private static readonly byte[] m_LayoutName = Gump.StringToBuffer("textentry");
        private int m_X, m_Y;
        private int m_Width, m_Height;
        private int m_Hue;
        private int m_EntryID;
        private string m_InitialText;
        public GumpTextEntry(int x, int y, int width, int height, int hue, int entryID, string initialText)
        {
            this.m_X = x;
            this.m_Y = y;
            this.m_Width = width;
            this.m_Height = height;
            this.m_Hue = hue;
            this.m_EntryID = entryID;
            this.m_InitialText = initialText;
        }

        public int X
        {
            get
            {
                return this.m_X;
            }
            set
            {
                this.Delta(ref this.m_X, value);
            }
        }
        public int Y
        {
            get
            {
                return this.m_Y;
            }
            set
            {
                this.Delta(ref this.m_Y, value);
            }
        }
        public int Width
        {
            get
            {
                return this.m_Width;
            }
            set
            {
                this.Delta(ref this.m_Width, value);
            }
        }
        public int Height
        {
            get
            {
                return this.m_Height;
            }
            set
            {
                this.Delta(ref this.m_Height, value);
            }
        }
        public int Hue
        {
            get
            {
                return this.m_Hue;
            }
            set
            {
                this.Delta(ref this.m_Hue, value);
            }
        }
        public int EntryID
        {
            get
            {
                return this.m_EntryID;
            }
            set
            {
                this.Delta(ref this.m_EntryID, value);
            }
        }
        public string InitialText
        {
            get
            {
                return this.m_InitialText;
            }
            set
            {
                this.Delta(ref this.m_InitialText, value);
            }
        }
        public override string Compile()
        {
            return String.Format("{{ textentry {0} {1} {2} {3} {4} {5} {6} }}", this.m_X, this.m_Y, this.m_Width, this.m_Height, this.m_Hue, this.m_EntryID, this.Parent.Intern(this.m_InitialText));
        }

        public override void AppendTo(IGumpWriter disp)
        {
            disp.AppendLayout(m_LayoutName);
            disp.AppendLayout(this.m_X);
            disp.AppendLayout(this.m_Y);
            disp.AppendLayout(this.m_Width);
            disp.AppendLayout(this.m_Height);
            disp.AppendLayout(this.m_Hue);
            disp.AppendLayout(this.m_EntryID);
            disp.AppendLayout(this.Parent.Intern(this.m_InitialText));

            disp.TextEntries++;
        }
    }
}