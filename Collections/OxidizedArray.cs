using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Oxidizer.Collections
{
    [StructLayout(LayoutKind.Sequential)]
    // ReSharper disable once StructCanBeMadeReadOnly
    public struct OxidizedArray<T> : IDisposable where T : unmanaged
    {
        private readonly unsafe IntPtr* _unmanagedArray;
        private readonly UIntPtr _size;

        public unsafe OxidizedArray(int size, bool cleanMemory = true)
        {
            int memSize = Marshal.SizeOf<IntPtr>();
            _unmanagedArray = (IntPtr*) Marshal.AllocHGlobal(memSize * size);
            _size = (UIntPtr) size;
            
            if (cleanMemory) UnsafeUtility.MemClear(_unmanagedArray, memSize * size);
        }

        public int Length => (int) _size;

        public unsafe T this[int index]
        {
            get
            {
                if (index < 0 || index >= (int) _size)
                    throw new IndexOutOfRangeException();
                return *(T*) ((IntPtr) _unmanagedArray + index * sizeof(IntPtr));
            }

            set
            {
                if (index < 0 || index >= (int) _size)
                    throw new IndexOutOfRangeException();
                *(T*) ((IntPtr) _unmanagedArray + index * sizeof(IntPtr)) = value;
            }
        }

        public unsafe void Dispose()
        {
            Marshal.FreeHGlobal((IntPtr) _unmanagedArray);
        }
    }
}