using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScrollviewController : MonoBehaviour {
    [SerializeField]
    GridLayoutGroup gridLayout;
    public float flexibleWidth;

    private float paddingWidth;
    void Start()
    {
        paddingWidth = gridLayout.padding.left + gridLayout.padding.right;
        gridLayout.cellSize = new Vector2(VariableManager.GetInstance.screenCanvas.width - paddingWidth, flexibleWidth);
    }
	
}
