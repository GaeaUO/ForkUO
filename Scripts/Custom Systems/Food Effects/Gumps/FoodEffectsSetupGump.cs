using System;
using System.Collections.Generic;

using Server;
using Server.Gumps;

namespace CustomsFramework.Systems.FoodEffects
{
    public class FoodEffectsSetupGump : Gump
    {
        private const Int32 ID_Cancel_Button = 0;
        private const Int32 ID_Core_Enabled = 100;
        private const Int32 ID_Save_Button = 200;
        private const Int32 ID_EffectAdd = 300;
        private const Int32 ID_EffectRemove = 400;
        private const Int32 ID_LastPage = 500;
        private const Int32 ID_NextPage = 501;

        private Mobile m_From;
        private FoodEffectsCore m_Core;
        private Boolean m_CoreEnabled;
        private Int32 m_EffectIndex;

        private List<Type> m_FoodTypes = new List<Type>();
        private List<FoodEffect> m_FoodEffects = new List<FoodEffect>();

        public FoodEffectsSetupGump() : this(null)
        {
        }

        public FoodEffectsSetupGump(Mobile from) : base(150, 100)
        {
            FoodEffectsCore core = World.GetCore(typeof(FoodEffectsCore)) as FoodEffectsCore;

            if (core == null)
                return;

            m_From = from;
            m_Core = core;
            m_CoreEnabled = core.Enabled;
            m_EffectIndex = 0;

            foreach (KeyValuePair<Type, FoodEffect> pair in m_Core.FoodEffects)
                m_FoodTypes.Add(pair.Key);

            m_FoodTypes.Sort(new TypeSorter());

            for (Int32 i = 0; i < m_FoodTypes.Count; i++)
                m_FoodEffects.Add(m_Core.FoodEffects[m_FoodTypes[i]]);

            Setup();
        }

        public FoodEffectsSetupGump(Mobile from, FoodEffectsCore core, Boolean coreEnabled, List<Type> foodTypes, List<FoodEffect> foodEffects, Int32 effectIndex) : base(150, 100)
        {
            m_From = from;
            m_Core = core;
            m_CoreEnabled = coreEnabled;
            m_EffectIndex = effectIndex;
            m_FoodTypes = foodTypes;
            m_FoodEffects = foodEffects;

            Setup();
        }

        private void Setup()
        {
            if (m_From != null)
            {
                m_From.CloseGump(typeof(FoodEffectsSetupGump));
                m_From.CloseGump(typeof(FoodEffectGump));
            }

            this.AddPage(0);

            this.AddBackground(0, 0, 260, 336, 5100);

            this.AddLabel(66, 4, 2062, "Food Effect System");

            this.AddLabel(10, 30, 1359, "Enabled");
            this.AddCheck(63, 25, 9720, 9724, m_CoreEnabled, ID_Core_Enabled);

            this.AddImageTiled(2, 55, 254, 4, 5101);

            this.AddLabel(64, 60, 62, "Food Buffs");

            this.AddLabel(10, 310, 75, String.Format("v{0}", m_Core.Version));

            this.AddButton(90, 305, 5202, 5203, ID_Save_Button, GumpButtonType.Reply, 0);
            this.AddButton(175, 305, 5200, 5201, ID_Cancel_Button, GumpButtonType.Reply, 0);

            List<Type> foodTypes = new List<Type>();

            Boolean lastEntryBlank = false;

            for (Int32 i = m_EffectIndex; i < m_FoodTypes.Count && i < m_EffectIndex + 10; i++)
            {
                foodTypes.Add(m_FoodTypes[i]);

                if (m_FoodTypes[i] == null)
                    lastEntryBlank = true;
            }

            Boolean anotherPage = false;

            if (foodTypes.Count == 10)
                anotherPage = true;
            else if (!lastEntryBlank)
                foodTypes.Add(null);

            if (m_EffectIndex > 0)
                this.AddButton(10, 63, 9706, 9707, ID_LastPage, GumpButtonType.Reply, 0);

            if (anotherPage)
                this.AddButton(172, 63, 9702, 9703, ID_NextPage, GumpButtonType.Reply, 0);

            Int32 counter = m_EffectIndex;

            for (Int32 i = 0; i < foodTypes.Count; i++)
            {
                this.AddImageTiled(10, 80 + (i * 22), 180, 23, 2624);
                this.AddImageTiled(11, 81 + (i * 22), 178, 21, 3004);

                this.AddLabelCropped(15, 81 + (i * 22), 175, 16, 0, (foodTypes[i] != null ? foodTypes[i].Name : ""));

                this.AddButton(195, 80 + (i * 22), 4029, 4031, ID_EffectAdd + counter, GumpButtonType.Reply, 0);
                this.AddButton(225, 80 + (i * 22), 4020, 4022, ID_EffectRemove + counter++, GumpButtonType.Reply, 0);
            }
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            m_CoreEnabled = info.IsSwitched(ID_Core_Enabled);


            if (info.ButtonID >= ID_EffectAdd && info.ButtonID < ID_EffectRemove)
            {
                sender.Mobile.SendGump(new FoodEffectGump(sender.Mobile, m_Core, m_CoreEnabled, m_FoodTypes, m_FoodEffects, info.ButtonID - ID_EffectAdd, null, false));
            }
            else if (info.ButtonID >= ID_EffectRemove && info.ButtonID < ID_LastPage)
            {
                Int32 idx = info.ButtonID - ID_EffectRemove;

                if (idx < m_FoodTypes.Count)
                {
                    m_FoodTypes.RemoveAt(idx);
                    m_FoodEffects.RemoveAt(idx);
                }

                sender.Mobile.SendGump(new FoodEffectsSetupGump(sender.Mobile, m_Core, m_CoreEnabled, m_FoodTypes, m_FoodEffects, m_EffectIndex));
            }
            else
            {
                switch (info.ButtonID)
                {
                    case ID_Save_Button:
                        m_Core.Enabled = m_CoreEnabled;

                        m_Core.FoodEffects.Clear();

                        for (Int32 i = 0; i < m_FoodTypes.Count; i++)
                            if (m_FoodTypes[i] != null)
                                m_Core.FoodEffects[m_FoodTypes[i]] = m_FoodEffects[i];

                        sender.Mobile.SendMessage("Food effect System is {0}!  System contains {1} foods defined.", (m_Core.Enabled ? "enabled" : "disabled"), m_Core.FoodEffects.Keys.Count);

                        FoodEffectsCore.InvokeOnFoodEffectSystemUpdate(m_Core);

                        break;
                    case ID_LastPage:
                        sender.Mobile.SendGump(new FoodEffectsSetupGump(sender.Mobile, m_Core, m_CoreEnabled, m_FoodTypes, m_FoodEffects, m_EffectIndex - 10));
                        break;
                    case ID_NextPage:
                        sender.Mobile.SendGump(new FoodEffectsSetupGump(sender.Mobile, m_Core, m_CoreEnabled, m_FoodTypes, m_FoodEffects, m_EffectIndex + 10));
                        break;
                }
            }
        }

        private class TypeSorter : IComparer<Type>
        {
            public Int32 Compare(Type x, Type y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }
    }
}
