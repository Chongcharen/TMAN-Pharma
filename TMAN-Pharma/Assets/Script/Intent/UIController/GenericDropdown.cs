using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GenericDropdown : Dropdown {

	public override void OnSelect (UnityEngine.EventSystems.BaseEventData eventData)
	{
		base.OnSelect (eventData);
		onValueChanged.RemoveAllListeners ();
		Destroy(GameObject.Find("Dropdown List"));
		gameObject.SetActive (false);
		EFE_Base.instance.CloseCurrentOverlayPanel();
	}
}
