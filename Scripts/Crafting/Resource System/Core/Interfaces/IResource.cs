using System;

namespace Server.Resources
{
    public interface IResource
    {
        ResourceInfo Info { get; }
        String ResourceGroup { get; }
        object PreloadResource { get; }
        ResourceString Resource { get; set; }
    }
}
