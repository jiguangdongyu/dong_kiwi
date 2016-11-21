using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResListener
{
    public resLoaded callback;
    public string param;

    public ResListener(resLoaded cb = null, string pa = "")
    {
        callback = cb;
        param = pa;
    }

    public delegate void resLoaded(UnityEngine.Object obj, string param);
}

