using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Forwokingsound : MonoBehaviour
{
    public GameManager gameManager;
    public string WhatToLoad;
    private void Awake()
    {
        MethodInfo Load = typeof(GameManager).GetMethod(WhatToLoad);
        Load.Invoke(gameManager, null);
    }
}
