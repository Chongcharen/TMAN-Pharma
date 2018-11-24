using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Service.ClassReference;
using OneP.InfinityScrollView;
public class PlaceBoard : InfinityBaseItem {
    public Text shop_name_txt, shop_address_txt;
    PlaceFilter placeFilter;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
	public override void Reload (InfinityScrollView infinity, int _index)
	{
		base.Reload (infinity, _index);
		placeFilter = DataManager.instance.placeFilter [_index];
		shop_name_txt.text = placeFilter.place_name;
		shop_address_txt.text = placeFilter.place_address;
		transform.localPosition = new Vector2(0,transform.localPosition.y);
	}
    public void UpdateData(PlaceFilter result)
    {
        //placeFilter = result;
      //  shop_name_txt.text = placeFilter.place_name;
        //shop_address_txt.text = placeFilter.place_address;
    }
    void OnClick()
    {
        MapManager.instance.OpenMapDirection();
        DataManager.instance.placeSelect = placeFilter;
        EFE_Base.instance.OpenPanelByIndex(Intent.FindDirection);
    }
}
