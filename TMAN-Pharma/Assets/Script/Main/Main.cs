using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    public GameObject FirstLogin;
	// Use this for initialization
	void Start () {
		VariableManager.GetInstance.canDispatchListener = true;
        //FirstLogin.SetActive(true);
	}
}
