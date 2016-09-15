using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VoxelBusters.Utility;
using VoxelBusters.Utility.UnityGUI.MENU;
using VoxelBusters.NativePlugins;
using Service.ClassReference;
public class IntentInputAddress : AbstractIntent {
    [SerializeField]
    Button b_imgPicker,b_send;
    [SerializeField]
    InputField shopName_input,store_code_input,tel_input,address_input;
    [SerializeField]
    Dropdown dropdown_shop, dropdown_arena;
    byte[] byteArray;
    Place placeData;
    void Start()
    {
        b_imgPicker.onClick.AddListener(PickImage);
        b_send.onClick.AddListener(SendDataToService);
        //store_code.
        store_code_input.onEndEdit.AddListener(OnEditStoreCode);
    }
    void OnEnable()
    {
        Events.LoadPlaceComplete += Events_LoadPlaceComplete;
    }
    void OnDisable()
    {
        Events.LoadPlaceComplete -= Events_LoadPlaceComplete;
    }

    private void Events_LoadPlaceComplete(Place place)
    {
        placeData = place;
        Debug.Log(placeData.code_store);
        shopName_input.text = place.place_name;
        tel_input.text = place.place_tel;
        address_input.text = place.place_address;

    }
    private void OnEditStoreCode(string store_code)
    {
        ServiceRequest.instance.GetPlaceRequest(store_code);
    }


    void PickImage()
    {
        if (NPBinding.MediaLibrary.IsCameraSupported())
        {
            PickImageFromBoth();
        }
    }
    private void PickImageFromBoth()
    {
        NPBinding.UI.SetPopoverPointAtLastTouchPosition();
        NPBinding.MediaLibrary.PickImage(eImageSource.BOTH, 1.0f, PickImageFinished);
    }
    private void PickImageFinished(ePickImageFinishReason _reason, Texture2D _image)
    {
        
        if (_image != null)
        {
            byteArray = Utils.Texture2dToByteArray(_image);
        }
    }
    private void SendDataToService()
    {
        if (byteArray == null) return;
        ServiceRequest.instance.SavePlaceRequest(placeData.place_id, 1, 1, byteArray, placeData.store_id + ".jpg");
    }
}
