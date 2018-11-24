using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatisTitleBoard : MonoBehaviour {
	public Text_Extend text_ex;
	public void SetData(string data){
		text_ex.text = data;
		text_ex.OriginText = data;
	}
}
