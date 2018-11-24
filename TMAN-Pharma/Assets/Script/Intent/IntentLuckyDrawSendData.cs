using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VoxelBusters.Utility;
using VoxelBusters.Utility.UnityGUI.MENU;
using VoxelBusters.NativePlugins;
using Service.ClassReference;
public class IntentLuckyDrawSendData : AbstractIntent {
    public GameObject attachFileObject;
    public Text attachFilename;
    
    public InputField input_name, input_store_code, input_tel, input_message;
    
    public Button b_camera, b_send;
    
    public Texture2D mockupTexture;
    byte[] imageByteArray;
    string filename;

    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_camera.onClick.AddListener(PickImage);
        b_send.onClick.AddListener(SendLuckyDraw);
    }
    void SendLuckyDraw()
    {
        if (imageByteArray == null)
        {
            PopupManager.instance.OpenAlert("กรุณาแนบรูปภาพ");
        }
        if (string.IsNullOrEmpty(input_name.text))
        {
            PopupManager.instance.OpenAlert("กรุณากรอกชื่อร้าน/ชื่อนามสกุล");
            return;
        }
        if (string.IsNullOrEmpty(input_tel.text))
        {
            PopupManager.instance.OpenAlert("กรุณากรอกเบอร์โทรศัพท์");
            return;
        }
        if (imageByteArray == null || imageByteArray.Length <= 0)
        {
            PopupManager.instance.OpenAlert("กรุณาแนบรูปภาพรหัส luckyDraw");
            return;
        }
        ServiceRequest.instance.GetReceiveAward(DataManager.instance.GetMember().member_id, input_name.text, input_store_code.text,
                                                   input_tel.text, input_message.text, imageByteArray, filename);
    }

    void PickImage()
    {

        #if UNITY_EDITOR
                if (imageByteArray == null)
                {
			TextureScale.Bilinear(mockupTexture,Mathf.FloorToInt(mockupTexture.width*0.2f),Mathf.FloorToInt(mockupTexture.height*0.2f));
			imageByteArray = mockupTexture.EncodeToJPG();
                    filename = mockupTexture.name + ".jpg";
                    attachFilename.text = filename;
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
            filename = _image.name + "Luckydraw.jpg";
            attachFilename.text = filename;
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

}
