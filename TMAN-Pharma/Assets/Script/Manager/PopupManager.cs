using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class PopupManager : MonoBehaviour {
    public GameObject screen,screenDropdown;
    public GameObject loader;

	public DropdownPopup dropdownPopup;
    void Awake()
    {
        Events.OpenLoader += OpenLoader;
        Events.PageReady += CloseLoader;
		Events.LoadInstanceDropdown += Events_LoadInstanceDropdown;
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

	void Events_LoadInstanceDropdown (Dropdown target)
	{
		//screenDropdown.SetActive (true);
		dropdownPopup.DropdownUpdate(target);
		//dropdownPopup.ClearOptions ();


	}
		
}
