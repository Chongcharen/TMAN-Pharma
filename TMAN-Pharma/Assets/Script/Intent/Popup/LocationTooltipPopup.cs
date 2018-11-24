using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Service.ClassReference;
public class LocationTooltipPopup : MonoBehaviour {

    [SerializeField]
    Button b_save;
    [SerializeField]
    Text address_txt;
    Place target;
    // Use this for initialization
    void Start() {
        b_save.onClick.AddListener(OnSaveButton);
    }
    void OnEnable()
    {
        target = DataManager.instance.getPlace();
        address_txt.text = target.place_address;
    }

    void OnSaveButton()
    {
        
        if (target != null && DataManager.instance.currentPosition != null)
		{
			ServiceRequest.instance.SavePlaceRequest(target.place_id,
													 System.Math.Round(DataManager.instance.currentPosition.y,6),
													 System.Math.Round(DataManager.instance.currentPosition.x,6),DataManager.instance.image_byteArray,
                                                     DataManager.instance.fileImage);
        }
    }
}
