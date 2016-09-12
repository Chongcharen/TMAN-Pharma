using UnityEngine;
using System.Collections;
using Service.ClassReference;
public class DataManager : MonoBehaviour {

    public static DataManager instance;
    RawMember member;
    void Awake()
    {
        instance = this;
    }


    #region MyRegion
    public void SetMember(RawMember _member)
    {
        member = _member;
    }
    public int GetMemberType()
    {
        return member.member_type;
    }
    public RawMember GetMember()
    {
        return member;
    } 
    #endregion
}
