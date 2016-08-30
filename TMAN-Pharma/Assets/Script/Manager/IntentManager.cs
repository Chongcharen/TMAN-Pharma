using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class IntentManager : MonoBehaviour {
    private static IntentManager instance;
    public int currentIntent = 0;
    public int olderIntent = 0;
    public int newIntent = 0;
    public List<AbstractIntent> listPage;
    public static IntentManager GetInstance() {
        if (instance == null)
        {
            instance = new GameObject("Intentmanager").AddComponent<IntentManager>();
        }
        return instance;
    }
    void Awake()
    {
        instance = this;
    }
    public void SetIntent(Intent intent)
    {
        newIntent = (int)intent;
        Debug.Log("newIntent " + newIntent);
        listPage[newIntent].SlideLeft();
    }
	
}
public enum Intent{
    Login = 0
}