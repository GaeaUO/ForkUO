/*****************************************************************************************************************************
 * 
 * Slayer Title Core
 * Version 3.0
 * Designed for ForkUO 0.2
 * 
 * Authored by Dougan Ironfist
 * Last Updated on 2/11/2013
 * 
* The purpose of these scripts is to allow shard administrators to create fun kill-based titles that players can earn. 
  * 
 ****************************************************************************************************************************/

using System;

namespace CustomsFramework.Systems.SlayerTitleSystem
{
    public class TitleEntry
    {
        private String m_Title;
        public String Title { get { return m_Title; } }

        private Int32 m_CountNeeded;
        public Int32 CountNeeded { get { return m_CountNeeded; } }

        public TitleEntry(String title, Int32 countNeeded)
        {
            m_Title = title;
            m_CountNeeded = countNeeded;
        }
    }
}
