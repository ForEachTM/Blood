using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDummie
{
    //T dummy;

    void DoDummy<T>(T dummy) where T: class
    {
        Type type = typeof(T);
        
    }
}
