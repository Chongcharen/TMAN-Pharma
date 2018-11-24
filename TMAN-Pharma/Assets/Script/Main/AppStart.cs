using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Service.ClassReference;
using System.Reflection;
using System;
using System.Globalization;
using OneSignalPush;
public class AppStart : MonoBehaviour {
    void Awake()
    {
		Application.targetFrameRate = 60;

        
       	Input.location.Start(1,1);
    }
    void Start()
    {
		VariableManager.GetInstance.screenCanvas = GameObject.Find("EFE_Canvas").GetComponent<RectTransform>().rect;
        InitOneSignal();
        StartCoroutine(DelayCall());
       // ServiceRequest.instance.GetMemberProfile(1);
		//ServiceRequest.instance.GetAllHeaderPDF ();
       // ServiceRequest.instance.GetNEWSRequest();
        //ServiceRequest.instance.GetPromotion(1);
       // ServiceRequest.instance.SearchPlaceByID(2);
        //ServiceRequest.instance.GetRewardPointRequest(1);
        // Debug.Log(OnlineMapsLocationService.instance.position);
        //ServiceRequest.instance.GetCheckinByID();
        //ServiceRequest.instance.GetAllGroupSale();
        // ServiceRequest.instance.GetPlaceRequest("ST0211223");
        //ServiceRequest.instance.LoginRequest("test", "12345678");
        // ServiceRequest.instance.RegisterRequest();
        //ServiceRequest.instance.ForgorPasswordRequest("aaa@aaa.aaa");
        //ServiceRequest.instance.GetNEWSRequest("1");
        //ServiceRequest.instance.GetStoreNEWSRequest();
        //ServiceRequest.instance.GetAllPromotionRequest();
        //ServiceRequest.instance.GetPromotionBYID("3");
        //ServiceRequest.instance.GetAllProvince();
        //ServiceRequest.instance.GetAllStore();
        // ServiceRequest.instance.UpdateMemberProfile("1", "Thiti Miangmook _test_edit", "11111", "22222", "333333", "tester@gmail.com", "444444",
        //                                          "5","Address");
    }
    IEnumerator DelayCall()
    {
        yield return new WaitForSeconds(1);
		//PlayerPrefs.DeleteAll ();
		CacheManager.Instance.LoadCache ();
        DataManager.instance.GetCache();


    }

	void Update(){
		#if UNITY_ANDROID
			if (Input.GetKeyDown (KeyCode.Escape)) {
				EFE_Base.instance.OpenPreviousIntent();
			}
		#endif
	}
    void InitOneSignal()
    {
        // Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
        // OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);
        // OneSignal.SetSubscription(true);
        /* OneSignal.StartInit("479d4c2b-9bcb-4567-9f2e-21f75100d78a", "639295472664")
           .HandleNotificationOpened(HandleNotificationOpened)
           .HandleNotificationReceived(HandleNotificationReceived)
           .InFocusDisplaying(OneSignal.OSInFocusDisplayOption.Notification)
           .Settings(new Dictionary<string, bool>() {
             { OneSignal.kOSSettingsAutoPrompt, true },
             { OneSignal.kOSSettingsInAppLaunchURL, true } })
           .EndInit();
           OneSignal.ClearOneSignalNotifications();
           OneSignal.RegisterForPushNotifications();
           OneSignal.SetSubscription(true);*/
		
        OneSignal.StartInit("479d4c2b-9bcb-4567-9f2e-21f75100d78a", "639295472664")
          .HandleNotificationOpened(HandleNotificationOpened)
          .HandleNotificationReceived(HandleNotificationReceived)
          .EndInit();


        // Sync hashed email if you have a login system or collect it.
        //   Will be used to reach the user at the most optimal time of day.
        // OneSignal.syncHashedEmail(userEmail);
    }
    private static void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
    }
    void HandleNotificationReceived(OSNotification result)
    {

    }
}
