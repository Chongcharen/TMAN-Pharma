using UnityEngine;
using System.Collections;

public class VariableManager : MonoBehaviour {
    private static VariableManager instance;
    public Rect screenCanvas;
    public static VariableManager GetInstance()
    {
        if(instance == null)
        {
            instance = new GameObject("VariableManager").AddComponent<VariableManager>();
        }
        return instance;
    }
}
