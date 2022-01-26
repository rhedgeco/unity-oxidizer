using System;
using System.Runtime.InteropServices;
using Oxidizer.Collections.RustyCollections;
using Oxidizer.Collections.RustyCollections.Internal;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Oxidizer.Collections
{
    public class ArrayOxidizer<T> : IDisposable where T : unmanaged
    {
        public RustyArray<T> RustyArray { get; }
        public NativeArray<T> NativeArray { get; }

        public unsafe ArrayOxidizer(int size)
        {
            NativeArray = new NativeArray<T>(size, Allocator.Persistent);

            int rustySize = Marshal.SizeOf<RustyArrayInternal>();
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(rustySize);
            RustyArrayInternal* rustyArray = (RustyArrayInternal*) unmanagedPointer;
            rustyArray->_array = (IntPtr) NativeArray.GetUnsafePtr();
            rustyArray->_length = (UIntPtr) NativeArray.Length;

            RustyArray = new RustyArray<T> {_internalRustyArray = unmanagedPointer};
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(RustyArray._internalRustyArray);
            if (NativeArray.IsCreated)
                NativeArray.Dispose();
        }
    }
}