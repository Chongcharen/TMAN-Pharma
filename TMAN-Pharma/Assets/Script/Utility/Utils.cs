using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
public class Utils : MonoBehaviour {

	public static bool IsValidEmailAddress(string s)
	{
		Regex regex = new Regex(@"[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
		return regex.IsMatch(s);
	}
    public static byte[] Texture2dToByteArray(Texture2D texture2d)
    {
        byte[] byteArray = texture2d.EncodeToJPG();
        return byteArray;
    }
    

}
