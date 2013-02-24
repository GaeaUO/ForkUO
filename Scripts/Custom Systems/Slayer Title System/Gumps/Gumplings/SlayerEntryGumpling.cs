using System;

using Server.Gumps;

namespace CustomsFramework.Systems.SlayerTitleSystem
{
    public class SlayerEntryGumpling : Gumpling
    {
        public event GumpResponse OnEdit;
        public event GumpResponse OnRemove;

        private Int32 _index;
        public Int32 Index { get { return _index; } }

        public SlayerEntryGumpling(Int32 index, Int32 x, Int32 y, String entryName, Int32 entryHue) : base(x, y)
        {
            _index = index;

            Add(new GumpImageTiled(x, y, 285, 23, 0xA40));
            Add(new GumpImageTiled(x + 1, y + 1, 283, 21, 0xBBC));
            Add(new GumpLabelCropped(x + 5, y + 1, 275, 16, entryHue, entryName));

            GumpButton editButton = new GumpButton(x + 286, y, 0xFBD, 0xFBF, -1, GumpButtonType.Reply, 0);
            editButton.OnGumpResponse += editButton_OnGumpResponse;
            Add(editButton);

            GumpButton removeButton = new GumpButton(x + 317, y, 0xFB4, 0xFB6, -1, GumpButtonType.Reply, 0);
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
