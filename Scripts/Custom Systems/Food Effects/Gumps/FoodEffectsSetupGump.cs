using System;
using System.Collections.Generic;

using Server;
using Server.Gumps;

namespace CustomsFramework.Systems.FoodEffects
{
    public class FoodEffectsSetupGump : Gump
    {
        private Boolean _CoreEnabled;
        private Int32 _EffectIndex;

        private List<Type> _FoodTypes = new List<Type>();
        private List<FoodEffect> _FoodEffects = new List<FoodEffect>();

        public FoodEffectsSetupGump()
            : base(150, 100)
        {
            if (FoodEffectsCore.Core == null)
                return;

            _CoreEnabled = FoodEffectsCore.Core.Enabled;
            _EffectIndex = 0;

            foreach (KeyValuePair<Type, FoodEffect> pair in FoodEffectsCore.Core.FoodEffects)
                _FoodTypes.Add(pair.Key);

            _FoodTypes.Sort(new TypeSorter());

            for (Int32 i = 0; i < _FoodTypes.Count; i++)
                _FoodEffects.Add(FoodEffectsCore.Core.FoodEffects[_FoodTypes[i]]);

            Setup();
        }

        public FoodEffectsSetupGump(Boolean coreEnabled, List<Type> foodTypes, List<FoodEffect> foodEffects, Int32 effectIndex)
            : base(150, 100)
        {
            _CoreEnabled = coreEnabled;
            _EffectIndex = effectIndex;
            _FoodTypes = foodTypes;
            _FoodEffects = foodEffects;

            Setup();
        }

        private void Setup()
        {
            AddPage(0);

            AddBackground(0, 0, 260, 336, 5100);

            AddLabel(66, 4, 2062, "Food Effect System");

            AddLabel(10, 30, 1359, "Enabled");
            Add(new GreyCheckbox(63, 25, "Core Enabled", _CoreEnabled));

            AddImageTiled(2, 55, 254, 4, 5101);

            AddLabel(64, 60, 62, "Food Buffs");

            AddLabel(10, 310, 75, String.Format("v{0}", FoodEffectsCore.Core.Version));

            Add(new SaveCancelGumpling(90, 305, SaveButtonPressed, null));

            List<Type> foodTypes = new List<Type>();

            Boolean lastEntryBlank = false;

            for (Int32 i = _EffectIndex; i < _FoodTypes.Count && i < _EffectIndex + 10; i++)
            {
                foodTypes.Add(_FoodTypes[i]);

                if (_FoodTypes[i] == null)
                    lastEntryBlank = true;
            }

            Boolean anotherPage = false;

            if (foodTypes.Count == 10)
                anotherPage = true;
            else if (!lastEntryBlank)
                foodTypes.Add(null);

            if (_EffectIndex > 0)
                Add(new GreyLeftArrow(10, 63, PreviousPagePressed));

            if (anotherPage)
                Add(new GreyRightArrow(172, 63, NextPagePressed));

            Int32 counter = _EffectIndex;

            for (Int32 i = 0; i < foodTypes.Count; i++)
                Add(new FoodEntryGumpling(counter++, 10, 80 + (i * 22), (foodTypes[i] != null ? foodTypes[i].Name : ""), AddEffectPressed, RemoveEffectPressed));
        }

        public override void OnAddressChange()
        {
            if (Address != null)
            {
                Address.CloseGump(typeof(FoodEffectsSetupGump));
                Address.CloseGump(typeof(FoodEffectGump));
            }
        }

        private void PreviousPagePressed(GumpEntry entry, object param)
        {
            if (Address != null)
                Address.SendGump(new FoodEffectsSetupGump(GetCheck("Core Enabled"), _FoodTypes, _FoodEffects, _EffectIndex - 10));
        }

        private void NextPagePressed(GumpEntry entry, object param)
        {
            if (Address != null)
                Address.SendGump(new FoodEffectsSetupGump(GetCheck("Core Enabled"), _FoodTypes, _FoodEffects, _EffectIndex + 10));
        }

        private void SaveButtonPressed(GumpEntry entry, object param)
        {
            FoodEffectsCore.Core.Enabled = GetCheck("Core Enabled");

            FoodEffectsCore.Core.FoodEffects.Clear();

            for (Int32 i = 0; i < _FoodTypes.Count; i++)
                if (_FoodTypes[i] != null)
                    FoodEffectsCore.Core.FoodEffects[_FoodTypes[i]] = _FoodEffects[i];

            if (Address != null)
                Address.SendMessage("Food effect System is {0}!  System contains {1} foods defined.", (FoodEffectsCore.Core.Enabled ? "enabled" : "disabled"), FoodEffectsCore.Core.FoodEffects.Keys.Count);

            FoodEffectsCore.InvokeOnFoodEffectSystemUpdate(FoodEffectsCore.Core);
        }

        private void AddEffectPressed(GumpEntry entry, object param)
        {
            if (entry.Parent is FoodEntryGumpling)
            {
                Int32 index = ((FoodEntryGumpling)entry.Parent).Index;

                if (Address != null)
                    Address.SendGump(new FoodEffectGump(GetCheck("Core Enabled"), _FoodTypes, _FoodEffects, index, null, false));
            }
        }

        private void RemoveEffectPressed(GumpEntry entry, object param)
        {
            if (entry.Parent is FoodEntryGumpling)
            {
                Int32 index = ((FoodEntryGumpling)entry.Parent).Index;

                if (index < _FoodTypes.Count)
                {
                    _FoodTypes.RemoveAt(index);
                    _FoodEffects.RemoveAt(index);
                }

                if (Address != null)
                    Address.SendGump(new FoodEffectsSetupGump(_CoreEnabled, _FoodTypes, _FoodEffects, _EffectIndex));
            }
        }

        private class TypeSorter : IComparer<Type>
        {
            public Int32 Compare(Type x, Type y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }
    }
}