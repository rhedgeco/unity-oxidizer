using System;
using System.Runtime.InteropServices;

namespace Oxidizer.Collections.RustyCollections.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RustyListInternal
    {
        public IntPtr _array;
        public int _length;
        public UIntPtr _capacity;
    }
}