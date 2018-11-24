using UnityEngine;
using System.Collections;
using System;
public class AcceptCallback : IAcceptPopup
{
    public Action action;
	public void Accept()
    {
        if(action != null)
        {
            action.Invoke();
        }
    }
}
