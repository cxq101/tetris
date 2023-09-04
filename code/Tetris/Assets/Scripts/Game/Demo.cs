using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class A
{
    public int age = 1;
}
public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] intarr = {1, 2, 3, 4, 4};
        Debug.Log("contains 2: " + intarr.Contains(2));
        Debug.Log("contains 10: " + intarr.Contains(10));
        Debug.Log("Array indexof 2: " + Array.IndexOf(intarr, 2));
        Debug.Log("Array indexof 10: " + Array.IndexOf(intarr, 10));

        A a1 = new A();
        A a2 = new A();
        A a3 = new A();
        A a4 = new A();
        A[] Aarr = { a1, a2, a3 };
        Debug.Log("contains a2: " + Aarr.Contains(a2));
        Debug.Log("contains a4: " + Aarr.Contains(a4));
        Debug.Log("Array index a2: " + Array.IndexOf(Aarr, a2));
        Debug.Log("Array index a4: " + Array.IndexOf(Aarr, a4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
