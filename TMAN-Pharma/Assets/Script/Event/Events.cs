using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Events : MonoBehaviour {
    public delegate void PageReadyEvent();
    public static event PageReadyEvent PageReady;

    public delegate void OpenLoaderEvent();
    public static event OpenLoaderEvent OpenLoader;

	public delegate void LoadInstanceDropdownEvent (Dropdown target);
	public static event LoadInstanceDropdownEvent LoadInstanceDropdown;

	public delegate void DropdownSelectEvent(Dropdown dropdown,int index);
	public static event DropdownSelectEvent DropdownSelect;

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

	public void LoadInstanceDropdown_Dispatch(Dropdown target){
		LoadInstanceDropdown (target);
	}
	public void DropdownSelect_Dispatch(Dropdown target,int index){
		DropdownSelect (target,index);
	}
}
