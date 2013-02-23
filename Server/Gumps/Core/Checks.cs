namespace Server.Gumps
{
    public partial class Gump
    {
        public void AddCheck(int x, int y, int inactiveID, int activeID, bool initialState, int switchID,
                             string name = "")
        {
            this.Add(new GumpCheck(x, y, inactiveID, activeID, initialState, switchID, null, null, name));
        }

        public void AddCheck(int x, int y, int inactiveID, int activeID, bool initialState, int switchID,
                             CheckResponse callback, string name = "")
        {
            this.Add(new GumpCheck(x, y, inactiveID, activeID, initialState, switchID, callback, null, name));
        }

        public void AddCheck(int x, int y, int inactiveID, int activeID, bool initialState, int switchID,
                             CheckResponse callback, object callbackParam, string name = "")
        {
            this.Add(new GumpCheck(x, y, inactiveID, activeID, initialState, switchID, callback, callbackParam, name));
        }

        public void AddCheck(int x, int y, int inactiveID, int activeID, bool initialState, CheckResponse callback,
                             string name = "")
        {
            this.Add(new GumpCheck(x, y, inactiveID, activeID, initialState, this.NewID(), callback, null, name));
        }

        public void AddCheck(int x, int y, int inactiveID, int activeID, bool initialState, CheckResponse callback,
                             object callbackParam, string name = "")
        {
            this.Add(new GumpCheck(x, y, inactiveID, activeID, initialState, this.NewID(), callback, callbackParam, name));
        }
    }
}
