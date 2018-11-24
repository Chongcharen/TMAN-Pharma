using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI.Extensions;
public class ImageIndicator : MonoBehaviour {
    [SerializeField]
    RawImage rawimage;
	RectTransform rect;
    public void SetImage(string path)
    {
		if (ImageManager.instance.IsImage (path) == true) {
			rawimage.texture = ImageManager.instance.GetImage (path);
			rawimage.SetNativeSize ();
			rect = rawimage.GetComponent<RectTransform> ();
			rect.anchorMin = new Vector2(0, 0);
			rect.anchorMax = new Vector2(1, 1);
			rect.offsetMin = Vector2.zero;
			rect.offsetMax = Vector2.zero;
		} else {
			/*StartCoroutine (StaticCoroutine.LoadImage (path, texture => {
				rawimage.texture = texture;
				rawimage.SetNativeSize ();
				ImageManager.instance.AddImageLink(path,texture);
				rect = rawimage.GetComponent<RectTransform> ();
				rect.anchorMin = new Vector2(0, 0);
				rect.anchorMax = new Vector2(1, 1);
				rect.offsetMin = Vector2.zero;
				rect.offsetMax = Vector2.zero;
			}));*/

			StartCoroutine (LoadImage (path));
		}
    }

	IEnumerator LoadImage(string path){
		WWW www = new WWW (path);
		yield return StartCoroutine(new WWWRequest(www));
		rawimage.texture = www.texture;
		rawimage.SetNativeSize ();
		ImageManager.instance.AddImageLink(path,www.texture);
		rect = rawimage.GetComponent<RectTransform> ();
		rect.anchorMin = new Vector2(0, 0);
		rect.anchorMax = new Vector2(1, 1);
		rect.offsetMin = Vector2.zero;
		rect.offsetMax = Vector2.zero;
	}
}
