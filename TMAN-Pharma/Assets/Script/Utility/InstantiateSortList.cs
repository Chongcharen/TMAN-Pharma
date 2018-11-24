using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Service.ClassReference;
public class InstantiateSortList : MonoBehaviour {
	public static InstantiateSortList instance;
	void Awake(){
		instance = this;
	}

}
