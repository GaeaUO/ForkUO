using System;
using System.Collections.Generic;

using Server;
using Server.Gumps;
using Server.Items;

namespace CustomsFramework.Systems.FoodEffects
{
    public class FoodEffectGump : Gump
    {
        private const Int32 ID_Cancel_Button = 0;
        private const Int32 ID_FoodType = 10;
        private const Int32 ID_HitsRegen = 20;
        private const Int32 ID_StamRegen = 21;
        private const Int32 ID_ManaRegen = 22;
        private const Int32 ID_StrBonus = 30;
        private const Int32 ID_DexBonus = 31;
        private const Int32 ID_IntBonus = 32;
        private const Int32 ID_Duration = 40;
        private const Int32 ID_Apply_Button = 200;

        private const Int32 HUE_ValidEntry = 0;
        private const Int32 HUE_InvalidEntry = 34;

        private Mobile m_From;
        private Boolean m_CoreEnabled;
        private Int32 m_EffectIndex;

        private List<Type> m_FoodTypes = new List<Type>();
        private List<FoodEffect> m_FoodEffects = new List<FoodEffect>();

        private String[] m_Values;

        public FoodEffectGump(Mobile from, Boolean coreEnabled, List<Type> foodTypes, List<FoodEffect> foodEffects, Int32 effectIndex, String[] values, Boolean validate) : base(150, 100)
        {
            m_From = from;
            m_CoreEnabled = coreEnabled;
            m_FoodTypes = foodTypes;
            m_FoodEffects = foodEffects;
            m_EffectIndex = effectIndex;
            m_Values = values;

            if (m_Values == null)
            {
                m_Values = new String[] { "", "0", "0", "0", "0", "0", "0", "0" };

                if (m_EffectIndex < m_FoodTypes.Count)
                {
                    m_Values[0] = m_FoodTypes[m_EffectIndex].Name;
                    m_Values[1] = m_FoodEffects[m_EffectIndex].RegenHits.ToString();
                    m_Values[2] = m_FoodEffects[m_EffectIndex].RegenStam.ToString();
                    m_Values[3] = m_FoodEffects[m_EffectIndex].RegenMana.ToString();
                    m_Values[4] = m_FoodEffects[m_EffectIndex].StrBonus.ToString();
                    m_Values[5] = m_FoodEffects[m_EffectIndex].DexBonus.ToString();
                    m_Values[6] = m_FoodEffects[m_EffectIndex].IntBonus.ToString();
                    m_Values[7] = m_FoodEffects[m_EffectIndex].Duration.ToString();
                }
            }

            m_From.CloseGump(typeof(FoodEffectsSetupGump));
            m_From.CloseGump(typeof(FoodEffectGump));

            this.AddPage(0);

            this.AddBackground(0, 0, 360, 220, 5100);

            this.AddLabel(10, 9, 0, "Food Type");
            this.AddImageTiled(110, 6, 240, 23, 2624);
            this.AddImageTiled(111, 7, 238, 21, 3004);
            this.AddTextEntry(114, 7, 235, 21, (validate || IsFoodType(m_Values[0]) ? HUE_ValidEntry : HUE_InvalidEntry), ID_FoodType, m_Values[0]);

            this.AddLabel(10, 31, 0, "Hit Point Regen");
            this.AddImageTiled(110, 28, 50, 23, 2624);
            this.AddImageTiled(111, 29, 48, 21, 3004);
            this.AddTextEntry(114, 29, 45, 21, (validate || IsValidNumber(m_Values[1], true) ? HUE_ValidEntry : HUE_InvalidEntry), ID_HitsRegen, m_Values[1]);

            this.AddLabel(10, 53, 0, "Stamina Regen");
            this.AddImageTiled(110, 50, 50, 23, 2624);
            this.AddImageTiled(111, 51, 48, 21, 3004);
            this.AddTextEntry(114, 51, 45, 21, (validate || IsValidNumber(m_Values[2], true) ? HUE_ValidEntry : HUE_InvalidEntry), ID_StamRegen, m_Values[2]);

            this.AddLabel(10, 75, 0, "Mana Regen");
            this.AddImageTiled(110, 72, 50, 23, 2624);
            this.AddImageTiled(111, 73, 48, 21, 3004);
            this.AddTextEntry(114, 73, 45, 21, (validate || IsValidNumber(m_Values[3], true) ? HUE_ValidEntry : HUE_InvalidEntry), ID_ManaRegen, m_Values[3]);

            this.AddLabel(10, 97, 0, "STR Bonus");
            this.AddImageTiled(110, 94, 50, 23, 2624);
            this.AddImageTiled(111, 95, 48, 21, 3004);
            this.AddTextEntry(114, 95, 45, 21, (validate || IsValidNumber(m_Values[4], true) ? HUE_ValidEntry : HUE_InvalidEntry), ID_StrBonus, m_Values[4]);

            this.AddLabel(10, 119, 0, "DEX Bonus");
            this.AddImageTiled(110, 116, 50, 23, 2624);
            this.AddImageTiled(111, 117, 48, 21, 3004);
            this.AddTextEntry(114, 117, 45, 21, (validate || IsValidNumber(m_Values[5], true) ? HUE_ValidEntry : HUE_InvalidEntry), ID_DexBonus, m_Values[5]);

            this.AddLabel(10, 141, 0, "INT Bonus");
            this.AddImageTiled(110, 138, 50, 23, 2624);
            this.AddImageTiled(111, 139, 48, 21, 3004);
            this.AddTextEntry(114, 139, 45, 21, (validate || IsValidNumber(m_Values[6], true) ? HUE_ValidEntry : HUE_InvalidEntry), ID_IntBonus, m_Values[6]);

            this.AddLabel(10, 163, 0, "Duration");
            this.AddImageTiled(110, 160, 50, 23, 2624);
            this.AddImageTiled(111, 161, 48, 21, 3004);
            this.AddTextEntry(114, 161, 45, 21, (validate || IsValidNumber(m_Values[7], false) ? HUE_ValidEntry : HUE_InvalidEntry), ID_Duration, m_Values[7]);

            this.AddLabel(170, 53, 0, "1 Regen = 0.1 / Sec");
            this.AddLabel(165, 163, 0, "Minutes");

            if (validate)
            {
                if (!IsFoodType(m_Values[0]))
                    this.AddLabel(165, 31, HUE_InvalidEntry, "Food Type must be a valid Food");

                Boolean valid = true;

                for (Int32 i = 1; i < 8; i++)
                    valid &= IsValidNumber(m_Values[7], (i < 7));

                if (!valid)
                    this.AddLabel(165, 119, HUE_InvalidEntry, "Invalid value(s) entered");
            }

            this.AddButton(190, 190, 5204, 5205, ID_Apply_Button, GumpButtonType.Reply, 0);
            this.AddButton(275, 190, 5200, 5201, ID_Cancel_Button, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            String[] values = new String[] { "", "0", "0", "0", "0", "0", "0", "0" };

            values[0] = info.GetTextEntry(ID_FoodType).Text.Trim();
            values[1] = info.GetTextEntry(ID_HitsRegen).Text;
            values[2] = info.GetTextEntry(ID_StamRegen).Text;
            values[3] = info.GetTextEntry(ID_ManaRegen).Text;
            values[4] = info.GetTextEntry(ID_StrBonus).Text;
            values[5] = info.GetTextEntry(ID_DexBonus).Text;
            values[6] = info.GetTextEntry(ID_IntBonus).Text;
            values[7] = info.GetTextEntry(ID_Duration).Text;

            switch (info.ButtonID)
            {
                case ID_Apply_Button:
                    Boolean valid = true;

                    valid &= IsFoodType(values[0]);

                    for (Int32 i = 1; i < 8; i++)
                        valid &= IsValidNumber(values[i], (i < 7));

                    if (valid)
                    {
                        Int32[] parsedValues = new Int32[] { 0, 0, 0, 0, 0, 0, 0 };

                        for (Int32 i = 1; i < 8; i++)
                            Int32.TryParse(values[i], out parsedValues[i - 1]);

                        FoodEffect effect = new FoodEffect(parsedValues[0], parsedValues[1], parsedValues[2], parsedValues[3], parsedValues[4], parsedValues[5], parsedValues[6]);

                        Type type = ScriptCompiler.FindTypeByName(values[0]);

                        if (m_EffectIndex < m_FoodTypes.Count)
                        {
                            m_FoodTypes[m_EffectIndex] = type;
                            m_FoodEffects[m_EffectIndex] = effect;
                        }
                        else
                        {
                            m_FoodTypes.Add(type);
                            m_FoodEffects.Add(effect);
                        }

                        sender.Mobile.SendGump(new FoodEffectsSetupGump(sender.Mobile, m_CoreEnabled, m_FoodTypes, m_FoodEffects, (m_EffectIndex / 10) * 10));
                    }
                    else
                    {
                        sender.Mobile.SendGump(new FoodEffectGump(sender.Mobile, m_CoreEnabled, m_FoodTypes, m_FoodEffects, m_EffectIndex, values, true));
                    }

                    break;
                default:
                    sender.Mobile.SendGump(new FoodEffectsSetupGump(sender.Mobile, m_CoreEnabled, m_FoodTypes, m_FoodEffects, (m_EffectIndex / 10) * 10));
                    break;
            }
        }

        private Boolean IsFoodType(String value)
        {
            Type typeCheck = typeof(Food);
            Type type = ScriptCompiler.FindTypeByName(value);

            Boolean valid = type != null || value == "";

            if (typeCheck != null && type != null)
                valid &= typeCheck.IsAssignableFrom(type);

            return valid;
        }

        private Boolean IsValidNumber(String value, Boolean allowNegatives)
        {
            Boolean valid = true;
            Int32 test;

            valid = Int32.TryParse(value, out test);

            if (!allowNegatives && test < 0)
                valid = false;

            return valid;
        }
    }
}
