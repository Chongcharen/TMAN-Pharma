using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class DropdownPopup : MonoBehaviour {
	public Dropdown dropdown;
	public Dropdown target;
    public Button blocker;
    
    void OnEnable()
    {
        Events.LoadInstanceDropdown += DropdownUpdate;
    }
	public void DropdownUpdate(Dropdown _target){
		target = _target;
		dropdown.value = target.value;
		dropdown.options = target.options;
		dropdown.gameObject.SetActive (true);
        dropdown.RefreshShownValue();
		dropdown.Show ();
		dropdown.onValueChanged.AddListener (OnValueChanged);
        
        StartCoroutine(FindBlocker());
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
        dropdown.onValueChanged.RemoveAllListeners();
        Destroy(GameObject.Find("Dropdown List"));
		gameObject.SetActive (false);
        EFE_Base.instance.CloseCurrentOverlayPanel();
	}

    IEnumerator FindBlocker()
    {
        yield return new WaitForSeconds(0.2f);
        blocker = GameObject.Find("Blocker").GetComponent<Button>();
        blocker.onClick.AddListener(OnSelect);
    }
}
