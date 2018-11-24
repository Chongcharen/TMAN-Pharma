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
        public int member_id;
        public string device_id;
        public string username;
        public string sername;
        public int store_id;
		public int member_type;
		public string group_id;
        public string store_name;
        public string code_store;
        public string code_offset;
		public string member_point;
        public string member_code;
        public string member_status;
        public string member_tel1;
        public string member_tel2;
        public string member_tel3;
        public string member_fax;
        public string member_address;
        public string email;
		public string description; 
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

    /*
     [{"promo_id":"2","promo_name":"Codebee","promo_content":"Hello wrold","member_type":"0",
     "promo_status":"1","timestamp":"2016-09-06 11:18:47",
     "image_list":["http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-22.png",
     "http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-3_-_Copy2.png"]},
     {"promo_id":"3","promo_name":"Codebee2","promo_content":"Codebee2","member_type":"0",
     "promo_status":"1","timestamp":"2016-09-06 13:49:08",
     "image_list":["http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-22.png",
     "http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-3_-_Copy2.png",
     "http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/1628371.png"]}]
         
         */

    /*
     [{"news_id":"1","news_name":"T-mans",
     "news_content":"HELLO WORLD ","news_status":"1","store_id":"1",
     "member_type":"1","timestamp":"2016-09-06 12:09:30","image_list":["http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/aboutus-nav-23.png"]}]
         
         
         */





    [Serializable]
     public class MemberNEWS
    {
        public string news_id;
        public string news_name;
        public string news_content;
        public string news_image;
        public string news_status;
        public string store_id;
		public int member_type;
        public string timestamp;
        public List<string> image_list;
    }

    [Serializable]
    public class Promotion
    {
        public string promo_id;
        public string promo_name;
        public string promo_content;
		public int member_type;
        public string promo_status;
        public string timestamp;
        public List<string> image_list;
    }

    [Serializable]
    public class Province
    {
        public int PROVINCE_ID;
        public string PROVINCE_CODE;
        public string PROVINCE_NAME;
        public string GEO_ID;
    }

    [Serializable]
    public class Store
    {
        public int store_id;
        public string store_name;
        public string timestamp;
    }


	/*

	/*
	[{"place_id":"1",
	"place_name":"\u0e1a\u0e23\u0e34\u0e29\u0e31\u0e17 \u0e42\u0e04\u0e4a\u0e14\u0e1a\u0e35 \u0e08\u0e33\u0e01\u0e31\u0e14",
	"sername":"Thiti Miangmook","code_store":"ST12345","code_offset":"","place_tel":"0856057748",
		"place_address":"41\/94 \u0e15\u0e33\u0e1a\u0e25\u0e1a\u0e32\u0e07\u0e15\u0e25\u0e32\u0e14 \u0e2d\u0e33\u0e40\u0e20\u0e2d\u0e1b\u0e32\u0e01\u0e40\u0e01\u0e23\u0e47\u0e14 \u0e08\u0e31\u0e07\u0e2b\u0e27\u0e31\u0e14\u0e19\u0e19\u0e17\u0e1a\u0e38\u0e23\u0e35 11120",
		"store_id":"1",s
		"group_id":"1,4",
		"province_id":"3",
		"timestamp":"2016-09-06 15:59:36"},
		{"place_id":"23","place_name":"ST12345","sername":"ST12345","code_store":"ST12345","code_offset":"ST12345","place_tel":"ST12345","place_address":"ST12345","store_id":"1","group_id":"1","province_id":"3","timestamp":"2016-12-06 16:02:26"}]
	*/
    [Serializable]
    public class Place
    {
        public int place_id;
        public string place_name;
        public string sername;
		public string code_store;
		public string code_offset;
		public string place_tel;
		public string place_address;
		public int store_id;
		public string group_id;
        public int province_id;
        public string timestamp;

    }





    /*
     * [{"group_id":"1","group_name":"\u0e40\u0e02\u0e15 1","timestamp":"2016-08-31 13:29:28"},
     * {"group_id":"2","group_name":"\u0e40\u0e02\u0e15 2","timestamp":"2016-08-31 13:29:28"},
     * {"group_id":"3","group_name":"\u0e40\u0e02\u0e15 3","timestamp":"2016-08-31 13:29:37"},
     * {"group_id":"4","group_name":"\u0e40\u0e02\u0e15 4","timestamp":"2016-08-31 13:29:37"}]
     */

    [Serializable]
    public class GroupSale
    {
        public int group_id;
        public string group_name;
        public string timestamp;
    }

    /*
     [{"pos_id":"1","place_id":"3","pos_image":"","pos_latitude":"3.00000000",
     "pos_longitude":"3.00000000","timestamp":"2016-09-15 14:00:02"},
     {"pos_id":"18","place_id":"1","pos_image":"21","pos_latitude":"1.00000000",
     "pos_longitude":"1.00000000","timestamp":"2016-09-22 02:49:34"}]
     */

    [Serializable]
    public class LatLongPosition
    {
        public int pos_id;
		public string place_id;
        public string pos_image;
		public double pos_latitude;
		public double pos_longitude;
        public string timestamp;
		public float store_meter;
		public double distance;
    }

    /*
     {"0":{"member_id":"1","sername":"Thiti Miangmook _test_edit",
     "place_id":"1","place_name":"CodeBee",
     "timestamp":"2016-09-23 14:26:04","date":"23 \u0e01\u0e31\u0e19\u0e22\u0e32\u0e22\u0e19 2559",
     "time":"14:26 \u0e19."},"success":true}
     */

    [Serializable]
    public class PlaceCheckin
    {
        public int member_id;
        public int place_id;
        public string sername;
        public string place_name;
        public string date;
        public string time;
        public string timestamp;
		public bool success;
		public string description;
    }

    /*
     [{"place_id":"1","place_name":"CodeBee","place_address":"19\/5 \u0e2b\u0e21\u0e39\u0e484"},
     {"place_id":"2","place_name":"T-man","place_address":"12344ewweee"},
     {"place_id":"3","place_name":"\u0e40\u0e0a\u0e35\u0e22\u0e07\u0e43\u0e2b\u0e21\u0e48","place_address":"12313"}]
     */

	/*
	[{"place_id":"19","place_name":"\u0e2a\u0e27\u0e19\u0e1e\u0e25\u0e39\u0e04\u0e25\u0e34\u0e19\u0e34\u0e01\u0e40\u0e27\u0e0a\u0e01\u0e23\u0e23\u0e21 ",
		"place_address":"500 \u0e0b\u0e2d\u0e22 \u0e2a\u0e27\u0e19\u0e1e\u0e25\u0e39 \u0e0b\u0e2d\u0e22 \u0e2a\u0e27\u0e19\u0e1e\u0e25\u0e39 \u0e41\u0e02\u0e27\u0e07 \u0e17\u0e38\u0e48\u0e07\u0e21\u0e2b\u0e32\u0e40\u0e21\u0e06 \u0e40\u0e02\u0e15 \u0e2a\u0e32\u0e17\u0e23 \u0e01\u0e23\u0e38\u0e07\u0e40\u0e17\u0e1e\u0e21\u0e2b\u0e32\u0e19\u0e04\u0e23 10120",
		"place_tel":"02 990 3407","pos_latitude":"13.726105","pos_longitude":"100.538300"}]
	*/





	[Serializable]
    public class PlaceFilter
    {
		public string place_id;
        public string place_name;
        public string place_address;
		public string place_tel;
		public double pos_latitude;
		public double pos_longitude;
    }

    [Serializable]
    public class RewardPoint
    {
        public string exp_code;
        public bool success;
    }


	/*
		[{"menu_id":"1","menu_name":"CHECK IN","member_type":"0","menu_status":"1","menu_row":"1",
		"timestamp":"2016-08-31 13:24:40"},
		{"menu_id":"2","menu_name":"\u0e04\u0e49\u0e19\u0e2b\u0e32\u0e40\u0e2a\u0e49\u0e19\u0e17\u0e32\u0e2d\u0e07",
		"member_type":"0","menu_status":"1","menu_row":"2","timestamp":"2016-08-31 13:24:40"},
		{"menu_id":"3","menu_name":"\u0e25\u0e07\u0e02\u0e49\u0e2d\u0e21\u0e39\u0e25\u0e2a\u0e16\u0e32\u0e19\u0e17\u0e35\u0e48","member_type":"0",
		"menu_status":"1","menu_row":"3","timestamp":"2016-08-31 13:25:32"},
		{"menu_id":"4","menu_name":"\u0e42\u0e1b\u0e23\u0e42\u0e21\u0e0a\u0e31\u0e48\u0e19","member_type":"0","menu_status":"1",
		"menu_row":"4","timestamp":"2016-08-31 13:25:32"},
		{"menu_id":"5","menu_name":"\u0e2a\u0e34\u0e19\u0e04\u0e49\u0e32","member_type":"0","menu_status":"1",
		"menu_row":"5","timestamp":"2016-08-31 13:26:12"},
		{"menu_id":"6","menu_name":"\u0e02\u0e49\u0e2d\u0e21\u0e39\u0e25\u0e2a\u0e48\u0e27\u0e19\u0e15\u0e31\u0e27",
		"member_type":"0","menu_status":"1","menu_row":"6","timestamp":"2016-08-31 13:26:12"},
		{"menu_id":"7","menu_name":"\u0e2a\u0e31\u0e48\u0e07\u0e0b\u0e37\u0e49\u0e2d\u0e2a\u0e34\u0e19\u0e04\u0e49\u0e32",
		"member_type":"0","menu_status":"1","menu_row":"8","timestamp":"2016-08-31 13:26:39"}]





		[{"menu_id":"8","menu_name":"\u0e02\u0e48\u0e32\u0e27\u0e2a\u0e32\u0e23","member_type":"1","menu_status":"1",
		"menu_row":"7","timestamp":"2016-08-31 13:27:20"},
		{"menu_id":"9","menu_name":"\u0e2a\u0e34\u0e19\u0e04\u0e49\u0e32","member_type":"1","menu_status":"1",
		"menu_row":"9","timestamp":"2016-08-31 13:27:20"},
		{"menu_id":"10","menu_name":"\u0e41\u0e1a\u0e1a\u0e2a\u0e33\u0e23\u0e27\u0e08","member_type":"1",
		"menu_status":"1","menu_row":"10","timestamp":"2016-08-31 13:27:57"},
		{"menu_id":"11","menu_name":"Lucky Draw","member_type":"1","menu_status":"1","menu_row":"11","timestamp":"2016-08-31 13:27:57"},
		{"menu_id":"12","menu_name":"\u0e41\u0e19\u0e30\u0e19\u0e33","member_type":"1",
		"menu_status":"1","menu_row":"12","timestamp":"2016-08-31 13:28:28"},
		{"menu_id":"13","menu_name":"\u0e02\u0e49\u0e2d\u0e21\u0e39\u0e25\u0e2a\u0e48\u0e27\u0e19\u0e15\u0e31\u0e27","member_type":"1",
		"menu_status":"1","menu_row":"13","timestamp":"2016-08-31 13:28:28"},
		{"menu_id":"14","menu_name":"\u0e2a\u0e31\u0e48\u0e07\u0e0b\u0e37\u0e49\u0e2d\u0e2a\u0e34\u0e19\u0e04\u0e49\u0e32",
		"member_type":"1","menu_status":"1","menu_row":"14","timestamp":"2016-08-31 13:28:47"}]
	*/

	[Serializable]
	public class MenuService
	{
		public int menu_id;
		public int menu_status;
		public string menu_name;
		public string menu_row;
	}

	[Serializable]
	public class HeaderPDF
	{
		public int header_id;
		public string header_name;
		public string timestamp;
	}

	/*
	[{"file_id":"1","file_name":"Test 01",
	"file_link":"http:\/\/www.codebee.co.th\/project\/tman\/backend\/uploads\/SME_EXPRESS_ECONOMY_2016.pdf",
	"file_content":"\u0e2a\u0e27\u0e31\u0e2a\u0e14\u0e35\u0e04\u0e23\u0e31\u0e1a \u0e01\u0e25\u0e38\u0e48\u0e21\u0e17\u0e35\u0e48 01 ",
	"file_image":"11","file_status":"1","header_id":"1","timestamp":"2016-09-16 10:03:10"}]

	*/
	[Serializable]
	public class HeaderFile{
		public int file_id;
		public int file_status;
		public int header_id;
        public int image_id;
        public int image_status;
        public string image_pic;
        public string file_name;
		public string file_link;
		public string file_content;
		public string file_image;
		public string timestamp;
	}

    [Serializable]
    public class ProductLink
    {
        public string url_product;
    }


	/*
	
	"title":[{"title_id":"1","title_name":"\u0e2a\u0e34\u0e19\u0e04\u0e49\u0e32","title_status":"1","timestamp":"2016-09-12 15:02:47"},{"title_id":"2","title_name":"\u0e01\u0e32\u0e23\u0e43\u0e2b\u0e49\u0e02\u0e49\u0e2d\u0e21\u0e39\u0e25\u0e2a\u0e34\u0e19\u0e04\u0e49\u0e32","title_status":"1","timestamp":"2016-09-12 15:02:47"},{"title_id":"3","title_name":"\u0e23\u0e32\u0e04\u0e32\u0e2a\u0e34\u0e19\u0e04\u0e49\u0e32","title_status":"1","timestamp":"2016-09-12 15:03:05"},{"title_id":"4","title_name":"\u0e1a\u0e23\u0e23\u0e08\u0e38\u0e20\u0e31\u0e13\u0e11\u0e4c","title_status":"1","timestamp":"2016-09-12 15:03:05"},{"title_id":"5","title_name":"\u0e02\u0e49\u0e2d\u0e21\u0e39\u0e25\u0e1a\u0e19\u0e09\u0e25\u0e32\u0e01\u0e2a\u0e34\u0e19\u0e04\u0e49\u0e32","title_status":"1","timestamp":"2016-09-12 15:03:18"},{"title_id":"6","title_name":"\u0e01\u0e32\u0e23\u0e02\u0e19\u0e2a\u0e48\u0e07","title_status":"1","timestamp":"2016-09-12 15:03:18"},{"title_id":"8","title_name":"\u0e01\u0e32\u0e23\u0e15\u0e2d\u0e1a\u0e23\u0e31\u0e1a\u0e02\u0e2d\u0e07\u0e25\u0e39\u0e01\u0e04\u0e49\u0e32","title_status":"1","timestamp":"2016-09-12 17:25:39"}],
	"title_content":"<p>\r\n\u0e08\u0e15\u0e38\u0e04\u0e32\u0e21\u0e41\u0e08\u0e4a\u0e01\u0e40\u0e01\u0e47\u0e15 \u0e2d\u0e35\u0e2a\u0e15\u0e4c\u0e25\u0e47\u0e2d\u0e1a\u0e1a\u0e35\u0e49\u0e0a\u0e32\u0e23\u0e4c\u0e08 \u0e27\u0e34\u0e20\u0e31\u0e0a\u0e20\u0e32\u0e04\u0e2a\u0e01\u0e23\u0e31\u0e21\u0e23\u0e30\u0e42\u0e07\u0e01 \u0e01\u0e23\u0e38\u0e4a\u0e1b\u0e42\u0e2d\u0e40\u0e1e\u0e48\u0e19 \u0e0b\u0e31\u0e21\u0e40\u0e21\u0e2d\u0e23\u0e4c\u0e23\u0e32\u0e0a\u0e32\u0e19\u0e38\u0e0d\u0e32\u0e15\u0e2a\u0e40\u0e15\u0e2d\u0e23\u0e34\u0e42\u0e2d\u0e41\u0e23\u0e07\u0e1c\u0e25\u0e31\u0e01\u0e42\u0e21\u0e08\u0e34 \u0e40\u0e2d\u0e4b\u0e2d\u0e22\u0e38\u0e15\u0e34\u0e18\u0e23\u0e23\u0e21 \u0e41\u0e08\u0e4a\u0e01\u0e1e\u0e47\u0e2d\u0e15\u0e25\u0e30\u0e2d\u0e48\u0e2d\u0e19\u0e23\u0e27\u0e21\u0e21\u0e34\u0e15\u0e23 \u0e40\u0e2d\u0e47\u0e19\u0e17\u0e23\u0e32\u0e19\u0e0b\u0e4c\u0e43\u0e0a\u0e49\u0e07\u0e32\u0e19\u0e40\u0e0b\u0e35\u0e49\u0e22\u0e27\u0e0a\u0e47\u0e2d\u0e04\u0e41\u0e14\u0e23\u0e35\u0e48 \u0e40\u0e08\u0e35\u0e4a\u0e22\u0e27\u0e40\u0e1a\u0e19\u0e42\u0e15\u0e30\u0e41\u0e08\u0e47\u0e01\u0e1e\u0e47\u0e2d\u0e15 \u0e41\u0e08\u0e21 \u0e21\u0e34\u0e19\u0e17\u0e4c\u0e04\u0e2d\u0e19\u0e42\u0e14\u0e21\u0e34\u0e40\u0e19\u0e35\u0e22\u0e21\u0e42\u0e1e\u0e25\u0e32\u0e23\u0e2d\u0e22\u0e14\u0e4c\u0e18\u0e23\u0e23\u0e21\u0e32\u0e20\u0e34\u0e1a\u0e32\u0e25\u0e0b\u0e32\u0e14\u0e34\u0e2a\u0e21\u0e4c \u0e44\u0e1a\u0e40\u0e1a\u0e34\u0e25\u0e1a\u0e23\u0e23\u0e1e\u0e0a\u0e19 \u0e1b\u0e0f\u0e34\u0e2a\u0e31\u0e21\u0e1e\u0e31\u0e19\u0e18\u0e4c \u0e14\u0e31\u0e21\u0e1e\u0e4c \u0e2a\u0e40\u0e1b\u0e04 \u0e14\u0e22\u0e38\u0e04\u0e42\u0e1b\u0e23\u0e40\u0e08\u0e47\u0e04\u0e2a\u0e38\u0e19\u0e17\u0e23\u0e35\u0e22\u0e4c\u0e1b\u0e0f\u0e34\u0e2a\u0e31\u0e21\u0e1e\u0e31\u0e19\u0e18\u0e4c\u0e04\u0e27\u0e32\u0e21\u0e2b\u0e21\u0e32\u0e22\r\n\r\n<\/p>\r\n\r\n<p>\r\n\u0e42\u0e2d\u0e40\u0e1b\u0e2d\u0e40\u0e23\u0e40\u0e15\u0e2d\u0e23\u0e4c\u0e19\u0e32\u0e07\u0e41\u0e1a\u0e1a \u0e2d\u0e34\u0e48\u0e21\u0e41\u0e1b\u0e23\u0e49\u0e0a\u0e32\u0e23\u0e4c\u0e15 \u0e1e\u0e38\u0e17\u0e42\u0e18\u0e1b\u0e39\u0e2d\u0e31\u0e14\u0e2a\u0e36\u0e19\u0e32\u0e21\u0e34\u0e42\u0e1b\u0e23 \u0e21\u0e25\u0e20\u0e32\u0e27\u0e30 \u0e41\u0e08\u0e4a\u0e01\u0e40\u0e01\u0e47\u0e15\u0e25\u0e30\u0e2d\u0e48\u0e2d\u0e19 \u0e2a\u0e44\u0e15\u0e23\u0e04\u0e4c\u0e2d\u0e48\u0e27\u0e21\u0e1e\u0e30\u0e40\u0e23\u0e2d\u0e1e\u0e23\u0e35\u0e40\u0e21\u0e35\u0e22\u0e21 \u0e41\u0e04\u0e0a\u0e40\u0e0a\u0e35\u0e22\u0e23\u0e4c\u0e0b\u0e32\u0e1f\u0e32\u0e23\u0e35\u0e08\u0e31\u0e21\u0e42\u0e1a\u0e49\u0e2a\u0e1b\u0e47\u0e2d\u0e15 \u0e2d\u0e21\u0e32\u0e15\u0e22\u0e32\u0e18\u0e34\u0e1b\u0e44\u0e15\u0e22\u0e41\u0e21\u0e48\u0e04\u0e49\u0e32 \u0e08\u0e4a\u0e2d\u0e01\u0e01\u0e35\u0e49 \u0e01\u0e29\u0e31\u0e15\u0e23\u0e34\u0e22\u0e32\u0e18\u0e34\u0e23\u0e32\u0e0a\u0e0b\u0e32\u0e23\u0e4c\u0e14\u0e35\u0e19\u0e2a\u0e40\u0e15\u0e0a\u0e31\u0e19 \u0e42\u0e2b\u0e25\u0e22\u0e42\u0e17\u0e48\u0e22\u0e40\u0e1e\u0e19\u0e01\u0e27\u0e34\u0e19\u0e2d\u0e38\u0e40\u0e17\u0e19\u0e23\u0e35\u0e44\u0e17\u0e23\u0e4c\u0e22\u0e39\u0e27\u0e35 \u0e14\u0e47\u0e2d\u0e01\u0e40\u0e15\u0e2d\u0e23\u0e4c\u0e40\u0e1e\u0e17\u0e19\u0e32\u0e01\u0e32\u0e23\u0e2e\u0e34\u0e1b\u0e42\u0e1b\u0e2a\u0e15\u0e23\u0e2d\u0e27\u0e4c\u0e40\u0e1a\u0e2d\u0e23\u0e4c\u0e23\u0e35\u0e1a\u0e34\u0e25 \u0e0b\u0e31\u0e21\u0e40\u0e21\u0e2d\u0e23\u0e4c\u0e1a\u0e4b\u0e2d\u0e22\u0e1a\u0e39\u0e15\u0e34\u0e04\u0e41\u0e21\u0e04\u0e40\u0e04\u0e2d\u0e40\u0e23\u0e25\u0e41\u0e1a\u0e04\u0e42\u0e2e \u0e18\u0e38\u0e23\u0e01\u0e23\u0e23\u0e21\u0e40\u0e17\u0e04\u0e42\u0e19\u0e41\u0e04\u0e23\u0e15\u0e2a\u0e21\u0e34\u0e15\u0e34\u0e40\u0e27\u0e0a \u0e1e\u0e32\u0e2a\u0e40\u0e08\u0e2d\u0e23\u0e4c\u0e44\u0e23\u0e2a\u0e4c\u0e42\u0e1b\u0e23\u0e40\u0e08\u0e47\u0e04 \u0e40\u0e25\u0e04\u0e40\u0e0a\u0e2d\u0e23\u0e4c\u0e27\u0e49\u0e2d\u0e22\u0e22\u0e39\u0e27\u0e35\u0e43\u0e0a\u0e49\u0e07\u0e32\u0e19\u0e1f\u0e2d\u0e22\u0e25\u0e4c\r\n\r\n<\/p>"}


	*/
    [Serializable]
    public class SatisFactionTitle
    {
		public int title_id;
		public int title_status;
		public string title_name;
		public string timestamp;
    }
	[Serializable]
	public class SatisFactionData
	{
		public List<SatisFactionTitle> title;
		public string title_content;
	}
    [Serializable]
    public class RecommendTitle
    {
		public string com_id;
		public string com_name;
		public string timestamp;
    }
    [Serializable]
    public class LuckyDrawCode
    {
        public string lucky_code;
        public string award_date;
        public string award_episode;
		public string award_content;
    }

    /*
     [{"member_id":"1","device_id":"0","username":"admin","password":"qwerty","email":"__tester@gmail.com",
     "sername":"Thiti Miangmook _test_edit_1","member_tel1":"111112","member_tel2":"222223","member_tel3":"3333334",
     "member_fax":"44444455","member_address":"fsdfdsfsdfsdfsdfsdfsdfsfdsfsdfsdfsfsdfdsfsdfsdfsdfsdfsdfsdfsdfdsfsdfsdfsdfsdfsdfsfsfsfsdfsdfsfsdfsfsdfsdfsdfsdfsdf",
     "province_id":"1","member_type":"0","store_id":"0","store_name":"0","code_store":" ","code_offset":" ",
     "member_code":"REP518724","member_point":"0","group_id":"1","member_status":"1","timestamp":"2016-08-31 16:08:30",
     "PROVINCE_ID":"1","PROVINCE_NAME":"\u0e01\u0e23\u0e38\u0e07\u0e40\u0e17\u0e1e\u0e21\u0e2b\u0e32\u0e19\u0e04\u0e23",
     "group_name":"\u0e40\u0e02\u0e15 1","storename":null}]
     */
    [Serializable]
    public class MemberProfile
    {
        public int member_id;
        public string device_id;
        public string username;
        public string password;
        public string email;
        public string sername;
        public string member_tel1;
        public string member_tel2;
        public string member_tel3;
        public string member_fax;
        public string member_address;
        public string member_code;
		public string member_point;
        public int member_status;
		public string group_id;
        public int province_id;
		public int member_type;
        public int store_id;
        public string store_name;
        public string code_store;
        public string code_offset;
        public int PROVINCE_ID;
        public string PROVINCE_NAME;
        public string group_name;
        public string storename;
        public string timestamp;
        public bool success;
    }
	[Serializable]
	public class ReceiveMemberPoint
	{
		public int member_point;
		public string description;
		public bool success;
	}

	[Serializable]
	public class NearCheckin{
		public double distance;
		 

	}
	/*{"member_point":1500,"success":true,"description":"\u0e40\u0e01\u0e47\u0e1a\u0e04\u0e48\u0e32\u0e04\u0e30\u0e41\u0e19\u0e19\u0e2a\u0e30\u0e2a\u0e21\u0e40\u0e23\u0e35\u0e22\u0e1a\u0e23\u0e49\u0e2d\u0e22\u0e41\u0e25\u0e49\u0e27"}*/
}
