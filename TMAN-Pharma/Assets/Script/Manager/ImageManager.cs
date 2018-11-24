using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ImageManager : MonoBehaviour {

	public static ImageManager instance;

	Dictionary<string,Texture2D> imageDic;
	Texture2D imageTexture;
	void Awake(){
		instance = this;
		imageDic = new Dictionary<string, Texture2D> ();
	}

	public bool IsImage(string link){
		return imageDic.ContainsKey (link);
	}
	public Texture2D GetImage(string link)
	{
		return imageDic [link];
	}
	public void AddImageLink(string link , Texture2D texture){
		imageDic.Add (link, texture);
	}
}
