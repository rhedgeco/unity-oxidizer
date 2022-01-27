using System;
using System.Runtime.InteropServices;

namespace Oxidizer.Collections.RustyCollections
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RustyList
    {
        public IntPtr _array;
        public int _length;
        public UIntPtr _capacity;
    }
}