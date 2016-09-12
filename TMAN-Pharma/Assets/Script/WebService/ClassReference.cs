using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Service.ClassReference
{
    [Serializable]
    public class ServiceResponse
    {
        public bool success;
        public string description;
    }
    [Serializable]
    public class RawMember
    {
        public string member_id;
        public string device_id;
        public string username;
        public string sername;
        public int member_type;
        public string store_id;
        public string store_name;
        public string code_store;
        public string code_offset;
        public string member_point;
        public string member_code;
        public string member_status;
        public bool success;
    }

    // forgotpassword ดึงเวลาได้รับข้อมูลพาส
    [Serializable]
    public class PasswrodGetter
    {
        public string password;
        public string sername;
        public string description;
    }
     [Serializable]
     public class MemberNEWS
    {
        public string news_id;
        public string news_name;
        public string news_content;
        public string news_image;
        public string news_status;
        public string store_id;
        public string member_type;
        public string timestamp;
        public List<string> image_list;
    }


    /*[{"promo_id":"2","promo_name":"Codebee","promo_content":"Hello wrold","promo_image":"4,5,2,3",
     * "member_type":"0","promo_status":"1","timestamp":"2016-09-06 11:18:47",
     * "image_list":["http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-22.png",
     * "http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-3_-_Copy2.png"]},
     * 
     * 
     * {"promo_id":"3","promo_name":"Codebee2","promo_content":"Codebee2","promo_image":"10","member_type":"0",
     * "promo_status":"1","timestamp":"2016-09-06 13:49:08",
     * "image_list":["http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-22.png",
     * "http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-3_-_Copy2.png","http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/1628371.png"]}]*/
    [Serializable]
    public class Promotion
    {
        public string promo_id;
        public string promo_name;
        public string promo_content;
        public string promo_image;
        public string member_type;
        public string promo_status;
        public string timestamp;
        public List<string> image_list;
    }

    [Serializable]
    public class Province
    {
        public string PROVINCE_ID;
        public string PROVINCE_CODE;
        public string PROVINCE_NAME;
        public string GEO_ID;
    }

    [Serializable]
    public class Store
    {
        public string store_id;
        public string store_name;
        public string timestamp;
    }


}
