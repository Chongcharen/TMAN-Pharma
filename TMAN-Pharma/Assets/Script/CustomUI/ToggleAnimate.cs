using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class ToggleAnimate : Toggle {

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick (eventData);
		transform.DOPunchScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f, 2);
	}

}
