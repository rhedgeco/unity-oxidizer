using System;
using System.Runtime.InteropServices;
using Oxidizer.Collections.RustyCollections;
using Oxidizer.Collections.RustyCollections.Internal;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Oxidizer.Collections
{
    public class ListOxidizer<T> : IDisposable where T : unmanaged
    {
        private NativeArray<T> _nativeArray;
        private readonly int _capacity;

        public RustyList<T> RustyList { get; }
        public NativeArray<T> ExtractedNativeArray => _nativeArray.GetSubArray(0, _capacity);

        public unsafe ListOxidizer(int capacity)
        {
            _capacity = capacity;
            _nativeArray = new NativeList<T>(_capacity, Allocator.Persistent);

            int rustySize = Marshal.SizeOf<RustyListInternal>();
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(rustySize);
            RustyListInternal* rustyList = (RustyListInternal*) unmanagedPointer;
            rustyList->_array = (IntPtr) _nativeArray.GetUnsafePtr();
            rustyList->_length = 0;
            rustyList->_capacity = (UIntPtr) _capacity;

            RustyList = new RustyList<T> {_internalRustyList = unmanagedPointer};
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(RustyList._internalRustyList);
            if (_nativeArray.IsCreated)
                _nativeArray.Dispose();
        }
    }
}