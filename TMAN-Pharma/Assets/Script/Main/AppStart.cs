using UnityEngine;
using System.Collections;

public class AppStart : MonoBehaviour {
    void Awake()
    {
        VariableManager.GetInstance.screenCanvas = GameObject.Find("EFE_Canvas").GetComponent<RectTransform>().rect;
    }
    void Start()
    {
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
}
