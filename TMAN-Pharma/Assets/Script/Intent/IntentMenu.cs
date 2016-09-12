using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class IntentMenu : AbstractIntent{
    [SerializeField]
    List<Button> customerMenu;
    [SerializeField]
    List<Button> sellerMenu;
    [SerializeField]
    Button b_order, b_product, b_profile, b_relations, b_comment, b_contentment, b_luckydraw, b_promotion, b_directions, b_inputaddress, b_checkin;
    void OpenInputAddressIntent()
    {
        IntentManager.instance.SetIntent(Intent.Inputaddress);
    }




    public override void UpdatePage()
    {
        if (DataManager.instance.GetMember() != null)
        {
            EnableButtonMember(DataManager.instance.GetMemberType());
        }
        Events.instance.PageReady_Dispatch();
    }
    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_inputaddress.onClick.AddListener(OpenInputAddressIntent);
    }

    void EnableButtonMember(int member_type)
    {
        if(member_type == 0)
        {
            foreach(Button b in sellerMenu)
            {
                b.gameObject.SetActive(true);
            }
            foreach(Button b in customerMenu)
            {
                b.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Button b in sellerMenu)
            {
                b.gameObject.SetActive(false);
            }
            foreach (Button b in customerMenu)
            {
                b.gameObject.SetActive(true);
            }
        }
    }
}
