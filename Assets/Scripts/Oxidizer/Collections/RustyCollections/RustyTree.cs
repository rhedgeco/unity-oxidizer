using System;
using System.Runtime.InteropServices;

namespace Oxidizer.Collections.RustyCollections
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RustyTree
    {
        public int _depth;
        public int _childCount;
        public IntPtr _array;
        public UIntPtr _length;
    }
}