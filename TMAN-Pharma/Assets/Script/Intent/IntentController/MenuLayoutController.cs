using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuLayoutController : MonoBehaviour {
    public int column;
    void Start()
    {
        float columnWidth = Mathf.Floor(VariableManager.GetInstance.screenCanvas.width/ column);
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(columnWidth, columnWidth);
    }
}
