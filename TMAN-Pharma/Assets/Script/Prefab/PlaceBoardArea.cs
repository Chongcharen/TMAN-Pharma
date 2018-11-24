using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Service.ClassReference;
using OneP.InfinityScrollView;

public class PlaceBoardArea : InfinityBaseItem {
	public Text shop_name_txt, shop_address_txt;
	PlaceFilter placeFilter;

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	public override void Reload (InfinityScrollView infinity, int _index)
	{
		base.Reload (infinity, _index);
		placeFilter = DataManager.instance.placeFilterArea [_index];
		shop_name_txt.text = placeFilter.place_name;
		shop_address_txt.text = placeFilter.place_address;
		transform.localPosition = new Vector2(0,transform.localPosition.y);
	}
		
	void OnClick()
	{
		AcceptCallback callback = new AcceptCallback();
		callback.action = delegate { 
			Events.instance.SearchPlace_Dispatch(placeFilter);
		};
		PopupManager.instance.OpenAlert("คุณต้องการยืนยันการ Check In สถานที่?", callback, POPUP.CHOICE);
		//DataManager.instance.placeSelect = placeFilter;
		//EFE_Base.instance.OpenPanelByIndex(Intent.FindDirection);
	}
}
