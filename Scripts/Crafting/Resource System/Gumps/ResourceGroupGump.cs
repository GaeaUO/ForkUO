using System;
using System.Collections.Generic;

using Server;
using Server.Gumps;

namespace Server.Resources
{
    public class ResourceGroupGump : Gump
    {
        private const Int32 HUE_ValidEntry = 0;
        private const Int32 HUE_InvalidEntry = 0x22;

        private Int32 _GroupIndex;
        private Int32 _ResourceIndex;

        private List<String> _DefinedGroups = new List<string>();
        private Dictionary<String, List<ResourceInfo>> _DefinedResources = new Dictionary<string, List<ResourceInfo>>();

        private String _GroupName;
        private List<ResourceInfo> _Resources;

        public ResourceGroupGump(List<String> definedGroups, Dictionary<String, List<ResourceInfo>> definedResources, Int32 groupIndex, String groupName, List<ResourceInfo> resources, Int32 resourceIndex, Boolean validate) : base(150, 100)
        {
            _GroupIndex = groupIndex;
            _DefinedGroups = definedGroups;
            _DefinedResources = definedResources;
            _GroupName = groupName;
            _Resources = resources;
            _ResourceIndex = resourceIndex;

            if (_GroupIndex < 0)
                _GroupIndex = 0;

            if (_ResourceIndex < 0)
                _ResourceIndex = 0;

            if (_GroupName == null)
            {
                _GroupName = "";
                _Resources = new List<ResourceInfo>();

                if (_GroupIndex < _DefinedGroups.Count)
                {
                    _GroupName = _DefinedGroups[_GroupIndex];

                    foreach (ResourceInfo info in _DefinedResources[_GroupName])
                        _Resources.Add(info.Clone());
                }
            }

            Add(new StoneyBackground(300, 310));
            Add(new GumpLabel(10, 6, 0x0, "Resource Group :"));
            Add(new EntryField(120, 4, 170, "Group Name", (validate && _GroupName == "" ? HUE_InvalidEntry : HUE_ValidEntry), _GroupName));

            if (validate && _GroupName == "")
                Add(new GumpLabel(120, 25, HUE_InvalidEntry, "Group name is blank"));

            List<ResourceInfo> resList = new List<ResourceInfo>();

            Boolean lastEntryBlank = false;

            for (int i = _ResourceIndex; i < _Resources.Count && i < _ResourceIndex + 10; i++)
            {
                resList.Add(_Resources[i]);

                if (_Resources[i].ResourceName == "")
                    lastEntryBlank = true;
            }

            bool anotherPage = false;

            if (resList.Count == 10)
                anotherPage = true;
            else if (!lastEntryBlank)
            {
                resList.Add(new ResourceInfo(ResourceType.Harvested, "", 0, "", 0x0, null, 0.0, 0.0, new Items.CraftAttributeInfo(), null, 0, "", null, 0, "", false));
            }

            if (_ResourceIndex > 0)
                Add(new GreyLeftArrow(10, 33, PreviousPagePressed));

            if (anotherPage)
                Add(new GreyRightArrow(277, 33, NextPagePressed));

            Int32 counter = _ResourceIndex;

            for (int i = 0; i < resList.Count; i++)
            {
                LabelAddRemoveGumpling g = new LabelAddRemoveGumpling(counter++, 10, 55 + (i * 22), 220, resList[i].ResourceName);

                g.OnEdit += EditResourcePressed;
                g.OnRemove += RemoveResourcePressed;

                Add(g);
            }

            Add(new ApplyCancelGumpling(130, 280, ApplyButtonPressed, CancelButtonPressed));
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

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
            base.OnResponse(sender, info);
        }

        private void PreviousPagePressed(IGumpComponent sender, object param)
        {
            if (Address != null)
                Address.SendGump(new ResourceGroupGump(_DefinedGroups, _DefinedResources, _GroupIndex, _GroupName, _Resources, _ResourceIndex - 10, true));
        }

        private void NextPagePressed(IGumpComponent sender, object param)
        {
            if (Address != null)
                Address.SendGump(new ResourceGroupGump(_DefinedGroups, _DefinedResources, _GroupIndex, _GroupName, _Resources, _ResourceIndex + 10, true));
        }

        private void ApplyButtonPressed(IGumpComponent sender, object param)
        {
            _GroupName = GetTextEntry("Group Name");

            if (Address == null)
                return;

            if (_GroupName == "")
                Address.SendGump(new ResourceGroupGump(_DefinedGroups, _DefinedResources, _GroupIndex, _GroupName, _Resources, _ResourceIndex, true));
            else
            {
                if (_GroupIndex < _DefinedGroups.Count)
                    _DefinedGroups[_GroupIndex] = _GroupName;
                else
                    _DefinedGroups.Add(_GroupName);

                _DefinedResources[_GroupName] = _Resources;

                Address.SendGump(new ResourceServiceGump(_DefinedGroups, _DefinedResources, _GroupIndex / 10));
            }
        }

        private void CancelButtonPressed(IGumpComponent sender, object param)
        {
            if (Address != null)
                Address.SendGump(new ResourceServiceGump(_DefinedGroups, _DefinedResources, _GroupIndex / 10));
        }

        private void EditResourcePressed(IGumpComponent sender, object param)
        {
            if (sender.Parent is LabelAddRemoveGumpling)
            {
                Int32 index = ((LabelAddRemoveGumpling)sender.Parent).Index;

                if (Address != null)
                    Address.SendGump(new ResourceDefinitionGump(_DefinedGroups, _DefinedResources, _GroupIndex, _GroupName, _Resources, index, null, ResourceType.Harvested, false, false));
            }
        }

        private void RemoveResourcePressed(IGumpComponent sender, object param)
        {
            if (sender.Parent is LabelAddRemoveGumpling)
            {
                Int32 index = ((LabelAddRemoveGumpling)sender.Parent).Index;

                if (index < _Resources.Count)
                    _Resources.RemoveAt(index);

                if (Address != null)
                    Address.SendGump(new ResourceGroupGump(_DefinedGroups, _DefinedResources, _GroupIndex, _GroupName, _Resources, _ResourceIndex, true));
            }
        }
    }
}
