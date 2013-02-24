namespace Server.Gumps
{
    public partial class Gump
    {
        public void AddButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type, int param,
                              string name = "")
        {
            this.Add(new GumpButton(x, y, normalID, pressedID, buttonID, type, param, null, null, name));
        }

        public void AddButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type, int param, ButtonResponse callback, string name = "")
        {
            this.Add(new GumpButton(x, y, normalID, pressedID, buttonID, type, param, callback, null));
        }

        public void AddButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type, int param, ButtonResponse callback, object callbackParam, string name = "")
        {
            this.Add(new GumpButton(x, y, normalID, pressedID, buttonID, type, param, callback, callbackParam, name));
        }

        public void AddButton(int x, int y, int normalID, int pressedID, GumpButtonType type, ButtonResponse callback,
                              int param = 0, string name = "")
        {
            this.Add(new GumpButton(x, y, normalID, pressedID, this.NewID(), type, param, callback, null, name));
        }

        public void AddButton(int x, int y, int normalID, int pressedID, GumpButtonType type, ButtonResponse callback,
                              object callbackParam, int param = 0, string name = "")
        {
            this.Add(new GumpButton(x, y, normalID, pressedID, this.NewID(), type, param, callback, callbackParam, name));
        }

        public void AddImageTiledButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type,
                                           int param, int itemID, int hue, int width, int height,
                                           int localizedTooltip = -1, string name = "")
        {
            this.Add(new GumpImageTileButton(x, y, normalID, pressedID, buttonID, type, param, itemID, hue, width,
                                             height, null, null, localizedTooltip, name));
        }

        public void AddImageTiledButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type,
                                           int param, int itemID, int hue, int width, int height,
                                           ButtonResponse callback, int localizedTooltip = -1, string name = "")
        {
            this.Add(new GumpImageTileButton(x, y, normalID, pressedID, buttonID, type, param, itemID, hue, width,
                                             height, callback, null, localizedTooltip, name));
        }

        public void AddImageTiledButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type,
                                           int param, int itemID, int hue, int width, int height,
                                           ButtonResponse callback, object callbackParam, int localizedTooltip = -1,
                                           string name = "")
        {
            this.Add(new GumpImageTileButton(x, y, normalID, pressedID, buttonID, type, param, itemID, hue, width,
                                             height, callback, callbackParam, localizedTooltip, name));
        }

        public void AddImageTiledButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type,
                                           int itemID, int hue, int width, int height, ButtonResponse callback,
                                           int param = 0, int localizedTooltip = -1, string name = "")
        {
            this.Add(new GumpImageTileButton(x, y, normalID, pressedID, this.NewID(), type, param, itemID, hue, width,
                                             height, callback, null, localizedTooltip, name));
        }

        public void AddImageTiledButton(int x, int y, int normalID, int pressedID, int buttonID, GumpButtonType type,
                                           int itemID, int hue, int width, int height, ButtonResponse callback,
                                           object callbackParam, int param = 0, int localizedTooltip = -1,
                                           string name = "")
        {
            this.Add(new GumpImageTileButton(x, y, normalID, pressedID, this.NewID(), type, param, itemID, hue, width,
                                             height, callback, callbackParam, localizedTooltip, name));
        }
    }
}
