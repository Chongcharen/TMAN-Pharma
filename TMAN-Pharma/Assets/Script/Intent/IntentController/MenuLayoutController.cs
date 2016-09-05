using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuLayoutController : MonoBehaviour {

    void Start()
    {
        float columnWidth = Mathf.Floor(VariableManager.GetInstance().screenCanvas.width/3);
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(columnWidth, columnWidth);
    }
}
