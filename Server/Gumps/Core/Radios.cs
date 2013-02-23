using System.Linq;

namespace Server.Gumps
{
    public partial class Gump
    {
        public void AddRadio(int x, int y, int inactiveID, int activeID, bool initialState, int switchID,
                             string name = "")
        {
            this.Add(new GumpRadio(x, y, inactiveID, activeID, initialState, switchID, null, null, name));
        }

        public void AddRadio(int x, int y, int inactiveID, int activeID, bool initialState, int switchID, RadioResponse callback, string name = "")
        {
            this.Add(new GumpRadio(x, y, inactiveID, activeID, initialState, switchID, callback, null, name));
        }

        public void AddRadio(int x, int y, int inactiveID, int activeID, bool initialState, int switchID, RadioResponse callback, object callbackParam, string name = "")
        {
            this.Add(new GumpRadio(x, y, inactiveID, activeID, initialState, switchID, callback, callbackParam, name));
        }

        public void AddRadio(int x, int y, int inactiveID, int activeID, bool initialState, RadioResponse callback, string name = "")
        {
            this.Add(new GumpRadio(x, y, inactiveID, activeID, initialState, this.NewID(), callback, null, name));
        }

        public void AddRadio(int x, int y, int inactiveID, int activeID, bool initialState, RadioResponse callback, object callbackParam, string name = "")
        {
            this.Add(new GumpRadio(x, y, inactiveID, activeID, initialState, this.NewID(), callback, callbackParam, name));
        }
    }
}
