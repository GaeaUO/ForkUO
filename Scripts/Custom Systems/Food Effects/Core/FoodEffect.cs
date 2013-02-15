using System;

namespace CustomsFramework.Systems.FoodEffects
{
    public class FoodEffect
    {
        private Int32 m_RegenHits = 0;
        public Int32 RegenHits { get { return m_RegenHits; } }

        private Int32 m_RegenStam = 0;
        public Int32 RegenStam { get { return m_RegenStam; } }

        private Int32 m_RegenMana = 0;
        public Int32 RegenMana { get { return m_RegenMana; } }

        private Int32 m_StrBonus = 0;
        public Int32 StrBonus { get { return m_StrBonus; } }

        private Int32 m_DexBonus = 0;
        public Int32 DexBonus { get { return m_DexBonus; } }

        private Int32 m_IntBonus = 0;
        public Int32 IntBonus { get { return m_IntBonus; } }

        private Int32 m_Duration = 0;
        public Int32 Duration { get { return m_Duration; } }

        private DateTime m_Added;
        public DateTime Added { get { return m_Added; } }

        public TimeSpan EffectTimeSpan { get { return TimeSpan.FromMinutes(m_Duration); } }

        public FoodEffect(Int32 regenHits, Int32 regenStam, Int32 regenMana, Int32 strBonus, Int32 dexBonus, Int32 intBonus, Int32 duration) :
            this(regenHits, regenStam, regenMana, strBonus, dexBonus, intBonus, duration, DateTime.Now)
        {
        }

        public FoodEffect(Int32 regenHits, Int32 regenStam, Int32 regenMana, Int32 strBonus, Int32 dexBonus, Int32 intBonus, Int32 duration, DateTime added)
        {
            m_RegenHits = regenHits;
            m_RegenStam = regenStam;
            m_RegenMana = regenMana;
            m_StrBonus = strBonus;
            m_DexBonus = dexBonus;
            m_IntBonus = intBonus;
            m_Duration = duration;
            m_Added = added;
        }

        public Boolean IsExpired
        {
            get
            {
                if (m_Added.AddMinutes((double)m_Duration) < DateTime.Now)
                    return false;
                else
                    return true;
            }
        }
    }
}
