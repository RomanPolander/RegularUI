using System.Collections.Generic;

namespace RegularUI.WPF
{
    internal static class PackIconDataFactory
    {
        internal static IDictionary<PackIconKind, string> Create() => new Dictionary<PackIconKind, string>
        {
            {PackIconKind.None,""},
        };
    }
}
