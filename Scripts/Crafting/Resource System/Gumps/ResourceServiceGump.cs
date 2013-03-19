using System;
using System.Collections.Generic;

using Server;
using Server.Gumps;

namespace Server.Resources
{
    public class ResourceServiceGump : Gump
    {
        private Int32 _GroupIndex;

        private List<String> _DefinedGroups = new List<string>();
        private Dictionary<String, List<ResourceInfo>> _DefinedResources = new Dictionary<string, List<ResourceInfo>>();

        public ResourceServiceGump() : base(150, 100)
        {
            if (ResourceService.Service == null)
                return;

            _GroupIndex = 0;

            foreach(KeyValuePair<String, List<ResourceInfo>> pair in ResourceService.Service.DefinedResources)
            {
                List<ResourceInfo> infoList = new List<ResourceInfo>();

                foreach (ResourceInfo info in pair.Value)
                    infoList.Add(info.Clone());

                _DefinedGroups.Add(pair.Key);
                _DefinedResources.Add(pair.Key, infoList);
            }

            _DefinedGroups.Sort();

            Setup();
        }

        public ResourceServiceGump(List<String> definedGroups, Dictionary<String, List<ResourceInfo>> definedResources, Int32 groupIndex) : base(150, 100)
        {
            _GroupIndex = groupIndex;
            _DefinedGroups = definedGroups;
            _DefinedResources = definedResources;

            if (_GroupIndex < 0)
                _GroupIndex = 0;

            Setup();
        }

        private void Setup()
        {
            Add(new CustomServiceGumpling(300, 310, 0x80E, "Resource Groups", ResourceService.Service.Version, SaveButtonPressed));

            List<String> groups = new List<string>();

            Boolean lastEntryBlank = false;

            for (int i = _GroupIndex; i < _DefinedGroups.Count && i < _GroupIndex + 10; i++)
            {
                groups.Add(_DefinedGroups[i]);

                if (_DefinedGroups[i] == "")
                    lastEntryBlank = true;
            }

            bool anotherPage = false;

            if (groups.Count == 10)
                anotherPage = true;
            else if (!lastEntryBlank)
                groups.Add("");

            if (_GroupIndex > 0)
                Add(new GreyLeftArrow(10, 33, PreviousPagePressed));

            if (anotherPage)
                Add(new GreyRightArrow(277, 33, NextPagePressed));

            Int32 counter = _GroupIndex;

            for (int i = 0; i < groups.Count; i++)
            {
                LabelAddRemoveGumpling g = new LabelAddRemoveGumpling(counter++, 10, 55 + (i * 22), 220, groups[i]);

                g.OnEdit += EditGroupPressed;
                g.OnRemove += RemoveGroupPressed;

                Add(g);
            }
        }

        public override void OnAddressChange()
        {
            if (Address != null)
            {
                Address.CloseGump(typeof(ResourceServiceGump));
                Address.CloseGump(typeof(ResourceGroupGump));
                Address.CloseGump(typeof(ResourceDefinitionGump));
            }
        }

        private void PreviousPagePressed(IGumpComponent sender, object param)
        {
            if (Address != null)
                Address.SendGump(new ResourceServiceGump(_DefinedGroups, _DefinedResources, _GroupIndex - 10));
        }

        private void NextPagePressed(IGumpComponent sender, object param)
        {
            if (Address != null)
                Address.SendGump(new ResourceServiceGump(_DefinedGroups, _DefinedResources, _GroupIndex + 10));
        }

        private void SaveButtonPressed(IGumpComponent sender, object param)
        {
            ResourceService.Service.DefinedResources.Clear();

            foreach (KeyValuePair<String, List<ResourceInfo>> pair in _DefinedResources)
                ResourceService.Service.DefinedResources[pair.Key] = pair.Value;

            if (Address != null)
                Address.SendMessage("Resource System contains {0} resource groups.", ResourceService.Service.DefinedResources.Keys.Count);
        }

        private void EditGroupPressed(IGumpComponent sender, object param)
        {
            if (sender.Parent is LabelAddRemoveGumpling)
            {
                Int32 index = ((LabelAddRemoveGumpling)sender.Parent).Index;

                if (Address != null)
                    Address.SendGump(new ResourceGroupGump(_DefinedGroups, _DefinedResources, index, null, null, 0, false));
            }
        }

        private void RemoveGroupPressed(IGumpComponent sender, object param)
        {
            if (sender.Parent is LabelAddRemoveGumpling)
            {
                Int32 index = ((LabelAddRemoveGumpling)sender.Parent).Index;

                if (index < _DefinedGroups.Count)
                {
                    _DefinedResources.Remove(_DefinedGroups[index]);
                    _DefinedGroups.RemoveAt(index);
                }

                if (Address != null)
                    Address.SendGump(new ResourceServiceGump(_DefinedGroups, _DefinedResources, _GroupIndex));
            }
        }
    }
}
