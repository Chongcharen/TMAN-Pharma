using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class PopupManager : MonoBehaviour {
    public GameObject screen;
    public GameObject loader;
    void Awake()
    {
        Events.OpenLoader += OpenLoader;
        Events.PageReady += CloseLoader;
    }

    private void OpenLoader()
    {
        screen.SetActive(true);
        loader.SetActive(true);
    }
    private void CloseLoader()
    {
        screen.SetActive(false);
        loader.SetActive(false);
    }
}
