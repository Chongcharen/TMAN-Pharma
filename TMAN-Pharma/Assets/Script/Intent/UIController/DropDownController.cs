using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class DropDownController : MonoBehaviour {
    [SerializeField]
    Dropdown dropdown;

    void OnEnable() {
        Events.DropdownSelect += Events_DropdownSelect;
    }
    void OnDisable()
    {
        Events.DropdownSelect -= Events_DropdownSelect;
    }

	void Events_DropdownSelect (Dropdown target, int index)
	{
		if (target == dropdown) {
			dropdown.value = index;
		}
	}

	public void ShowDropdownTarget(){
		dropdown.Hide ();
		Events.instance.LoadInstanceDropdown_Dispatch (dropdown);
	}
}
