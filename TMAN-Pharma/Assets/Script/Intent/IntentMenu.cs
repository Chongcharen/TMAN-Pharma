using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Service.ClassReference;
public class IntentMenu : AbstractIntent {
    
    public List<Button> customerMenu;
    
    public List<Button> sellerMenu;
    
    public Button b_order, b_product,b_customer_product, b_profile, b_news, b_recommend, b_satisFaction, b_luckydraw, b_promotion, b_directions, b_inputaddress, b_checkin;
  
    public List<GameObject> menuButton;
    public Transform content;

	void Start(){
		AddButtonListeners();
	}
		
    void OpenInputAddressIntent()
    {
        //IntentManager.instance.SetIntent(Intent.Inputaddress);
		if (LocationService_ex.instance.isEnableByUser ()) {
			EFE_Base.instance.OpenPanelByIndex (Intent.Inputaddress);
		}
    }
    void OpenCheckinIntent()
    {
		if (!LocationService_ex.instance.isEnableByUser ())
			return;
        AcceptCallback callback = new AcceptCallback();
        callback.action = delegate { 
			//PopupManager.instance.ClosePopup();
			//PopupManager.instance.OpenLoading();
				LocationService_ex.instance.OpenCheckin();
		};
		LocationService_ex.instance.OpenCheckin();
       // PopupManager.instance.OpenAlert("คุณต้องการยืนยันการ Check In สถานที่?", callback, POPUP.CHOICE);
    }
    void OpenFindPlace()
    {
		/*if (LocationService_ex.instance.isEnableByUser ()) {
			EFE_Base.instance.OpenPanelByIndex (Intent.FindPlace);
		}*/
		EFE_Base.instance.OpenPanelByIndex (Intent.FindPlace);
    }
    void OpenPromotion()
    {
        EFE_Base.instance.OpenPanelByIndex(Intent.Promotion);
    }
    void OpenNews()
    {
        EFE_Base.instance.OpenPanelByIndex(Intent.Promotion);
    }
    void OpenOrder()
    {
        ServiceRequest.instance.GetURLProduct();
    }
    void OpenProfile()
    {
        //ServiceRequest.instance.GetMemberProfile(DataManager.instance.GetMember().member_id);
		EFE_Base.instance.OpenPanelByIndex(Intent.Profile);
    }
    void OpenProduct()
    {
		EFE_Base.instance.OpenPanelByIndex(Intent.PDFProduct);
    }
    void OpenRecommend()
    {
        /*if (DataManager.instance.recommends == null || DataManager.instance.recommends.Count <= 10)
        {
            ServiceRequest.instance.GetAllRecommend();
        }
        else
        {
            EFE_Base.instance.OpenPanelByIndex(Intent.Recommend);
        }*/
		EFE_Base.instance.OpenPanelByIndex(Intent.Recommend);
    }
   void OpenLuckyDraw()
    {
		EFE_Base.instance.OpenPanelByIndex(Intent.LuckyDraw);
        //ServiceRequest.instance.GetCodeAward();
    }
    void OpenSatisFaction()
    {
       // ServiceRequest.instance.GetTitleSurvey();
		EFE_Base.instance.OpenPanelByIndex(Intent.SatisFaction);
    }

    public override void UpdatePage()
    {
		DataManager.instance.isFindDirection = false;
		DataManager.instance.isInputAddress = false;
        if (DataManager.instance.GetMember() != null)
        {
           EnableButtonMember(DataManager.instance.GetMemberType());
        }
        DataManager.instance.image_byteArray = null;
       	Events.instance.PageReady_Dispatch();
    }
    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_inputaddress.onClick.AddListener(OpenInputAddressIntent);
        b_checkin.onClick.AddListener(OpenCheckinIntent);
        b_directions.onClick.AddListener(OpenFindPlace);
        b_promotion.onClick.AddListener(OpenPromotion);
        b_order.onClick.AddListener(OpenOrder);
        b_product.onClick.AddListener(OpenProduct);
        b_profile.onClick.AddListener(OpenProfile);
        b_satisFaction.onClick.AddListener(OpenSatisFaction);
        b_recommend.onClick.AddListener(OpenRecommend);
        b_news.onClick.AddListener(OpenNews);
        b_luckydraw.onClick.AddListener(OpenLuckyDraw);
        b_customer_product.onClick.AddListener(OpenProduct);
    }

    void EnableButtonMember(int member_type)
    {
		foreach (GameObject g in menuButton) {
			g.gameObject.SetActive (false);
		}
		foreach (MenuService m in DataManager.instance.menuService) {
			menuButton [m.menu_id - 1].SetActive (true);
			menuButton [m.menu_id - 1].transform.SetSiblingIndex (m.menu_id - 1);
		}
    }





}
