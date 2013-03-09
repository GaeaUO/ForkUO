using System;
using System.Collections.Generic;
using System.Reflection;

using Server.Gumps;
using Server.Items;

namespace Server.Resources
{
    public class CraftInfoGumpling : GoldRimmedFrame
    {
        private const Int32 width = 480;
        private const Int32 height = 151;

        private const Int32 HUE_ValidEntry = 0;
        private const Int32 HUE_InvalidEntry = 0x22;

        private Dictionary<String, String> _Values = new Dictionary<String, String>();

        public CraftInfoGumpling(Int32 x, Int32 y, CraftAttributeInfo info, Boolean validate) : base(x, y, width, height)
        {
            Add(new CenteredLabel(0, 3, width, 0x0, "Crafting Attributes"));

            PropertyInfo[] props = info.GetType().GetProperties();

            foreach (PropertyInfo pi in props)
                if (pi.GetValue(info) is Int32)
                    _Values[pi.Name] = pi.GetValue(info).ToString();

            BuildDisplay(validate);
        }

        public CraftInfoGumpling(Int32 x, Int32 y, Dictionary<String, String> values, Boolean validate) : base(x, y, width, height)
        {
            _Values = values;

            BuildDisplay(validate);
        }

        private void BuildDisplay(Boolean validate)
        {
            List<String> fieldNames = new List<String>();

            foreach (KeyValuePair<String, String> pair in _Values)
                fieldNames.Add(pair.Key);

            fieldNames.Sort();

            Int32 pageCount = fieldNames.Count / 10 + (fieldNames.Count % 10 > 0 ? 1 : 0);

            for (Int32 page = 0; page < pageCount; page++)
            {
                Add(new GumpPage(page + 1));

                if (page > 0)
                    Add(new GreyLeftArrow(5, 5, GumpButtonType.Page, page));

                for (Int32 column = 0; column < 2; column++)
                {
                    for (Int32 row = 0; row < 5; row++)
                    {
                        Int32 index = (page * 10) + (column * 5) + row;

                        if (index >= fieldNames.Count)
                            return;

                        Int32 hue = !validate || ResourceDefinitionGump.IsInteger(_Values[fieldNames[index]], true) ? HUE_ValidEntry : HUE_InvalidEntry;

                        Add(new GumpLabelCropped(5 + (column * 242), 28 + (row * 22), 180, 18, 0x0, FormatLabel(fieldNames[index])));
                        Add(new EntryField(180 + (column * 242), 25 + (row * 22), 50, fieldNames[index], hue, _Values[fieldNames[index]]));
                    }
                }

                if (page < pageCount - 1)
                    Add(new GreyRightArrow(width - 22, 5, GumpButtonType.Page, page + 2));
            }
        }

        private String FormatLabel(String label)
        {
            String output = "";

            foreach (Char c in label)
            {
                if (Char.IsUpper(c) && output != "")
                    output = String.Format("{0} ", output);

                output = String.Format("{0}{1}", output, c);
            }

            return output;
        }

        public CraftAttributeInfo GetCraftInfo()
        {
            CraftAttributeInfo info = new CraftAttributeInfo();
            PropertyInfo[] props = info.GetType().GetProperties();

            foreach (PropertyInfo pi in props)
            {
                if (pi.GetValue(info) is Int32)
                {
                    Int32 value = 0;
                    Int32.TryParse(RootParent.GetTextEntry(pi.Name), out value);

                    pi.SetValue(info, value);
                }
            }

            return info;
        }

        public Boolean IsValid
        {
            get
            {
                Boolean result = true;

                PropertyInfo[] props = typeof(CraftAttributeInfo).GetProperties();

                foreach (PropertyInfo pi in props)
                    if (pi.PropertyType == typeof(Int32))
                        result &= ResourceDefinitionGump.IsInteger(RootParent.GetTextEntry(pi.Name), true);

                return result;
            }
        }
    }
}
