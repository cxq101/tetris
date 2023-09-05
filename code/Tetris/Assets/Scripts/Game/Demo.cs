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
     
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Unity Random=========" + UnityEngine.Random.Range(1, 3));
        Debug.Log("System Random=========" + new System.Random().Next(1, 3));
    }
}
