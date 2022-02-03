using System;
using System.Runtime.InteropServices;
using Oxidizer.Collections.RustyCollections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Oxidizer.Collections
{
    public class TreeOxidizer<T> where T : struct
    {
        private int _depth;
        private int _childCount;
        private NativeArray<RustyTreeNode<T>> _nativeArray;
        private unsafe RustyTree* _rustyTree;

        public RustyTreeNode<T> Root => _nativeArray[0];
        public IntPtr RustyArrayPointer { get; }

        public unsafe TreeOxidizer(int depth, int childCount)
        {
            if (depth <= 0) throw new ArgumentException("Tree 'depth' must be greater than 0");
            if (childCount <= 0) throw new ArgumentException("Tree 'childCount' must be greater than 0");
            _depth = depth;
            _childCount = childCount;

            PopulateArrays();
            int rustySize = Marshal.SizeOf<RustyTree>();
            RustyArrayPointer = Marshal.AllocHGlobal(rustySize);
            _rustyTree = (RustyTree*) RustyArrayPointer;
            _rustyTree->_depth = _depth;
            _rustyTree->_childCount = _childCount;
            _rustyTree->_array = (IntPtr) _nativeArray.GetUnsafePtr();
            _rustyTree->_length = (UIntPtr) _nativeArray.Length;
        }

        public RustyTreeNode<T> GetRootNode() => _nativeArray[0];

        public NativeArray<RustyTreeNode<T>> GetChildren(RustyTreeNode<T> node)
        {
            if (node.IsLeaf) throw new ArgumentException("node is a leaf and has no children");
            return _nativeArray.GetSubArray(node._internalChildrenIndex, _childCount);
        }

        public void Dispose()
        {
            if (_nativeArray.IsCreated)
                _nativeArray.Dispose();
            Marshal.FreeHGlobal(RustyArrayPointer);
        }

        private void PopulateArrays()
        {
            // to figure out the length of the array, we need to add the size of each layer together
            // initially start with a length 1, which is the size of the root node
            int nativeLength = 1;
            
            for (
                // layer: determines how deep in the tree we are
                // lCount: determines how many items there are in that layer
                int layer = 1, lCount = _childCount;
                
                // loop until our we are on the last layer
                layer < _depth;
                
                // increase the layer
                // then multiply the lCount to reflect the number of elements in the new layer
                layer++, lCount *= _childCount
            )
            {
                // add the lCount to the total
                nativeLength += lCount;
            }

            // create array and assign
            _nativeArray = new NativeArray<RustyTreeNode<T>>(nativeLength, Allocator.Persistent);
            _nativeArray[0] = new RustyTreeNode<T> {_internalChildrenIndex = 1};

            // this is incredibly complex, but it lays out the tree in a flat way
            // the way that it lies means that each node only has to track the starting index of its children
            // the logic of the loop is similar to when we found the length of the array
            for (
                // layer: determines how deep in the tree we are
                // lCount: determines how many items there are in that layer
                // lIndex: determines at what index in the array the current layer starts
                int layer = 1, lCount = _childCount, lIndex = 1;

                // loop until our we are on the last layer
                layer < _depth;

                // increase the layer
                // increment the lIndex by the amount of elements in the last layer
                // then multiply the lCount to reflect the number of elements in the new layer
                layer++, lIndex += lCount, lCount *= _childCount
            )
            {
                // localIndex: determines the index for the current element within the layer
                for (int localIndex = 0; localIndex < lCount; localIndex++)
                {
                    // treeIndex: is the absolute index for the item
                    int treeIndex = lIndex + localIndex;

                    // childIndex: if the child will not be a leaf, calculate where its children would be
                    // where the children will be is found by doing the following:
                    //     1. lIndex + lCount to get the index of the next layer
                    //     2. localIndex * _childCount to offset the localIndex by the next layers factor
                    int childIndex = layer == _depth - 1 ? 0 : lIndex + lCount + localIndex * _childCount;

                    // finally, assign the values to the node
                    _nativeArray[treeIndex] = new RustyTreeNode<T>
                    {
                        _internalLayer = layer,
                        _internalChildrenIndex = childIndex
                    };
                }
            }
        }
    }
}