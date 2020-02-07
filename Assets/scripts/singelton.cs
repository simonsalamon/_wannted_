﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singelton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance{
        get{
            if (instance == null)
                instance = FindObjectOfType<T>();
            else if(instance != FindObjectOfType<T>())
                Destroy(instance);
            return instance;
        }
    }
}
