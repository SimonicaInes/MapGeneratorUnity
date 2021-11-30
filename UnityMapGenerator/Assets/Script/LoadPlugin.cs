using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LoadPlugin : MonoBehaviour
{


    [DllImport("MapGeneratorPlugin", CallingConvention = CallingConvention.Cdecl)]

    private static extern int PrintMyNumber();




    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PrintMyNumber());
    }


   
}
