﻿using System;

using Server.Gumps;

namespace CustomsFramework.Systems.FoodEffects
{
    public class FoodEntryGumpling : Gumpling
    {
        public event GumpResponse OnEdit;
        public event GumpResponse OnRemove;

        private Int32 _index;
        public Int32 Index { get { return _index; } }

        public FoodEntryGumpling(Int32 index, Int32 x, Int32 y, String entryName) : base(x, y)
        {
            _index = index;

            Add(new GumpImageTiled(x, y, 180, 23, 0xA40));
            Add(new GumpImageTiled(x + 1, y + 1, 178, 21, 0xBBC));
            Add(new GumpLabelCropped(x + 5, y + 1, 175, 16, 0, entryName));

            GumpButton editButton = new GumpButton(x + 185, y, 0xFBD, 0xFBF, -1, GumpButtonType.Reply, 0);
            editButton.OnGumpResponse += editButton_OnGumpResponse;
            Add(editButton);

            GumpButton removeButton = new GumpButton(x + 215, y, 0xFB4, 0xFB6, -1, GumpButtonType.Reply, 0);
            removeButton.OnGumpResponse += removeButton_OnGumpResponse;
            Add(removeButton);
        }

        void editButton_OnGumpResponse(IGumpComponent sender, object param)
        {
            if (OnEdit != null)
                OnEdit(sender, param);
        }

        void removeButton_OnGumpResponse(IGumpComponent sender, object param)
        {
            if (OnRemove != null)
                OnRemove(sender, param);
        }
    }
}
