using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using VoxelBusters.Utility;
using VoxelBusters.Utility.UnityGUI.MENU;
using VoxelBusters.NativePlugins;
using Service.ClassReference;
public class IntentRecommend : AbstractIntent {
   
    public GameObject attachFileObject;
    public Text attachFilename;
    public Button b_image, b_sendRecommend;
    public Dropdown d_category;
    public InputField input_recommend;
    public Texture2D mockupTexture;

    byte[] imageByteArray;

    void OnDisable()
    {
        attachFileObject.SetActive(false);
        imageByteArray = null;
		Events.OnLoadRecommend -= Events_OnLoadRecommend;
    }
    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_image.onClick.AddListener(PickImage);
        b_sendRecommend.onClick.AddListener(SendRecommend);
    }

    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
        //AddOptionDataDropDown();
        Events.instance.PageReady_Dispatch();
		Events.OnLoadRecommend += Events_OnLoadRecommend;
		StartCoroutine (DelayUpdateIntent ());
    }
		
    
    void AddOptionDataDropDown()
    {

        Dropdown.OptionData optionData;
        d_category.ClearOptions();
        optionData = new Dropdown.OptionData();
        optionData.text = "เลือกข้อความแนะนำหรือแจ้งข้อมูล";
        d_category.options.Add(optionData);
        foreach (RecommendTitle p in DataManager.instance.recommends)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = p.com_name;
            d_category.options.Add(optionData);
        }

        d_category.RefreshShownValue();
    }
    void SendRecommend()
    {
        if (DataManager.instance.image_recommend_byteArray == null || DataManager.instance.image_recommend_byteArray.Length <= 0)
        {
            PopupManager.instance.OpenAlert("กรุณาบันทึกรูป");
            return;
        }

        if (string.IsNullOrEmpty(input_recommend.text))
        {
            PopupManager.instance.OpenAlert("กรุณาพิมพ์ข้อความแนะนำ");
            return;
        }
        if (d_category.value == 0)
        {
            PopupManager.instance.OpenAlert("กรุณาเลือกข้อความแนะนำหรือแจ้งข้อมูล");
            return;
        }
		ServiceRequest.instance.SendRecommendRequest(DataManager.instance.GetMember().member_id,
												       DataManager.instance.recommends[d_category.value - 1].com_id.ToString(),
														input_recommend.text, DataManager.instance.image_recommend_byteArray,
                                                       DataManager.instance.fileRecommendImage);
    }
    void PickImage()
    {
        #if UNITY_EDITOR
                if (imageByteArray == null)
                {
					//mockupTexture.Resize(Mathf.FloorToInt(mockupTexture.width*0.2f),Mathf.FloorToInt(mockupTexture.height*0.2f));
                    imageByteArray = mockupTexture.EncodeToPNG();
                    DataManager.instance.image_recommend_byteArray = imageByteArray;
                    DataManager.instance.fileRecommendImage = mockupTexture.name + "Recommend.jpg";
                    attachFilename.text = DataManager.instance.fileRecommendImage;
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
            imageByteArray = Utils.Texture2dToByteArray(_image);
            DataManager.instance.image_recommend_byteArray = imageByteArray;
            DataManager.instance.fileRecommendImage = _image.name + "Recommend.jpg";
            attachFilename.text = DataManager.instance.fileRecommendImage;
            attachFileObject.SetActive(true);
        }
        StartCoroutine(DelayPickImageFinished());
		
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
	IEnumerator DelayUpdateIntent(){
		yield return new WaitForSeconds (0.1f);
		if (DataManager.instance.recommends == null || DataManager.instance.recommends.Count <= 0) {
			ServiceRequest.instance.GetAllRecommend ();
		} else {
			AddOptionDataDropDown ();
		}
	}


	void Events_OnLoadRecommend (List<RecommendTitle> profile)
	{
		AddOptionDataDropDown ();
	}
}
