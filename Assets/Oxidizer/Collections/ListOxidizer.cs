using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Oxidizer.Collections.RustyCollections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Oxidizer.Collections
{
    public class ListOxidizer<T> : IDisposable, IEnumerable<T> where T : unmanaged
    {
        private unsafe RustyList* _rustyList;
        private NativeArray<T> _nativeArray;

        public unsafe int Length => _rustyList->_length;
        public IntPtr RustyListPointer { get; }
        public unsafe NativeArray<T> ExtractedNativeArray => _nativeArray.GetSubArray(0, _rustyList->_length);

        public unsafe ListOxidizer(int capacity)
        {
            _nativeArray = new NativeArray<T>(capacity, Allocator.Persistent);

            int rustySize = Marshal.SizeOf<RustyList>();
            RustyListPointer = Marshal.AllocHGlobal(rustySize);
            _rustyList = (RustyList*) RustyListPointer;
            _rustyList->_array = (IntPtr) _nativeArray.GetUnsafePtr();
            _rustyList->_length = 0;
            _rustyList->_capacity = (UIntPtr) capacity;
        }

        public unsafe T this[int index]
        {
            get
            {
                if (index < 0 || index > _rustyList->_length)
                    throw new IndexOutOfRangeException($"Index {index} is out of range " +
                                                       $"for range 0-{_rustyList->_length}");
                return _nativeArray[index];
            }

            set
            {
                if (index < 0 || index > _rustyList->_length)
                    throw new IndexOutOfRangeException($"Index {index} is out of range " +
                                                       $"for range 0-{_rustyList->_length}");
                _nativeArray[index] = value;
            }
        }

        public unsafe void Dispose()
        {
            _nativeArray.Dispose();
            Marshal.FreeHGlobal(RustyListPointer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListOxidizerEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}