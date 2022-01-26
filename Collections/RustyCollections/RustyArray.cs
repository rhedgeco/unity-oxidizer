using System;
using System.Runtime.InteropServices;

namespace Oxidizer.Collections.RustyCollections
{
    // we use this class as a wrapper for the internal rusty array
    // this is so we can pass around a 'loosely' typed object in C# and be somewhat confident about the contents
    // the generic type means nothing and simply exists for this purpose
    // we will also disable the warning in Intellij with a comment so we dont hear it complain lol 
    [StructLayout(LayoutKind.Sequential)]
    // ReSharper disable once UnusedTypeParameter
    public struct RustyArray<T> where T : unmanaged
    {
        internal IntPtr _internalRustyArray;
    }
}