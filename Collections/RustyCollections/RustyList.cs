using System;
using System.Runtime.InteropServices;

namespace Oxidizer.Collections.RustyCollections
{
    [StructLayout(LayoutKind.Sequential)]
    // ReSharper disable once UnusedTypeParameter
    public struct RustyList<T> where T : unmanaged
    {
        internal IntPtr _internalRustyList;
    }
}