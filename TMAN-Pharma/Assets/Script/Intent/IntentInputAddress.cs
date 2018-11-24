using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VoxelBusters.Utility;
using VoxelBusters.Utility.UnityGUI.MENU;
using VoxelBusters.NativePlugins;
using Service.ClassReference;
public class IntentInputAddress : AbstractIntent {
    
    public GameObject attachFileObject;
    
    public Text attachFilename,detail_txt;
    
    public Texture2D exTexture;
    
    public Button b_imgPicker,b_send;
    
    public InputField shopName_input,store_code_input,tel_input,address_input;
    
    public Dropdown dropdown_store, dropdown_groupSale;
    byte[] byteArray;
    Place placeData;

	public List<GameObject> disableObject;
    void Start()
    {
        b_imgPicker.onClick.AddListener(PickImage);
        b_send.onClick.AddListener(SendDataToService);
        //store_code.
        store_code_input.onEndEdit.AddListener(OnEditStoreCode);
        AddButtonListeners();
    }
    void OnEnable()
    {
        Events.LoadPlaceComplete += Events_LoadPlaceComplete;
        UpdatePage();
    }
    void OnDisable()
    {
        Events.LoadPlaceComplete -= Events_LoadPlaceComplete;
        attachFileObject.SetActive(false);
        byteArray = null;
    }
	public override void OnBack ()
	{
		base.OnBack ();
		DataManager.instance.branchSelect = null;
		store_code_input.text = "";
	}
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
        AddOptionDataDropDown();
		if (DataManager.instance.branchSelect != null) {
			foreach (GameObject go in disableObject) {
				go.SetActive (true);
			}
			detail_txt.gameObject.SetActive (false);
			placeData = DataManager.instance.branchSelect;
			shopName_input.text = DataManager.instance.branchSelect.place_name;
			tel_input.text = DataManager.instance.branchSelect.place_tel;
			address_input.text = DataManager.instance.branchSelect.place_address;
			DataManager.instance.SetPlace(DataManager.instance.branchSelect);
		} else {
			foreach (GameObject go in disableObject) {
				go.SetActive (false);
			}
			detail_txt.gameObject.SetActive (true);
		}
        Events.instance.PageReady_Dispatch();
    }
    private void Events_LoadPlaceComplete(Place place)
    {
		foreach (GameObject go in disableObject) {
			go.SetActive (true);
		}
		detail_txt.gameObject.SetActive (false);
        placeData = place;
        shopName_input.text = place.place_name;
        tel_input.text = place.place_tel;
        address_input.text = place.place_address;
        DataManager.instance.SetPlace(place);
        

    }
    void AddOptionDataDropDown()
    {
        List<GroupSale> groupSales = DataManager.instance.groupSale;
        List<Store> stores = DataManager.instance.store;
        Dropdown.OptionData optionData;
        dropdown_groupSale.options.Clear();
        dropdown_store.options.Clear();
        foreach(GroupSale g in groupSales)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = g.group_name;
           
            dropdown_groupSale.options.Add(optionData);
        }
        foreach(Store s in stores)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = s.store_name;
            dropdown_store.options.Add(optionData);
        }
        dropdown_groupSale.RefreshShownValue();
        dropdown_store.RefreshShownValue();
    }
    private void OnEditStoreCode(string store_code)
    {
		if (string.IsNullOrEmpty (store_code)) {
			return;
		}
        ServiceRequest.instance.GetPlaceRequest(store_code,DataManager.instance.memberProfile.group_id);
    }


    void PickImage()
    {
		
        #if UNITY_EDITOR
        if (byteArray == null)
             {
					exTexture.Resize(Mathf.FloorToInt(exTexture.width*0.5f),Mathf.FloorToInt(exTexture.height*0.5f));
					byteArray = exTexture.EncodeToJPG();
                    DataManager.instance.image_byteArray = byteArray;
                    DataManager.instance.fileImage = exTexture.name + ".jpg";
                    attachFilename.text = DataManager.instance.fileImage;
                    attachFileObject.SetActive(true);
        	}
        #endif
        if (NPBinding.MediaLibrary.IsCameraSupported())
        {
            PopupManager.instance.OpenLoading();
            StartCoroutine(DelayOpenImage());
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
			//_image.Resize(Mathf.FloorToInt(_image.width*0.2f),Mathf.FloorToInt(_image.height*0.2f));
			TextureScale.Bilinear(_image,Mathf.FloorToInt(_image.width*0.2f),Mathf.FloorToInt(_image.height*0.2f));
            byteArray = Utils.Texture2dToByteArray(_image);
            DataManager.instance.image_byteArray = byteArray;
            DataManager.instance.fileImage = _image.name + "Address.jpg";
            attachFilename.text = DataManager.instance.fileImage;
            attachFileObject.SetActive(true);
        }
        StartCoroutine(DelayPickImageFinished());
    }
    private void SendDataToService()
    {
		#if UNITY_EDITOR
			EFE_Base.instance.OpenPanelByIndex(Intent.InputAddressByMap);
		#else
	        if (DataManager.instance.image_byteArray == null)
	        {
	            PopupManager.instance.OpenAlert("กรุณาแนบรูปภาพ");
	            return;
	        }

	        if (placeData != null & DataManager.instance.image_byteArray != null)
	        {
	            EFE_Base.instance.OpenPanelByIndex(Intent.InputAddressByMap);
	        }
	        else
	        {
	            PopupManager.instance.OpenAlert("กรุณาระบุข้อมูลสถานที่");
	        }
		#endif
    }
    IEnumerator DelayOpenImage()
    {
        yield return new WaitForSeconds(1);
        PickImageFromBoth();
    }
    IEnumerator DelayPickImageFinished()
    {
        yield return new WaitForSeconds(1);
        PopupManager.instance.ClosePopup();
    }
}
