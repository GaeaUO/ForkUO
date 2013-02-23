using System;

namespace Server.Crafting
{
    public interface IResource
    {
        ResourceInfo Info { get; }
        String ResourceGroup { get; }
        ResourceString Resource { get; set; }
    }
}
