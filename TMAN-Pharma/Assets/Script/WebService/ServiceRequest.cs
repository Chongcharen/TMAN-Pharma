﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using Newtonsoft.Json;
using LitJson;
public class ServiceRequest : MonoBehaviour {

	// Use this for initialization
	public static ServiceRequest instance;
	private ServiceResponse response;
    private object success;
    private object description;
    private object json;

    void Awake(){
		instance = this;
	}
	void Start(){
		LitJson.JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));
		LitJson.JsonMapper.RegisterImporter<int, long>(input => Convert.ToInt64(input));
		LitJson.JsonMapper.RegisterImporter<string, decimal>(input => Convert.ToDecimal(input));
		LitJson.JsonMapper.RegisterImporter<string, int>(input => Convert.ToInt32(input));
		LitJson.JsonMapper.RegisterImporter<string, float>(input => Convert.ToSingle(input));
	}

    public void LoginRequest(string username ,string password)
    {
        StartCoroutine(TestResponse());
    }


    IEnumerator TestResponse()
    {
        /* WWW www = new WWW("http://codebee.co.th/project/tman/service/login");
         yield return StartCoroutine(new WWWRequest(www));
         if (www.isDone)
         {
             ServiceResponse response = JsonHelper.FromSingleJson<ServiceResponse>(www.text);
             Debug.Log(www.text);
         }*/
        /* WWWForm form = new WWWForm();
         form.AddField("username", "admin");
         form.AddField("password", "qwerty");
         WWW www = new WWW("http://codebee.co.th/project/tman/service/login",form);
         yield return StartCoroutine(new WWWRequest(www));
         if (www.isDone)
         {
             Debug.Log(www.text);
             //ServiceResponse response = JsonUtility.FromJson<ServiceResponse>(www.text);//JsonHelper.FromSingleJson<ServiceResponse>(www.text);
         }*/



        WWWForm form = new WWWForm();
        //form.AddField("email", "chongcharen@hotmail.com");
       // WWW www = new WWW("http://thewalklifestylemall.com/service/forgotPassword");
       // form.AddField("username", "admin");
       // form.AddField("password", "qwerty");
        WWW www = new WWW("http://codebee.co.th/project/tman/service/forgotPassword");
        yield return StartCoroutine(new WWWRequest(www));
        if (www.isDone)
        {
            
            string old = " { 'success':false,'description':'\u0e44\u0e21\u0e48\u0e1e\u0e1a email \u0e19\u0e35\u0e49'}";
            string newstring = " { 'success':false,'description:'\u0e44\u0e21\u0e48\u0e1e\u0e1a\u0e2a\u0e21\u0e32\u0e0a\u0e34\u0e01\u0e19\u0e35\u0e49'}";
            //string oldString = "/*{ "success":false,"description":"\u0e44\u0e21\u0e48\u0e1e\u0e1a email \u0e19\u0e35\u0e49"}*/;
            //﻿{"success":false,"description":"\u0e44\u0e21\u0e48\u0e1e\u0e1a\u0e2a\u0e21\u0e32\u0e0a\u0e34\u0e01\u0e19\u0e35\u0e49"}
            //{"success":false,"description":"\u0e44\u0e21\u0e48\u0e1e\u0e1a email \u0e19\u0e35\u0e49"}

            Debug.Log(www.text);
            ServiceResponse data = JsonMapper.ToObject<ServiceResponse>(www.text);
            Debug.Log(data.description);

/*

            string convertString = System.Convert.ToString(www.text);
            Debug.Log(convertString);
           // Debug.Log(www.text);
            ServiceResponse response = JsonHelper.FromSingleJson<ServiceResponse>(convertString);
            Debug.Log(response.description);
            */
        }


    }
}
[System.Serializable]
public class ServiceResponse{
	public bool success;
	public string description;
}