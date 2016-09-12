using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class DropdownPopup : MonoBehaviour {
	public Dropdown dropdown;
	public Dropdown target;
	public void DropdownUpdate(Dropdown _target){
		target = _target;
		dropdown.value = target.value;
		dropdown.options = target.options;
		dropdown.gameObject.SetActive (true);
		dropdown.Show ();
		dropdown.onValueChanged.AddListener (OnValueChanged);
	}
	void OnValueChanged(int index){
		Events.instance.DropdownSelect_Dispatch (target,index);
		ClosePopup ();
	}
	public void OnSelect(){
		Events.instance.DropdownSelect_Dispatch (target,dropdown.value);
		ClosePopup ();
	}
	void ClosePopup(){
		//dropdown.ClearOptions ();
		//dropdown.Hide ();
		Destroy(GameObject.Find("Dropdown List"));
		gameObject.SetActive (false);
	}
}
