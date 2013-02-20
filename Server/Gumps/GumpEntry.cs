using Server.Network;

namespace Server.Gumps
{
    public abstract class GumpEntry
    {
        private Gump m_Parent;

        protected GumpEntry()
        {
        }

        public Gump Parent
        {
            get { return this.m_Parent; }
            set
            {
                if (this.m_Parent == value) return;

                if (this.m_Parent != null)
                {
                    this.m_Parent.Remove(this);
                }

                this.m_Parent = value;

                this.m_Parent.Add(this);
            }
        }

        public abstract string Compile();

        public abstract void AppendTo(IGumpWriter disp);

        protected void Delta(ref int var, int val)
        {
            if (var == val) return;

            var = val;

            if (this.m_Parent != null)
            {
                this.m_Parent.Invalidate();
            }
        }

        protected void Delta(ref bool var, bool val)
        {
            if (var == val) return;

            var = val;

            if (this.m_Parent != null)
            {
                this.m_Parent.Invalidate();
            }
        }

        protected void Delta(ref string var, string val)
        {
            if (var == val) return;

            var = val;

            if (this.m_Parent != null)
            {
                this.m_Parent.Invalidate();
            }
        }

        protected void Delta(ref object var, object val)
        {
            if (var == val) return;

            var = val;

            if (this.m_Parent != null)
            {
                this.m_Parent.Invalidate();
            }
        }
    }
}