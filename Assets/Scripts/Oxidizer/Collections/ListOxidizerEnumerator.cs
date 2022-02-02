using System;
using System.Collections;
using System.Collections.Generic;

namespace Oxidizer.Collections
{
    public class ListOxidizerEnumerator<T> : IEnumerator<T> where T : unmanaged
    {
        private int _position = -1;
        private readonly ListOxidizer<T> _listOxidizer;

        public ListOxidizerEnumerator(ListOxidizer<T> listOxidizer)
        {
            _listOxidizer = listOxidizer;
        }

        public bool MoveNext()
        {
            _position++;
            return _position < _listOxidizer.Length;
        }

        public void Reset()
        {
            _position = -1;
        }

        public T Current
        {
            get
            {
                try
                {
                    return _listOxidizer[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // do nothing
        }
    }
}