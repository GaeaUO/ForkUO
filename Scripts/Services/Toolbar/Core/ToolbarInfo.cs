using System;
using System.Collections.Generic;
using Server;
using CustomsFramework;

namespace Services.Toolbar.Core
{
    public partial class ToolbarInfo
    {
        private int _Font, _Skin;
        private bool _Phantom, _Stealth, _Reverse, _Lock;
        private List<int> _Dimensions = new List<int>();
        private List<string> _Entries = new List<string>();
        private List<Point3D> _Points = new List<Point3D>();

        public ToolbarInfo(List<int> dimensions, List<string> entries, int skin, List<Point3D> points,
            int font, bool[] switches)
        {
            _Dimensions = dimensions;
            _Entries = entries;
            _Skin = skin;
            _Points = points;
            _Font = font;
            _Phantom = switches[0];
            _Stealth = switches[1];
            _Reverse = switches[2];
            _Lock = switches[3];
        }

        public int Font
        {
            get
            {
                return _Font;
            }
            set
            {
                _Font = value;
            }
        }

        public int Skin
        {
            get
            {
                return _Skin;
            }
            set
            {
                _Skin = value;
            }
        }

        public bool Phantom
        {
            get
            {
                return _Phantom;
            }
            set
            {
                _Phantom = value;
            }
        }

        public bool Stealth
        {
            get
            {
                return _Stealth;
            }
            set
            {
                _Stealth = value;
            }
        }

        public bool Reverse
        {
            get
            {
                return _Reverse;
            }
            set
            {
                _Reverse = value;
            }
        }

        public bool Lock
        {
            get
            {
                return _Lock;
            }
            set
            {
                _Lock = value;
            }
        }

        public List<int> Dimensions
        {
            get
            {
                return _Dimensions;
            }
            set
            {
                _Dimensions = value;
            }
        }

        public List<string> Entries
        {
            get
            {
                return _Entries;
            }
            set
            {
                _Entries = value;
            }
        }

        public List<Point3D> Points
        {
            get
            {
                return _Points;
            }
            set
            {
                _Points = value;
            }
        }

        public static ToolbarInfo CreateNew(Mobile from)
        {
            List<int> dimensions = DefaultDimensions(from.AccessLevel);
            List<string> entries = DefaultEntries(from.AccessLevel);
            List<Point3D> points = new List<Point3D>();

            for (int i = entries.Count; i <= 135; i++)
                entries.Add("-*UNUSED*-");

            return new ToolbarInfo(dimensions, entries, 0, points, 0, new bool[] { true, false, false, true });
        }

        public static List<string> DefaultEntries(AccessLevel level)
        {
            List<string> entries = new List<string>();

            switch (level)
            {
                case AccessLevel.Player:
                    {
                        break;
                    }
                case AccessLevel.VIP:
                    {
                        break;
                    }
                case AccessLevel.Counselor:
                    {
                        entries.Add("[GMBody"); entries.Add("[StaffRunebook");
                        entries.Add("[SpeedBoost"); entries.Add("[M Tele");
                        entries.Add("[Where"); entries.Add("[Who");

                        break;
                    }
                case AccessLevel.Decorator:
                    {
                        entries.Add("[GMBody"); entries.Add("[StaffRunebook");
                        entries.Add("[SpeedBoost"); entries.Add("[M Tele");
                        entries.Add("[Where"); entries.Add("[Who");

                        for (int j = 0; j < 3; j++)
                            entries.Add("-*UNUSED*-");

                        entries.Add("[Add"); entries.Add("[Remove");
                        entries.Add("[Move"); entries.Add("[ShowArt");
                        entries.Add("[Get ItemID"); entries.Add("[Get Hue");

                        break;
                    }
                case AccessLevel.Spawner:
                    {
                        entries.Add("[GMBody"); entries.Add("[StaffRunebook");
                        entries.Add("[SpeedBoost"); entries.Add("[M Tele");
                        entries.Add("[Where"); entries.Add("[Who");

                        for (int j = 0; j < 3; j++)
                            entries.Add("-*UNUSED*-");

                        entries.Add("[Add"); entries.Add("[Remove");
                        entries.Add("[XmlAdd"); entries.Add("[XmlFind");
                        entries.Add("[XmlShow"); entries.Add("[XmlHide");

                        break;
                    }
                case AccessLevel.GameMaster:
                    {
                        entries.Add("[GMBody"); entries.Add("[StaffRunebook");
                        entries.Add("[SpeedBoost"); entries.Add("[M Tele");
                        entries.Add("[Where"); entries.Add("[Who");

                        for (int j = 0; j < 3; j++)
                            entries.Add("-*UNUSED*-");

                        entries.Add("[Add"); entries.Add("[Remove");
                        entries.Add("[Props"); entries.Add("[Move");
                        entries.Add("[Kill"); entries.Add("[Follow");

                        break;
                    }
                case AccessLevel.Seer:
                    {
                        goto case AccessLevel.GameMaster;
                    }
                case AccessLevel.Administrator:
                    {
                        entries.Add("[Admin"); entries.Add("[StaffRunebook");
                        entries.Add("[SpeedBoost"); entries.Add("[M Tele");
                        entries.Add("[Where"); entries.Add("[Who");

                        for (int j = 0; j < 3; j++)
                            entries.Add("-*UNUSED*-");

                        entries.Add("[Props"); entries.Add("[Move");
                        entries.Add("[Add"); entries.Add("[Remove");
                        entries.Add("[ViewEquip"); entries.Add("[Kill");

                        break;
                    }
                case AccessLevel.Developer:
                    {
                        goto case AccessLevel.Administrator;
                    }
                case AccessLevel.CoOwner:
                    {
                        goto case AccessLevel.Administrator;
                    }
                case AccessLevel.Owner:
                    {
                        goto case AccessLevel.Administrator;
                    }
            }
            return entries;
        }

        public static List<int> DefaultDimensions(AccessLevel level)
        {
            List<int> dimensions = new List<int>();

            switch (level)
            {
                case AccessLevel.Player:
                    {
                        dimensions.Add(0); dimensions.Add(0);
                        break;
                    }
                case AccessLevel.VIP:
                    {
                        goto case AccessLevel.Player;
                    }
                case AccessLevel.Counselor:
                    {
                        dimensions.Add(6); dimensions.Add(1);
                        break;
                    }
                case AccessLevel.Decorator:
                    {
                        dimensions.Add(6); dimensions.Add(2);
                        break;
                    }
                case AccessLevel.Spawner:
                    {
                        goto case AccessLevel.Decorator;
                    }
                case AccessLevel.GameMaster:
                    {
                        goto case AccessLevel.Decorator;
                    }
                case AccessLevel.Seer:
                    {
                        goto case AccessLevel.Decorator;
                    }
                case AccessLevel.Administrator:
                    {
                        goto case AccessLevel.Decorator;
                    }
                case AccessLevel.Developer:
                    {
                        goto case AccessLevel.Decorator;
                    }
                case AccessLevel.CoOwner:
                    {
                        goto case AccessLevel.Decorator;
                    }
                case AccessLevel.Owner:
                    {
                        goto case AccessLevel.Decorator;
                    }
            }
            return dimensions;
        }

        public ToolbarInfo(GenericReader reader)
        {
            Deserialize(reader);
        }

        public void Serialize(GenericWriter writer)
        {
            Utilities.WriteVersion(writer, 0);

            writer.Write(this.Font);
            writer.Write(this.Phantom);
            writer.Write(this.Stealth);
            writer.Write(this.Reverse);
            writer.Write(this.Lock);

            writer.Write(this.Dimensions.Count);

            for (int i = 0; i < this.Dimensions.Count; i++)
                writer.Write(this.Dimensions[i]);

            writer.Write(this.Entries.Count);

            for (int i = 0; i < this.Entries.Count; i++)
                writer.Write(this.Entries[i]);

            writer.Write(this.Skin);

            writer.Write(this.Points.Count);

            for (int i = 0; i < this.Points.Count; i++)
                writer.Write(this.Points[i]);
        }

        private void Deserialize(GenericReader reader)
        {
            int version = reader.ReadInt();

            List<int> _Dimensions = new List<int>();
            List<string> _Entries = new List<string>();
            List<Point3D> _Points = new List<Point3D>();

            switch (version)
            {
                case 0:
                    {
                        _Font = reader.ReadInt();
                        _Phantom = reader.ReadBool();
                        _Stealth = reader.ReadBool();
                        _Reverse = reader.ReadBool();
                        _Lock = reader.ReadBool();

                        int count = reader.ReadInt();

                        for (int i = 0; i < count; i++)
                            _Dimensions.Add(reader.ReadInt());
                        
                        count = reader.ReadInt();

                        for (int i = 0; i < count; i++)
                            _Entries.Add(reader.ReadString());

                        _Skin = reader.ReadInt();

                        count = reader.ReadInt();

                        for (int i = 0; i < count; i++)
                            _Points.Add(reader.ReadPoint3D());

                        break;
                    }
            }
        }
    }
}
