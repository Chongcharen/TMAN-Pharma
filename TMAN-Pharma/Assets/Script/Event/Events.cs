using UnityEngine;
using System.Collections;

public class Events : MonoBehaviour {
    public delegate void PageReadyEvent();
    public static event PageReadyEvent PageReady;

    public delegate void OpenLoaderEvent();
    public static event OpenLoaderEvent OpenLoader;

    public static Events instance;

    void Awake()
    {
        instance = this;
    }
    public void PageReady_Dispatch()
    {
        PageReady();
    }

    public void OpenLoader_Dispatch()
    {
        OpenLoader();
    }
}
