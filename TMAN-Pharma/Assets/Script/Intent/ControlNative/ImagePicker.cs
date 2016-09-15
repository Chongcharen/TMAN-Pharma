using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using VoxelBusters.Utility;
using VoxelBusters.Utility.UnityGUI.MENU;
using VoxelBusters.NativePlugins;

public class ImagePicker : MonoBehaviour {

    public Button b_imgPicker;
    void Start()
    {
        b_imgPicker.onClick.AddListener(CallImg);
    }
    void CallImg()
    {
        if (IsCameraSupported()) {
            PickImageFromBoth();
           }
    }
    private bool IsCameraSupported()
    {
        return NPBinding.MediaLibrary.IsCameraSupported();
    }
    private void PickImageFromAlbum()
    {
        // Set popover to last touch position
        NPBinding.UI.SetPopoverPointAtLastTouchPosition();

        // Pick image
        NPBinding.MediaLibrary.PickImage(eImageSource.ALBUM, 1.0f, PickImageFinished);
    }

    private void PickImageFromCamera()
    {
        // Set popover to last touch position
        NPBinding.UI.SetPopoverPointAtLastTouchPosition();

        // Pick image
        NPBinding.MediaLibrary.PickImage(eImageSource.CAMERA, 1.0f, PickImageFinished);
    }

    private void PickImageFromBoth()
    {
        // Set popover to last touch position
        NPBinding.UI.SetPopoverPointAtLastTouchPosition();

        // Pick image
        NPBinding.MediaLibrary.PickImage(eImageSource.BOTH, 1.0f, PickImageFinished);
    }
    private void PickImageFinished(ePickImageFinishReason _reason, Texture2D _image)
    {
        if(_image != null)
        {
            byte[] byteArray = Utils.Texture2dToByteArray(_image);
        }
    }
		
}
