/*****************************************************************************************************************************
 * 
 * Slayer Title Configuration Gump
 * Version 3.0
 * Designed for ForkUO 0.2
 * 
 * Authored by Dougan Ironfist
 * Last Updated on 2/11/2013
 * 
 *
 * The purpose of these scripts is to allow shard administrators to create fun kill-based titles that players can earn. 
 ****************************************************************************************************************************/

using System;
using System.Collections.Generic;

using Server;
using Server.Gumps;

namespace CustomsFramework.Systems.SlayerTitleSystem
{
    public class SlayerTitleSetupGump : Gump
    {
        private const Int32 ID_Cancel_Button = 0;
        private const Int32 ID_Core_Enabled = 100;
        private const Int32 ID_Save_Button = 200;
        private const Int32 ID_TitlesAdd = 300;
        private const Int32 ID_TitlesRemove = 400;
        private const Int32 ID_LastPage = 500;
        private const Int32 ID_NextPage = 501;

        private const Int32 HUE_Enabled = 0;
        private const Int32 HUE_Disabled = 34;

        private Mobile m_From;
        private SlayerTitleCore m_Core;
        private Boolean m_CoreEnabled;
        private Int32 m_TitleIndex;

        private List<TitleDefinition> m_TitleDefinitions = new List<TitleDefinition>();

        public SlayerTitleSetupGump()
            : this(null)
        {
        }

        public SlayerTitleSetupGump(Mobile from) : base(150, 100)
        {
            SlayerTitleCore core = World.GetCore(typeof(SlayerTitleCore)) as SlayerTitleCore;

            if (core == null)
                return;

            m_From = from;
            m_Core = core;
            m_CoreEnabled = core.Enabled;
            m_TitleIndex = 0;

            foreach (TitleDefinition def in m_Core.TitleDefinitions)
                m_TitleDefinitions.Add(new TitleDefinition(def.DefinitionName, def.Enabled, def.CreatureRegistry, def.TitleRegistry));

            Setup();
        }

        public SlayerTitleSetupGump(Mobile from, SlayerTitleCore core, Boolean coreEnabled, List<TitleDefinition> titleDefinitions, Int32 titleIndex)
            : base(150, 100)
        {
            m_From = from;
            m_Core = core;
            m_CoreEnabled = coreEnabled;
            m_TitleIndex = titleIndex;
            m_TitleDefinitions = titleDefinitions;

            Setup();
        }

        private void Setup()
        {
            if (m_From != null)
            {
                m_From.CloseGump(typeof(SlayerTitleSetupGump));
                m_From.CloseGump(typeof(TitleDefinitionGump));
            }

            this.AddPage(0);

            this.AddBackground(0, 0, 365, 335, 5100);

            this.AddLabel(115, 4, 2062, "Slayer Title System");

            this.AddLabel(10, 30, 1359, "Enabled");
            this.AddCheck(63, 25, 9720, 9724, m_CoreEnabled, ID_Core_Enabled);

            this.AddImageTiled(2, 55, 356, 4, 5101);

            this.AddLabel(94, 60, 62, "Registered Title Definitions");

            this.AddLabel(10, 310, 75, String.Format("v{0}", m_Core.Version));

            this.AddButton(195, 305, 5202, 5203, ID_Save_Button, GumpButtonType.Reply, 0);
            this.AddButton(280, 305, 5200, 5201, ID_Cancel_Button, GumpButtonType.Reply, 0);

            List<TitleDefinition> titles = new List<TitleDefinition>();

            Boolean lastEntryBlank = false;

            for (int i = m_TitleIndex; i < m_TitleDefinitions.Count && i < m_TitleIndex + 10; i++)
            {
                titles.Add(m_TitleDefinitions[i]);

                if (m_TitleDefinitions[i].DefinitionName == "")
                    lastEntryBlank = true;
            }

            bool anotherPage = false;

            if (titles.Count == 10)
                anotherPage = true;
            else if (!lastEntryBlank)
                titles.Add(new TitleDefinition("", false, new List<Type>(), new List<TitleEntry>()));

            if (m_TitleIndex > 0)
                this.AddButton(10, 63, 9706, 9707, ID_LastPage, GumpButtonType.Reply, 0);

            if (anotherPage)
                this.AddButton(277, 63, 9702, 9703, ID_NextPage, GumpButtonType.Reply, 0);

            int titleCounter = m_TitleIndex;

            for (int i = 0; i < titles.Count; i++)
            {
                TitleDefinition def = titles[i];

                this.AddImageTiled(10, 80 + (i * 22), 285, 23, 2624);
                this.AddImageTiled(11, 81 + (i * 22), 283, 21, 3004);

                this.AddLabelCropped(15, 81 + (i * 22), 275, 16, (def.Enabled ? HUE_Enabled : HUE_Disabled), def.DefinitionName);

                this.AddButton(296, 80 + (i * 22), 4029, 4031, ID_TitlesAdd + titleCounter, GumpButtonType.Reply, 0);
                this.AddButton(327, 80 + (i * 22), 4020, 4022, ID_TitlesRemove + titleCounter++, GumpButtonType.Reply, 0);
            }
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            m_CoreEnabled = info.IsSwitched(ID_Core_Enabled);

            if (info.ButtonID >= ID_TitlesAdd && info.ButtonID < ID_TitlesRemove)
            {
                sender.Mobile.SendGump(new TitleDefinitionGump(sender.Mobile, m_Core, m_CoreEnabled, m_TitleDefinitions, info.ButtonID - ID_TitlesAdd, null, null, null, false));
            }
            else if (info.ButtonID >= ID_TitlesRemove && info.ButtonID < ID_LastPage)
            {
                int idx = info.ButtonID - ID_TitlesRemove;

                if (idx < m_TitleDefinitions.Count)
                    m_TitleDefinitions.RemoveAt(idx);

                sender.Mobile.SendGump(new SlayerTitleSetupGump(sender.Mobile, m_Core, m_CoreEnabled, m_TitleDefinitions, m_TitleIndex));
            }
            else
            {
                switch (info.ButtonID)
                {
                    case ID_Save_Button:
                        m_Core.Enabled = m_CoreEnabled;

                        m_Core.TitleDefinitions.Clear();

                        foreach (TitleDefinition def in m_TitleDefinitions)
                            m_Core.TitleDefinitions.Add(def);

                        m_Core.CrossReferenceDefinitions();

                        sender.Mobile.SendMessage("Slayer Title System is {0}!  System contains {1} title definitions.", (m_Core.Enabled ? "enabled" : "disabled"), m_Core.TitleDefinitions.Count);

                        break;
                    case ID_LastPage:
                        sender.Mobile.SendGump(new SlayerTitleSetupGump(sender.Mobile, m_Core, m_CoreEnabled, m_TitleDefinitions, m_TitleIndex - 10));
                        break;
                    case ID_NextPage:
                        sender.Mobile.SendGump(new SlayerTitleSetupGump(sender.Mobile, m_Core, m_CoreEnabled, m_TitleDefinitions, m_TitleIndex + 10));
                        break;
                }
            }
        }
    }
}
