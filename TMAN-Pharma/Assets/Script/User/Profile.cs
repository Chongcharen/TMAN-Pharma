using UnityEngine;
using System.Collections;


/*
 {"member_id":"1","device_id":"0","username":"admin","sername":"Thiti Miangmook _test_edit",
 "member_type":"0","store_id":null,"store_name":null,"code_store":null,"code_offset":null,"member_point":null,
 "member_code":"REP518724","member_status":"1","success":true}
     
     */
public class Profile : MonoBehaviour {
    public string username;
    public string sername;
    public string phone1, phone2, phone3;
    public string email;
    public string fax;
    public string province;
    public string address;
    public string member_id;
    public string member_type;
    public string device_id;
         
    public string point;

    public string GetMember()
    {
        return "";
    }
}
