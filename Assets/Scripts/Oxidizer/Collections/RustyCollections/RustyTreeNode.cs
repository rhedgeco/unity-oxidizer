using System.Runtime.InteropServices;

namespace Oxidizer.Collections.RustyCollections
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RustyTreeNode<T> where T : struct
    {
        // fields
        // these are what rust sees
        internal int _internalChildrenIndex;
        internal int _internalLayer;
        public T Item;
        
        // properties
        public bool IsRoot => _internalLayer == 0;
        public bool IsLeaf => _internalChildrenIndex == 0;
    }
}