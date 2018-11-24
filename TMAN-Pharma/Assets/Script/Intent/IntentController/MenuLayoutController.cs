using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuLayoutController : MonoBehaviour {
    public int column;
    void Start()
    {
        float columnWidth = Mathf.Ceil(GameObject.Find("EFE_Canvas").GetComponent<RectTransform>().rect.width/ column);
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(columnWidth, columnWidth);
    }
}
