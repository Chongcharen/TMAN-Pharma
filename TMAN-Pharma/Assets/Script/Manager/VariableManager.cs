﻿using UnityEngine;
using System.Collections;

public class VariableManager : MonoBehaviour {
    private static VariableManager instance;
    public Rect screenCanvas;
	public bool canDispatchListener = false;
    public static VariableManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("VariableManager").AddComponent<VariableManager>();

            }
            return instance;
        }
    }
}
