using System;
using System.Runtime.InteropServices;
using Oxidizer.Collections.RustyCollections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Oxidizer.Collections
{
    public class ArrayOxidizer<T> : IDisposable where T : unmanaged
    {
        private unsafe RustyArray* _rustyArray;
        
        public IntPtr RustyArrayPointer { get; }
        public NativeArray<T> NativeArray { get; }

        public unsafe ArrayOxidizer(int size)
        {
            NativeArray = new NativeArray<T>(size, Allocator.Persistent);

            int rustySize = Marshal.SizeOf<RustyArray>();
            RustyArrayPointer = Marshal.AllocHGlobal(rustySize);
            _rustyArray = (RustyArray*) RustyArrayPointer;
            _rustyArray->_array = (IntPtr) NativeArray.GetUnsafePtr();
            _rustyArray->_length = (UIntPtr) NativeArray.Length;
        }

        public void Dispose()
        {
            if (NativeArray.IsCreated)
                NativeArray.Dispose();
            Marshal.FreeHGlobal(RustyArrayPointer);
        }
    }
}