using System;

namespace Server.Crafting
{
    public class CraftAttributeInfo
    {
        private bool _RandomBonus = false;

        private Int32 _WeaponFireDamage;
        private Int32 _WeaponColdDamage;
        private Int32 _WeaponPoisonDamage;
        private Int32 _WeaponEnergyDamage;
        private Int32 _WeaponChaosDamage;
        private Int32 _WeaponDirectDamage;
        private Int32 _WeaponDurability;
        private Int32 _WeaponLuck;
        private Int32 _WeaponGoldIncrease;
        private Int32 _WeaponLowerRequirements;

        private Int32 _ArmorPhysicalResist;
        private Int32 _ArmorFireResist;
        private Int32 _ArmorColdResist;
        private Int32 _ArmorPoisonResist;
        private Int32 _ArmorEnergyResist;
        private Int32 _ArmorDurability;
        private Int32 _ArmorLuck;
        private Int32 _ArmorGoldIncrease;
        private Int32 _ArmorLowerRequirements;

        private Int32 _RunicMinAttributes;
        private Int32 _RunicMaxAttributes;
        private Int32 _RunicMinIntensity;
        private Int32 _RunicMaxIntensity;

        public bool RandomBonus { get { return _RandomBonus; } set { _RandomBonus = value; } }

        public Int32 WeaponFireDamage { get { return this._WeaponFireDamage; } set { this._WeaponFireDamage = value; } }
        public Int32 WeaponColdDamage { get { return this._WeaponColdDamage; } set { this._WeaponColdDamage = value; } }
        public Int32 WeaponPoisonDamage { get { return this._WeaponPoisonDamage; } set { this._WeaponPoisonDamage = value; } }
        public Int32 WeaponEnergyDamage { get { return this._WeaponEnergyDamage; } set { this._WeaponEnergyDamage = value; } }
        public Int32 WeaponChaosDamage { get { return this._WeaponChaosDamage; } set { this._WeaponChaosDamage = value; } }
        public Int32 WeaponDirectDamage { get { return this._WeaponDirectDamage; } set { this._WeaponDirectDamage = value; } }
        public Int32 WeaponDurability { get { return this._WeaponDurability; } set { this._WeaponDurability = value; } }
        public Int32 WeaponLuck { get { return this._WeaponLuck; } set { this._WeaponLuck = value; } }
        public Int32 WeaponGoldIncrease { get { return this._WeaponGoldIncrease; } set { this._WeaponGoldIncrease = value; } }
        public Int32 WeaponLowerRequirements { get { return this._WeaponLowerRequirements; } set { this._WeaponLowerRequirements = value; } }
        public Int32 ArmorPhysicalResist { get { return this._ArmorPhysicalResist; } set { this._ArmorPhysicalResist = value; } }
        public Int32 ArmorFireResist { get { return this._ArmorFireResist; } set { this._ArmorFireResist = value; } }
        public Int32 ArmorColdResist { get { return this._ArmorColdResist; } set { this._ArmorColdResist = value; } }
        public Int32 ArmorPoisonResist { get { return this._ArmorPoisonResist; } set { this._ArmorPoisonResist = value; } }
        public Int32 ArmorEnergyResist { get { return this._ArmorEnergyResist; } set { this._ArmorEnergyResist = value; } }
        public Int32 ArmorDurability { get { return this._ArmorDurability; } set { this._ArmorDurability = value; } }
        public Int32 ArmorLuck { get { return this._ArmorLuck; } set { this._ArmorLuck = value; } }
        public Int32 ArmorGoldIncrease { get { return this._ArmorGoldIncrease; } set { this._ArmorGoldIncrease = value; } }
        public Int32 ArmorLowerRequirements { get { return this._ArmorLowerRequirements; } set { this._ArmorLowerRequirements = value; } }
        public Int32 RunicMinAttributes { get { return this._RunicMinAttributes; } set { this._RunicMinAttributes = value; } }
        public Int32 RunicMaxAttributes { get { return this._RunicMaxAttributes; } set { this._RunicMaxAttributes = value; } }
        public Int32 RunicMinIntensity { get { return this._RunicMinIntensity; } set { this._RunicMinIntensity = value; } }
        public Int32 RunicMaxIntensity { get { return this._RunicMaxIntensity; } set { this._RunicMaxIntensity = value; } }

        #region Mondain's Legacy
        private Int32 _WeaponDamage;
        private Int32 _WeaponHitChance;
        private Int32 _WeaponHitLifeLeech;
        private Int32 _WeaponRegenHits;
        private Int32 _WeaponSwingSpeed;

        private Int32 _ArmorDamage;
        private Int32 _ArmorHitChance;
        private Int32 _ArmorRegenHits;
        private Int32 _ArmorMage;

        private Int32 _ShieldPhysicalResist;
        private Int32 _ShieldFireResist;
        private Int32 _ShieldColdResist;
        private Int32 _ShieldPoisonResist;
        private Int32 _ShieldEnergyResist;

        public Int32 WeaponDamage { get { return this._WeaponDamage; } set { this._WeaponDamage = value; } }
        public Int32 WeaponHitChance { get { return this._WeaponHitChance; } set { this._WeaponHitChance = value; } }
        public Int32 WeaponHitLifeLeech { get { return this._WeaponHitLifeLeech; } set { this._WeaponHitLifeLeech = value; } }
        public Int32 WeaponRegenHits { get { return this._WeaponRegenHits; } set { this._WeaponRegenHits = value; } }
        public Int32 WeaponSwingSpeed { get { return this._WeaponSwingSpeed; } set { this._WeaponSwingSpeed = value; } }

        public Int32 ArmorDamage { get { return this._ArmorDamage; } set { this._ArmorDamage = value; } }
        public Int32 ArmorHitChance { get { return this._ArmorHitChance; } set { this._ArmorHitChance = value; } }
        public Int32 ArmorRegenHits { get { return this._ArmorRegenHits; } set { this._ArmorRegenHits = value; } }
        public Int32 ArmorMage { get { return this._ArmorMage; } set { this._ArmorMage = value; } }

        public Int32 ShieldPhysicalResist { get { return this._ShieldPhysicalResist; } set { this._ShieldPhysicalResist = value; } }
        public Int32 ShieldFireResist { get { return this._ShieldFireResist; } set { this._ShieldFireResist = value; } }
        public Int32 ShieldColdResist { get { return this._ShieldColdResist; } set { this._ShieldColdResist = value; } }
        public Int32 ShieldPoisonResist { get { return this._ShieldPoisonResist; } set { this._ShieldPoisonResist = value; } }
        public Int32 ShieldEnergyResist { get { return this._ShieldEnergyResist; } set { this._ShieldEnergyResist = value; } }
        #endregion

        public CraftAttributeInfo()
        {
        }

        public static readonly CraftAttributeInfo Blank;
        public static readonly CraftAttributeInfo DullCopper, ShadowIron, Copper, Bronze, Golden, Agapite, Verite, Valorite;
        public static readonly CraftAttributeInfo Spined, Horned, Barbed;
        public static readonly CraftAttributeInfo RedScales, YellowScales, BlackScales, GreenScales, WhiteScales, BlueScales;
        public static readonly CraftAttributeInfo OakWood, AshWood, YewWood, Heartwood, Bloodwood, Frostwood;

        static CraftAttributeInfo()
        {
            Blank = new CraftAttributeInfo();

            CraftAttributeInfo dullCopper = DullCopper = new CraftAttributeInfo();

            dullCopper.ArmorPhysicalResist = 6;
            dullCopper.ArmorDurability = 50;
            dullCopper.ArmorLowerRequirements = 20;
            dullCopper.WeaponDurability = 100;
            dullCopper.WeaponLowerRequirements = 50;
            dullCopper.RunicMinAttributes = 1;
            dullCopper.RunicMaxAttributes = 2;
            if (Core.ML)
            {
                dullCopper.RunicMinIntensity = 40;
                dullCopper.RunicMaxIntensity = 100;
            }
            else
            {
                dullCopper.RunicMinIntensity = 10;
                dullCopper.RunicMaxIntensity = 35;
            }

            CraftAttributeInfo shadowIron = ShadowIron = new CraftAttributeInfo();

            shadowIron.ArmorPhysicalResist = 2;
            shadowIron.ArmorFireResist = 1;
            shadowIron.ArmorEnergyResist = 5;
            shadowIron.ArmorDurability = 100;
            shadowIron.WeaponColdDamage = 20;
            shadowIron.WeaponDurability = 50;
            shadowIron.RunicMinAttributes = 2;
            shadowIron.RunicMaxAttributes = 2;
            if (Core.ML)
            {
                shadowIron.RunicMinIntensity = 45;
                shadowIron.RunicMaxIntensity = 100;
            }
            else
            {
                shadowIron.RunicMinIntensity = 20;
                shadowIron.RunicMaxIntensity = 45;
            }

            CraftAttributeInfo copper = Copper = new CraftAttributeInfo();

            copper.ArmorPhysicalResist = 1;
            copper.ArmorFireResist = 1;
            copper.ArmorPoisonResist = 5;
            copper.ArmorEnergyResist = 2;
            copper.WeaponPoisonDamage = 10;
            copper.WeaponEnergyDamage = 20;
            copper.RunicMinAttributes = 2;
            copper.RunicMaxAttributes = 3;
            if (Core.ML)
            {
                copper.RunicMinIntensity = 50;
                copper.RunicMaxIntensity = 100;
            }
            else
            {
                copper.RunicMinIntensity = 25;
                copper.RunicMaxIntensity = 50;
            }

            CraftAttributeInfo bronze = Bronze = new CraftAttributeInfo();

            bronze.ArmorPhysicalResist = 3;
            bronze.ArmorColdResist = 5;
            bronze.ArmorPoisonResist = 1;
            bronze.ArmorEnergyResist = 1;
            bronze.WeaponFireDamage = 40;
            bronze.RunicMinAttributes = 3;
            bronze.RunicMaxAttributes = 3;
            if (Core.ML)
            {
                bronze.RunicMinIntensity = 55;
                bronze.RunicMaxIntensity = 100;
            }
            else
            {
                bronze.RunicMinIntensity = 30;
                bronze.RunicMaxIntensity = 65;
            }

            CraftAttributeInfo golden = Golden = new CraftAttributeInfo();

            golden.ArmorPhysicalResist = 1;
            golden.ArmorFireResist = 1;
            golden.ArmorColdResist = 2;
            golden.ArmorEnergyResist = 2;
            golden.ArmorLuck = 40;
            golden.ArmorLowerRequirements = 30;
            golden.WeaponLuck = 40;
            golden.WeaponLowerRequirements = 50;
            golden.RunicMinAttributes = 3;
            golden.RunicMaxAttributes = 4;
            if (Core.ML)
            {
                golden.RunicMinIntensity = 60;
                golden.RunicMaxIntensity = 100;
            }
            else
            {
                golden.RunicMinIntensity = 35;
                golden.RunicMaxIntensity = 75;
            }

            CraftAttributeInfo agapite = Agapite = new CraftAttributeInfo();

            agapite.ArmorPhysicalResist = 2;
            agapite.ArmorFireResist = 3;
            agapite.ArmorColdResist = 2;
            agapite.ArmorPoisonResist = 2;
            agapite.ArmorEnergyResist = 2;
            agapite.WeaponColdDamage = 30;
            agapite.WeaponEnergyDamage = 20;
            agapite.RunicMinAttributes = 4;
            agapite.RunicMaxAttributes = 4;
            if (Core.ML)
            {
                agapite.RunicMinIntensity = 65;
                agapite.RunicMaxIntensity = 100;
            }
            else
            {
                agapite.RunicMinIntensity = 40;
                agapite.RunicMaxIntensity = 80;
            }

            CraftAttributeInfo verite = Verite = new CraftAttributeInfo();

            verite.ArmorPhysicalResist = 3;
            verite.ArmorFireResist = 3;
            verite.ArmorColdResist = 2;
            verite.ArmorPoisonResist = 3;
            verite.ArmorEnergyResist = 1;
            verite.WeaponPoisonDamage = 40;
            verite.WeaponEnergyDamage = 20;
            verite.RunicMinAttributes = 4;
            verite.RunicMaxAttributes = 5;
            if (Core.ML)
            {
                verite.RunicMinIntensity = 70;
                verite.RunicMaxIntensity = 100;
            }
            else
            {
                verite.RunicMinIntensity = 45;
                verite.RunicMaxIntensity = 90;
            }

            CraftAttributeInfo valorite = Valorite = new CraftAttributeInfo();

            valorite.ArmorPhysicalResist = 4;
            valorite.ArmorColdResist = 3;
            valorite.ArmorPoisonResist = 3;
            valorite.ArmorEnergyResist = 3;
            valorite.ArmorDurability = 50;
            valorite.WeaponFireDamage = 10;
            valorite.WeaponColdDamage = 20;
            valorite.WeaponPoisonDamage = 10;
            valorite.WeaponEnergyDamage = 20;
            valorite.RunicMinAttributes = 5;
            valorite.RunicMaxAttributes = 5;
            if (Core.ML)
            {
                valorite.RunicMinIntensity = 85;
                valorite.RunicMaxIntensity = 100;
            }
            else
            {
                valorite.RunicMinIntensity = 50;
                valorite.RunicMaxIntensity = 100;
            }

            CraftAttributeInfo spined = Spined = new CraftAttributeInfo();

            spined.ArmorPhysicalResist = 5;
            spined.ArmorLuck = 40;
            spined.RunicMinAttributes = 1;
            spined.RunicMaxAttributes = 3;
            if (Core.ML)
            {
                spined.RunicMinIntensity = 40;
                spined.RunicMaxIntensity = 100;
            }
            else
            {
                spined.RunicMinIntensity = 20;
                spined.RunicMaxIntensity = 40;
            }

            CraftAttributeInfo horned = Horned = new CraftAttributeInfo();

            horned.ArmorPhysicalResist = 2;
            horned.ArmorFireResist = 3;
            horned.ArmorColdResist = 2;
            horned.ArmorPoisonResist = 2;
            horned.ArmorEnergyResist = 2;
            horned.RunicMinAttributes = 3;
            horned.RunicMaxAttributes = 4;
            if (Core.ML)
            {
                horned.RunicMinIntensity = 45;
                horned.RunicMaxIntensity = 100;
            }
            else
            {
                horned.RunicMinIntensity = 30;
                horned.RunicMaxIntensity = 70;
            }

            CraftAttributeInfo barbed = Barbed = new CraftAttributeInfo();

            barbed.ArmorPhysicalResist = 2;
            barbed.ArmorFireResist = 1;
            barbed.ArmorColdResist = 2;
            barbed.ArmorPoisonResist = 3;
            barbed.ArmorEnergyResist = 4;
            barbed.RunicMinAttributes = 4;
            barbed.RunicMaxAttributes = 5;
            if (Core.ML)
            {
                barbed.RunicMinIntensity = 50;
                barbed.RunicMaxIntensity = 100;
            }
            else
            {
                barbed.RunicMinIntensity = 40;
                barbed.RunicMaxIntensity = 100;
            }

            CraftAttributeInfo red = RedScales = new CraftAttributeInfo();

            red.ArmorFireResist = 10;
            red.ArmorColdResist = -3;

            CraftAttributeInfo yellow = YellowScales = new CraftAttributeInfo();

            yellow.ArmorPhysicalResist = -3;
            yellow.ArmorLuck = 20;

            CraftAttributeInfo black = BlackScales = new CraftAttributeInfo();

            black.ArmorPhysicalResist = 10;
            black.ArmorEnergyResist = -3;

            CraftAttributeInfo green = GreenScales = new CraftAttributeInfo();

            green.ArmorFireResist = -3;
            green.ArmorPoisonResist = 10;

            CraftAttributeInfo white = WhiteScales = new CraftAttributeInfo();

            white.ArmorPhysicalResist = -3;
            white.ArmorColdResist = 10;

            CraftAttributeInfo blue = BlueScales = new CraftAttributeInfo();

            blue.ArmorPoisonResist = -3;
            blue.ArmorEnergyResist = 10;

            #region Mondain's Legacy
            CraftAttributeInfo oak = OakWood = new CraftAttributeInfo();

            oak.ArmorPhysicalResist = 3;
            oak.ArmorFireResist = 3;
            oak.ArmorPoisonResist = 2;
            oak.ArmorEnergyResist = 3;
            oak.ArmorLuck = 40;
            oak.ShieldPhysicalResist = 1;
            oak.ShieldFireResist = 1;
            oak.ShieldColdResist = 1;
            oak.ShieldPoisonResist = 1;
            oak.ShieldEnergyResist = 1;
            oak.WeaponLuck = 40;
            oak.WeaponDamage = 5;
            oak.RunicMinAttributes = 1;
            oak.RunicMaxAttributes = 2;
            oak.RunicMinIntensity = 1;
            oak.RunicMaxIntensity = 50;

            CraftAttributeInfo ash = AshWood = new CraftAttributeInfo();

            ash.ArmorPhysicalResist = 4;
            ash.ArmorFireResist = 2;
            ash.ArmorColdResist = 4;
            ash.ArmorPoisonResist = 1;
            ash.ArmorEnergyResist = 6;
            ash.ArmorLowerRequirements = 20;
            ash.ShieldEnergyResist = 3;
            ash.WeaponSwingSpeed = 10;
            ash.WeaponLowerRequirements = 20;
            ash.RunicMinAttributes = 2;
            ash.RunicMaxAttributes = 3;
            ash.RunicMinIntensity = 35;
            ash.RunicMaxIntensity = 75;

            CraftAttributeInfo yew = YewWood = new CraftAttributeInfo();

            yew.ArmorPhysicalResist = 6;
            yew.ArmorFireResist = 3;
            yew.ArmorColdResist = 3;
            yew.ArmorEnergyResist = 3;
            yew.ArmorRegenHits = 1;
            yew.ShieldPhysicalResist = 3;
            yew.WeaponHitChance = 5;
            yew.WeaponDamage = 10;
            yew.RunicMinAttributes = 3;
            yew.RunicMaxAttributes = 3;
            yew.RunicMinIntensity = 40;
            yew.RunicMaxIntensity = 90;

            CraftAttributeInfo heartwood = Heartwood = new CraftAttributeInfo();

            heartwood.ArmorPhysicalResist = 2;
            heartwood.ArmorFireResist = 3;
            heartwood.ArmorColdResist = 2;
            heartwood.ArmorPoisonResist = 7;
            heartwood.ArmorEnergyResist = 2;

            // one of below
            heartwood.RandomBonus = true;
            heartwood.ArmorDamage = 10;
            heartwood.ArmorHitChance = 5;
            heartwood.ArmorLuck = 40;
            heartwood.ArmorLowerRequirements = 20;
            heartwood.ArmorMage = 1;

            // one of below
            heartwood.WeaponDamage = 10;
            heartwood.WeaponHitChance = 5;
            heartwood.WeaponHitLifeLeech = 13;
            heartwood.WeaponLuck = 40;
            heartwood.WeaponLowerRequirements = 20;
            heartwood.WeaponSwingSpeed = 10;

            heartwood.RunicMinAttributes = 4;
            heartwood.RunicMaxAttributes = 4;
            heartwood.RunicMinIntensity = 50;
            heartwood.RunicMaxIntensity = 100;

            CraftAttributeInfo bloodwood = Bloodwood = new CraftAttributeInfo();

            bloodwood.ArmorPhysicalResist = 3;
            bloodwood.ArmorFireResist = 8;
            bloodwood.ArmorColdResist = 1;
            bloodwood.ArmorPoisonResist = 3;
            bloodwood.ArmorEnergyResist = 3;
            bloodwood.ArmorRegenHits = 2;
            bloodwood.ShieldFireResist = 3;
            bloodwood.WeaponRegenHits = 2;
            bloodwood.WeaponHitLifeLeech = 16;

            CraftAttributeInfo frostwood = Frostwood = new CraftAttributeInfo();

            frostwood.ArmorPhysicalResist = 2;
            frostwood.ArmorFireResist = 1;
            frostwood.ArmorColdResist = 8;
            frostwood.ArmorPoisonResist = 3;
            frostwood.ArmorEnergyResist = 4;
            frostwood.ShieldColdResist = 3;
            frostwood.WeaponColdDamage = 40;
            frostwood.WeaponDamage = 12;
            #endregion
        }
    }
}