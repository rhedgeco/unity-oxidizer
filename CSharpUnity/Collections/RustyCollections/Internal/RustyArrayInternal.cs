using System;
using System.Runtime.InteropServices;

namespace Oxidizer.Collections.RustyCollections.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RustyArrayInternal
    {
        public IntPtr _array;
        public UIntPtr _length;
    }
}