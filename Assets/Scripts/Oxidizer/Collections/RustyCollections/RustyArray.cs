using System;
using System.Runtime.InteropServices;

namespace Oxidizer.Collections.RustyCollections
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RustyArray
    {
        public IntPtr _array;
        public UIntPtr _length;
    }
}