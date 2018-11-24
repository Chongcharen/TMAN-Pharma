using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Service.ClassReference;
public class BranchShopBoard : MonoBehaviour {

	public Text shop_name_txt, shop_address_txt;
	Place branchPlace;

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	public void UpdateData(Place result)
	{
		branchPlace = result;
		shop_name_txt.text = branchPlace.place_name;
		shop_address_txt.text = branchPlace.place_address;
	}
	void OnClick()
	{
		//MapManager.instance.OpenMapDirection();
		//DataManager.instance.placeSelect = branchPlace;
		DataManager.instance.branchSelect = branchPlace;
		EFE_Base.instance.OpenPanelByIndex(Intent.Inputaddress);
	}
}
