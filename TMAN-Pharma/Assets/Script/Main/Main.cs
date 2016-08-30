using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("aaaa");
        Debug.Log(IntentManager.GetInstance());
        // GameObject.Find("Canvas").GetComponent<Canvas>().
        Debug.Log(GameObject.Find("Canvas").GetComponent<RectTransform>().rect);
      
	}
}
