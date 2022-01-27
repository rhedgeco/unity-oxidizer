using System;
using System.Runtime.InteropServices;
using System.Text;
using Oxidizer.Collections;
using UnityEngine;

public class TestOxidizer : MonoBehaviour
{
    [DllImport("unity_oxidizer_usage", EntryPoint = "test_array_populate")]
    private static extern void TestArrayPopulate(IntPtr array);
    
    [DllImport("unity_oxidizer_usage", EntryPoint = "test_list_populate")]
    private static extern void TestListPopulate(IntPtr list);

    private ArrayOxidizer<float> _array;
    private ListOxidizer<float> _list;

    private void Awake()
    {
        _array = new ArrayOxidizer<float>(5);
        _list = new ListOxidizer<float>(5);
        
        TestArrayPopulate(_array.RustyArrayPointer);
        TestListPopulate(_list.RustyListPointer);

        StringBuilder arrayItems = new StringBuilder();
        foreach (float f in _array.NativeArray)
        {
            arrayItems.Append($" {f},");
        }
        Debug.Log($"Array Items: ({arrayItems} )");
        
        StringBuilder listItems = new StringBuilder();
        foreach (float f in _list)
        {
            listItems.Append($" {f},");
        }
        Debug.Log($"List Items: ({listItems} )");
    }

    private void OnDestroy()
    {
        _array.Dispose();
        _list.Dispose();
    }
}
