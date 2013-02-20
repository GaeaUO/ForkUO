/*****************************************************************************************************************************
 * 
 * Slayer Title Definition Gump
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
using Server.Mobiles;

namespace CustomsFramework.Systems.SlayerTitleSystem
{
    public class TitleDefinitionGump : Gump
    {
        private const Int32 ID_Cancel_Button = 0;
        private const Int32 ID_DefinitionName = 50;
        private const Int32 ID_DefinitionEnabled = 100;
        private const Int32 ID_Apply_Button = 200;
        private const Int32 ID_CreatureEntry = 300;
        private const Int32 ID_TitleEntry = 400;
        private const Int32 ID_CountNeededEntry = 500;

        private const Int32 HUE_ValidEntry = 0;
        private const Int32 HUE_InvalidEntry = 34;

        private Mobile m_From;
        private Boolean m_CoreEnabled;
        private Int32 m_TitleIndex;

        private List<TitleDefinition> m_TitleDefinitions = new List<TitleDefinition>();

        private List<String> m_CreatureNames = new List<string>();
        private List<String> m_Titles = new List<string>();
        private List<String> m_Counts = new List<string>();

        public TitleDefinitionGump(Mobile from, Boolean coreEnabled, List<TitleDefinition> titleDefinitions, Int32 titleIndex, List<String> creatureNames, List<String> titles, List<String> counts, Boolean validate) : base(50, 100)
        {
            m_From = from;
            m_CoreEnabled = coreEnabled;
            m_TitleDefinitions = titleDefinitions;
            m_TitleIndex = titleIndex;
            m_CreatureNames = creatureNames;
            m_Titles = titles;
            m_Counts = counts;

            TitleDefinition definition;

            if (m_TitleIndex < m_TitleDefinitions.Count)
                definition = m_TitleDefinitions[m_TitleIndex];
            else
                definition = new TitleDefinition("", false, new List<Type>(), new List<TitleEntry>());

            if (m_CreatureNames == null)
            {
                m_CreatureNames = new List<string>();
                m_Titles = new List<string>();
                m_Counts = new List<string>();

                foreach (Type t in definition.CreatureRegistry)
                    m_CreatureNames.Add(t.Name);

                foreach (TitleEntry entry in definition.TitleRegistry)
                {
                    m_Titles.Add(entry.Title);
                    m_Counts.Add(entry.CountNeeded.ToString());
                }
            }

            m_From.CloseGump(typeof(SlayerTitleSetupGump));
            m_From.CloseGump(typeof(TitleDefinitionGump));

            this.AddPage(0);

            this.AddBackground(0, 0, 730, 470, 5100);

            this.AddLabel(10, 5, 1265, "Title Definition Name");

            this.AddImageTiled(150, 4, 570, 23, 2624);
            this.AddImageTiled(151, 5, 568, 21, 3004);
            this.AddTextEntry(154, 5, 565, 21, 0, ID_DefinitionName, definition.DefinitionName);

            if (validate && definition.DefinitionName == "")
                this.AddLabel(100, 30, HUE_InvalidEntry, "Title Definition Name must be entered");

            this.AddLabel(10, 30, 1359, "Enabled");
            this.AddCheck(63, 25, 9720, 9724, definition.Enabled, ID_DefinitionEnabled);

            this.AddImageTiled(2, 55, 722, 4, 5101);

            this.AddLabel(300, 60, 62, "Creature Registry");

            Type typeCheck = typeof(BaseCreature);
            Boolean creaturesValid = true;

            for (int col = 0; col < 2; col++)
            {
                for (int row = 0; row < 10; row++)
                {
                    Int32 idx = row + (col * 10);
                    String name = "";

                    if (idx < m_CreatureNames.Count)
                        name = m_CreatureNames[idx];

                    Type type = ScriptCompiler.FindTypeByName(name);

                    Boolean valid = type != null || name == "";

                    if (typeCheck != null && type != null)
                        valid &= typeCheck.IsAssignableFrom(type);

                    creaturesValid &= valid;

                    this.AddImageTiled(10 + (col * 360), 80 + (row * 22), 350, 23, 2624);
                    this.AddImageTiled(11 + (col * 360), 81 + (row * 22), 348, 21, 3004);
                    this.AddTextEntry(14 + (col * 360), 81 + (row * 22), 345, 21, (valid ? HUE_ValidEntry : HUE_InvalidEntry), ID_CreatureEntry + idx, name);
                }
            }

            if (!creaturesValid)
                this.AddLabel(10, 60, HUE_InvalidEntry, "Invalid BaseCreature Type Specified");

            Boolean titlesValid = true;

            this.AddLabel(314, 305, 62, "Title Registry");

            for (int col = 0; col < 2; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    Int32 idx = row + (col * 5);
                    String title = "";
                    String count = "";

                    if (idx < m_Titles.Count)
                        title = m_Titles[idx];

                    if (idx < m_Counts.Count)
                        count = m_Counts[idx];

                    if (title != "" && count == "")
                        count = "0";

                    Int32 test;
                    Boolean countValid = Int32.TryParse(count, out test);

                    if (title != "" && count == "0")
                        countValid = false;

                    if (test < 0)
                        countValid = false;

                    if (title == "" && count == "")
                        countValid = true;

                    titlesValid &= countValid;

                    this.AddImageTiled(10 + (col * 360), 325 + (row * 22), 295, 23, 2624);
                    this.AddImageTiled(11 + (col * 360), 326 + (row * 22), 293, 21, 3004);
                    this.AddTextEntry(14 + (col * 360), 326 + (row * 22), 290, 21, HUE_ValidEntry, ID_TitleEntry + idx, title);

                    this.AddImageTiled(310 + (col * 360), 325 + (row * 22), 50, 23, 2624);
                    this.AddImageTiled(311 + (col * 360), 326 + (row * 22), 48, 21, 3004);
                    this.AddTextEntry(314 + (col * 360), 326 + (row * 22), 45, 21, (countValid ? HUE_ValidEntry : HUE_InvalidEntry), ID_CountNeededEntry + idx, count);
                }
            }

            if (!titlesValid)
                this.AddLabel(10, 305, HUE_InvalidEntry, "Invalid Count Value Specified");

            this.AddButton(560, 440, 5204, 5205, ID_Apply_Button, GumpButtonType.Reply, 0);
            this.AddButton(645, 440, 5200, 5201, ID_Cancel_Button, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            String titleName = info.GetTextEntry(ID_DefinitionName).Text.Trim();
            Boolean enabled = info.IsSwitched(ID_DefinitionEnabled);

            switch (info.ButtonID)
            {
                case ID_Apply_Button:
                    if (IsDataValid(sender, info))
                    {
                        List<Type> creatureRegistry = new List<Type>();
                        List<TitleEntry> titleRegistry = new List<TitleEntry>();
                        
                        for (int i = 0; i < 20; i++)
                        {
                            string name = info.GetTextEntry(ID_CreatureEntry + i).Text;

                            Type type = ScriptCompiler.FindTypeByName(name);

                            if (type != null)
                                creatureRegistry.Add(type);
                        }

                        for (int i = 0; i < 10; i++)
                        {
                            string title = info.GetTextEntry(ID_TitleEntry + i).Text;
                            string count = info.GetTextEntry(ID_CountNeededEntry + i).Text;

                            Int32 cnt = 0;
                            Int32.TryParse(count, out cnt);

                            if (title != "" && cnt > 0)
                                titleRegistry.Add(new TitleEntry(title, cnt));
                        }

                        if (m_TitleIndex < m_TitleDefinitions.Count)
                            m_TitleDefinitions[m_TitleIndex] = new TitleDefinition(titleName, enabled, creatureRegistry, titleRegistry);
                        else
                            m_TitleDefinitions.Add(new TitleDefinition(titleName, enabled, creatureRegistry, titleRegistry));

                        sender.Mobile.SendGump(new SlayerTitleSetupGump(sender.Mobile, m_CoreEnabled, m_TitleDefinitions, (m_TitleIndex / 10) * 10));
                    }
                    else
                    {
                        if (m_TitleIndex < m_TitleDefinitions.Count)
                            m_TitleDefinitions[m_TitleIndex] = new TitleDefinition(titleName, enabled, m_TitleDefinitions[m_TitleIndex].CreatureRegistry, m_TitleDefinitions[m_TitleIndex].TitleRegistry);
                        else
                            m_TitleDefinitions.Add(new TitleDefinition(titleName, enabled, new List<Type>(), new List<TitleEntry>()));

                        sender.Mobile.SendGump(new TitleDefinitionGump(sender.Mobile, m_CoreEnabled, m_TitleDefinitions, m_TitleIndex, m_CreatureNames, m_Titles, m_Counts, true));
                    }

                    break;
                default:
                    sender.Mobile.SendGump(new SlayerTitleSetupGump(sender.Mobile, m_CoreEnabled, m_TitleDefinitions, (m_TitleIndex / 10) * 10));
                    break;
            }
        }

        private Boolean IsDataValid(Server.Network.NetState sender, RelayInfo info)
        {
            bool valid = true;
            Type typeCheck = typeof(BaseCreature);

            m_CreatureNames.Clear();
            m_Titles.Clear();
            m_Counts.Clear();

            if (info.GetTextEntry(ID_DefinitionName).Text == "")
                valid = false;

            for (int i = 0; i < 20; i++)
            {
                string name = info.GetTextEntry(ID_CreatureEntry + i).Text;

                Type type = ScriptCompiler.FindTypeByName(name);

                if (name != "" && type == null)
                    valid = false;

                if (typeCheck != null && type != null)
                    valid &= typeCheck.IsAssignableFrom(type);

                if (name != "")
                    m_CreatureNames.Add(name);
            }

            for (int i = 0; i < 10; i++)
            {
                string title = info.GetTextEntry(ID_TitleEntry + i).Text.Trim();
                string count = info.GetTextEntry(ID_CountNeededEntry + i).Text.Trim();

                if (title == "")
                    count = "";
                else if (count == "")
                    count = "0";

                if (title != "" && count != "")
                {
                    Int32 test;

                    if (!Int32.TryParse(count, out test))
                        valid = false;

                    if (title != "" && (count == "0" || test < 0))
                        valid = false;

                    m_Titles.Add(title);
                    m_Counts.Add(count);
                }
            }

            return valid;
        }
    }
}