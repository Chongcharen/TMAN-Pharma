using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class IntentManager : MonoBehaviour {
    public static IntentManager instance;
    private List<int> historyPage;
    public int currentIndex = 0;
    public int previousIndex = 0;
    public int nextIndex = 0;
    public List<AbstractIntent> listPage;

    private AbstractIntent currentIntent, previousIntent, nextIntent;
  
}
public enum Intent{
    splashscreen = 0 ,Login = 1,Register = 2 ,ForgotPassword = 3, Menu = 4 , Inputaddress = 5,InputAddressByMap = 6 ,Checkin = 7 ,
    FindPlace =8 , FindPlaceResult = 9 , FindDirection = 10,Promotion = 11 , PromotionDetail = 12 , PDFProduct = 13,
	Profile = 14 , SatisFaction = 15 , Recommend = 16 , LuckyDraw = 17 , LuckyDrawSendData = 18 , FindShopResult = 19,PlaceAreaCheckin = 20
}
public enum POPUP
{
    LOAD = 0 , ALERT = 1 ,CHOICE = 2
}