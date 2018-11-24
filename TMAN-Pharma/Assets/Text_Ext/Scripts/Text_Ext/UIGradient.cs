

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/UIGradient")]

public enum GradientStyle
{
    Horizontal,
    Vertical,
}

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
public class UIGradient : BaseMeshEffect
#else
public class UIGradient : BaseVertexEffect
#endif
{
    [SerializeField]
    private Color32 topColor = Color.white;
    [SerializeField]
    private Color32 bottomColor = Color.black;

    [SerializeField]
    private GradientStyle gradientStyle = GradientStyle.Vertical;

    [SerializeField]
    private bool textType = false;
    
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
    [NonSerialized]
    private static Mesh s_TransferMesh;
#endif

    Vector3 getVertexListIdx(List<UIVertex> list, int idx)
    {
        return list[idx].position;
    }

    Vector3 getVertexListIdx(Vector3[] list, int idx)
    {
        return list[idx];
    }
    
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
    public override void ModifyMesh(Mesh mesh)
#else
    public override void ModifyVertices(List<UIVertex> vertexList)
#endif
    {
        if (!IsActive())
        {
            return;
        }

        switch (gradientStyle)
        {
            case GradientStyle.Vertical:
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
                setVerticalColor(mesh);
#else
                setVerticalColor(vertexList);
#endif
                break;
            case GradientStyle.Horizontal:
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
                setHorizontalColor(mesh);
#else
                setHorizontalColor(vertexList);
#endif
                break;
        }
    }

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
    public override void ModifyMesh(VertexHelper vh)
    {
        if (null == s_TransferMesh)
            s_TransferMesh = new Mesh();

        vh.FillMesh(s_TransferMesh);
        ModifyMesh(s_TransferMesh);
        VertexHelper temp = new VertexHelper(s_TransferMesh);

        int count = temp.currentVertCount;
        UIVertex tep = new UIVertex();
        for (int i = 0; i < count; ++i)
        {
            temp.PopulateUIVertex(ref tep, i);
            vh.SetUIVertex(tep, i);
        }
    }
#endif

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
    void setVerticalColor(Mesh mesh)
#else
    void setVerticalColor(List<UIVertex> vertexList)
#endif
    {
        if (textType && null != this.gameObject.GetComponent<Text>())
        {
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
            setTextVerticalColor(mesh);
#else
                setTextVerticalColor(vertexList);
#endif
            return;
        }

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        Vector3[] vertexList = mesh.vertices;
        int count = mesh.vertexCount;
#else
        int count = vertexList.Count;
#endif
        if (count < 1)
            return;

        float bottomY = getVertexListIdx(vertexList, 0).y;
        float topY = bottomY;

        //has a problem: if use at multiple line text, the gradient will effect from top line to bottom line, not font at one line.
        for (int i = 1; i < count; i++)
        {
            float y = getVertexListIdx(vertexList, i).y;

            if (y > topY)
            {
                topY = y;
            }
            else if (y < bottomY)
            {
                bottomY = y;
            }
        }

        float uiElementHeight = topY - bottomY;

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        List<Color32> colors = new List<Color32>();
        for (int i = 0; i < count; i++)
        {
            colors.Add(Color32.Lerp(bottomColor, topColor, (vertexList[i].y - bottomY) / uiElementHeight));
        }
        mesh.SetColors(colors);
#else
            for (int i = 0; i < count; i++) {
				UIVertex uiVertex = vertexList[i];
				uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.y - bottomY) / uiElementHeight);
				vertexList[i] = uiVertex;
			}
#endif
    }

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
    void setTextVerticalColor(Mesh mesh)
#else
    void setTextVerticalColor(List<UIVertex> vertexList)
#endif
    {

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        Vector3[] vertexList = mesh.vertices;
        int count = mesh.vertexCount;
#else
        int count = vertexList.Count;
#endif
        if (count < 1)
            return;

        Text textComp = this.gameObject.GetComponent<Text>();

        IList<UILineInfo> lines = textComp.cachedTextGenerator.lines;

        float unitsPerPixel = 1 / textComp.pixelsPerUnit;
        UILineInfo lineInfo;
        int lineEndCharIdx;
        int i0;
        float lineTopY, lineHeight;

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        List<Color32> colors = new List<Color32>();
#else
        Vector2 textAnchorPivot = Text.GetTextAnchorPivot(textComp.alignment);
        float textTopY = 0f;
        float textHeight = lines[0].height * lines.Count;
        Rect inputRect = textComp.rectTransform.rect;
        if (1 == textAnchorPivot.y)
            textTopY = inputRect.yMax;
        else if (0 == textAnchorPivot.y)
            textTopY = inputRect.yMin + textHeight;
        else
            textTopY = inputRect.center.y + textHeight / 2;
#endif

        for (int i = 0, max = lines.Count; i < max; ++i)
        {
            lineInfo = lines[i];
            lineHeight = lineInfo.height * unitsPerPixel;
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
            lineTopY = lineInfo.topY * unitsPerPixel;
#else
            lineTopY = (textTopY - i * lineHeight) * unitsPerPixel;
#endif
            if(max > i + 1)
                lineEndCharIdx = lines[i+1].startCharIdx;
            else
                lineEndCharIdx = count / 4;
            
            for (int j = lineInfo.startCharIdx; j < lineEndCharIdx; ++j)
            {
                i0 = (j << 2);
                for (int k = 0; k < 4; ++k)
                {
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
                    colors.Add(Color32.Lerp(topColor, bottomColor, (lineTopY - vertexList[i0 + k].y) / lineHeight));
#else
                UIVertex uiVertex = vertexList[i0 + k];
                uiVertex.color = Color32.Lerp(topColor, bottomColor, (lineTopY - uiVertex.position.y) / lineHeight);
                vertexList[i0 + k] = uiVertex;
#endif
                }
            }
        }

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        mesh.SetColors(colors);
#endif
    }

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
    void setHorizontalColor(Mesh mesh)
#else
    void setHorizontalColor(List<UIVertex> vertexList)
#endif
    {

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        Vector3[] vertexList = mesh.vertices;
        int count = mesh.vertexCount;
#else
        int count = vertexList.Count;
#endif
        if (count < 1)
            return;

        float leftX = getVertexListIdx(vertexList, 0).x;
        float rightX = leftX;

        //has a problem: if use at multiple line text, the gradient will effect from top line to bottom line, not font at one line.
        for (int i = 1; i < count; i++)
        {
            float x = getVertexListIdx(vertexList, i).x;

            if (x > rightX)
            {
                rightX = x;
            }
            else if (x < leftX)
            {
                leftX = x;
            }
        }

        float uiElementWidth = rightX - leftX;

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        List<Color32> colors = new List<Color32>();
        for (int i = 0; i < count; i++)
        {
            colors.Add(Color32.Lerp(topColor, bottomColor, (vertexList[i].x - leftX) / uiElementWidth));
        }
        mesh.SetColors(colors);
#else
            for (int i = 0; i < count; i++) {
				UIVertex uiVertex = vertexList[i];
				uiVertex.color = Color32.Lerp( topColor,bottomColor, (uiVertex.position.x - leftX) / uiElementWidth);
				vertexList[i] = uiVertex;
			}
#endif
    }

}
