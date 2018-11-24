using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    /// <summary>
    /// Labels are graphics that display text.
    /// </summary>

    [AddComponentMenu("UI/Text_Extend")]
    public class Text_Extend : Text, IPointerClickHandler
    {

        protected GameObject mGo;
        protected RectTransform mTrans;

        public GameObject cachedGameObject { get { if (mGo == null) mGo = gameObject; return mGo; } }

        public RectTransform cachedTransform { get { if (mTrans == null) mTrans = transform as RectTransform; return mTrans; } }



        [TextArea(3, 10)][SerializeField] protected string m_OriginText = String.Empty;

        // text with flag
        public string OriginText
        {
            get
            {
                return m_OriginText;
            }
            set
            {
                m_OriginText = value;
                setPrintText();
            }
        }

        // true text that print in UI.
        public string processedText
        {
            get { return base.text; }
        }

        protected List<int> mOriginIndexList = new List<int>();

        protected List<TextSegmentFlag> mUnderlineIndexList = new List<TextSegmentFlag>();


#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        [NonSerialized]
        // vertices pixel position per font.
        VertexHelper mVertexHelper = new VertexHelper();
#else
        // vertices pixel position per font.
        List<UIVertex> vbo = new List<UIVertex>();
#endif

        protected TextGenerator mCharTextGenerator;

        protected UnderlineEdgeConfig mUnderlineEdgeCfg;

        //flag "NonSerialized", compile code over will reset to null. if not do this, compile over underline will missing.
        [NonSerialized]
        string mLastPrintOriginText;

        public float underlineOffsetY;
        [SerializeField]
        public float underlineHeightScale = 0.7f;

        [SerializeField]
        [FormerlySerializedAs("linkObject")]
        public bool linkObject = true;

        protected override void Awake()
        {
            base.Awake();
            setPrintText();
        }

#if UNITY_EDITOR

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        protected override void OnValidate()
        {
            base.OnValidate();
            setPrintText();
        }
#else
        protected override void UpdateGeometry()
        {
            setPrintText();
            base.UpdateGeometry();
        }
#endif

#endif

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!linkObject || OriginText.Length < 1)
                return;

            Vector2 localPos;
            bool ret = RectTransformUtility.ScreenPointToLocalPointInRectangle(cachedTransform, eventData.position, eventData.pressEventCamera, out localPos);
            if (!ret)
                return;

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
            int textIndex = UGUIText_Extend.GetExactCharacterIndex(mVertexHelper, localPos, this);
#else
            int textIndex = UGUIText_Extend.GetExactCharacterIndex(vbo, localPos, this);
#endif

            if (textIndex < 0)
                return;

            if (mOriginIndexList.Count <= textIndex)
                return;

            int originIndex = mOriginIndexList[textIndex];
            UGUIText_Extend.ProcessLinkObjectText(OriginText, originIndex);

        }

        public void setPrintText()
        {
            if (mLastPrintOriginText == OriginText)
                return;
            mLastPrintOriginText = OriginText;

            if (linkObject)
            {

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
                base.text = UGUIText_Extend.PrintText(ref m_OriginText, mOriginIndexList, mUnderlineIndexList);
#else
                base.m_Text = UGUIText_Extend.PrintText(ref m_OriginText, mOriginIndexList, mUnderlineIndexList);
#endif
            }
            else
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
                base.text = this.OriginText;
#else
                base.m_Text = this.OriginText;
#endif
        }

        /// <summary>
        /// at Unity version 4.6 - 5.1, don't write [u] out of <color=...> (eg. [u]<color=#00507C>Falcon[/u]</color>), 
        /// because underline calculate include rich text(like "<color") vertex and char info.
        /// </summary>
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        void fillUnderline(VertexHelper toFill)
#else
        void fillUnderline(List<UIVertex> toFill)
#endif
        {
            //comment it, if not, save Scene the underline will missing.
            //if (null == mCharTextGenerator)
                mCharTextGenerator = new TextGenerator(1);
            
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
            m_DisableFontTextureRebuiltCallback = true;
#else
#endif
            Vector2 extents = rectTransform.rect.size;
            TextGenerationSettings settings = GetGenerationSettings(extents);
            mCharTextGenerator.Populate("_", settings);

            IList<UIVertex> verts = mCharTextGenerator.verts;
            if (verts.Count < 4)
                return;
            Vector2 topUV = (verts[0].uv0 + verts[1].uv0) / 2;
            Vector2 bottomUV = (verts[2].uv0 + verts[3].uv0) / 2;
            Vector2 centerUV = (topUV + bottomUV) / 2;
            
            UIVertex[] temp = new UIVertex[4];
            for (int i = 0; i < 4; i++)
            {
                temp[i] = verts[i];
                temp[i].uv0 = centerUV;
                //if (i < 2)
                //    temp[i].uv0 = topUV;
                //else
                //    temp[i].uv0 = bottomUV;
            }

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
            float underLineTopY = mCharTextGenerator.lines[0].topY;
#else
            float underLineTopY = mCharTextGenerator.characters[0].cursorPos.y;
#endif
            float offsetTop = verts[0].position.y - underLineTopY;
            float offsetBottom = verts[3].position.y - underLineTopY;

            float adjustY = underlineOffsetY * pixelsPerUnit;
            offsetTop += adjustY;
            offsetBottom += adjustY;

            float underlineHeight = Mathf.Abs(offsetBottom - offsetTop);
            float scale = (underlineHeightScale - 1.0f) * underlineHeight / 2;
            offsetTop += scale;
            offsetBottom -= scale;

            IList<UILineInfo> lines = cachedTextGenerator.lines;
            TextSegmentFlag segment;
            int segmentIdx = 0, segmentStartIdx = -1, segmentEndIdx = -1, endIdx = lines.Count - 1;
            bool start = false;

            if (null == mUnderlineEdgeCfg)
                mUnderlineEdgeCfg = new UnderlineEdgeConfig();
            mUnderlineEdgeCfg.init(toFill, temp, cachedTextGenerator, offsetTop, offsetBottom, this);

            for (int i = 0, max = lines.Count; i < max; ++i)
            {
                if (segmentIdx == mUnderlineIndexList.Count)
                    break;

                segment = mUnderlineIndexList[segmentIdx];
                if (!start && (lines[i].startCharIdx > segment.start || i == endIdx))
                {
                    if (!mUnderlineEdgeCfg.setUnderLineStartPos(segment.start, ref segmentStartIdx, i))
                        break;

                    start = true;
                }

                if (start && (lines[i].startCharIdx > segment.end || i == endIdx))
                {
                    segmentEndIdx = i - (lines[i].startCharIdx > segment.end ? 1 : 0);

                    if (segmentStartIdx == segmentEndIdx)
                    {
                        if (!mUnderlineEdgeCfg.setUnderLineEndPos(segment.end))
                            break;

                        segmentIdx++;
                        start = false;
                        --i;
                        continue;
                    }

                    //start -> start line end
                    int startLineEndCharIdx = lines[segmentStartIdx + 1].startCharIdx - 1;
                    if ('\n' == text[startLineEndCharIdx])
                        --startLineEndCharIdx;

                    if (!mUnderlineEdgeCfg.setUnderLineEndPos(startLineEndCharIdx))
                        break;

                    //for(line start + 1 --> line end - 1)
                    for (int k = segmentStartIdx + 1; k < segmentEndIdx; ++k)
                    {
                        int lineStartCharIdx = lines[k].startCharIdx;

                        //ignore line feed
                        if ('\n' == text[lineStartCharIdx])
                            continue;

                        if (!mUnderlineEdgeCfg.setUnderLineStartPos(lineStartCharIdx, ref k, k))
                            break;

                        int lineEndCharIdx = lines[k + 1].startCharIdx - 1;
                        if ('\n' == text[lineEndCharIdx])
                            --lineEndCharIdx;

                        if (!mUnderlineEdgeCfg.setUnderLineEndPos(lineEndCharIdx))
                            break;
                    }

                    //end line start -> end
                    int endLineStartCharIdx = lines[segmentEndIdx].startCharIdx;

                    if (!mUnderlineEdgeCfg.setUnderLineStartPos(endLineStartCharIdx, ref segmentEndIdx, segmentEndIdx))
                        break;

                    if (!mUnderlineEdgeCfg.setUnderLineEndPos(segment.end))
                        break;

                    segmentIdx++;
                    start = false;
                    --i;
                    continue;
                }
            }


#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
            m_DisableFontTextureRebuiltCallback = false;
#else
#endif
        }
        
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            base.OnPopulateMesh(toFill);

            //add underline vertex buffer
            fillUnderline(toFill);
            
            Mesh tMesh = new Mesh();
            toFill.FillMesh(tMesh);
            mVertexHelper = new VertexHelper(tMesh);
        }
#else
        protected override void OnFillVBO(List<UIVertex> toFill)
        {
            base.OnFillVBO(toFill);

            //add underline vertex buffer
            fillUnderline(toFill);

            vbo.Clear();
            vbo.AddRange(toFill);
        }
#endif

    }
}
