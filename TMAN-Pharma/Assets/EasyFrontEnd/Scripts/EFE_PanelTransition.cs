using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using DG.Tweening;



public class EFE_PanelTransition : MonoBehaviour {
	
	public TransitionType transitionInType;
	public enum TransitionType {None,SlideInFromLeft,SlideInFromRight,SlideInFromTop,SlideInFromBottom};//FadeIn, ScaleIn coming soon
	[Tooltip("For main panels you can leav this at zero but is useful for offsetting buttons and images which have EFE transition components that should slide in AFTER the" +
	"main panel has transitioned into view")]
	public float transitionInDelay;
	public UnityEvent transitionInCompletedEvent;
	public UnityEvent transitionOutCompletedEvent;
	
	public TransitionTypeOut transitionOutType;
	public enum TransitionTypeOut {None,SlideOutToLeft,SlideOutToRight,SlideOutToTop,SlideOutToBottom};// FadeOut, ScaleOut coming soon
	public EaseType easeType;
	
	[Tooltip("Transition time in seconds")]
	public float transitionSpeed ;
	
	//ease types
	public enum EaseType 
	{Linear,
		InBack,
		InBounce,
		InCirc,
		InCubic,
		InElastic,
		InExpo,
		InOutBack,
		InOutBounce,
		InOutCirc,
		InOutCubic,
		InOutExpo,
		InOutQuad,
		InOutQuart,
		InOutQuint,
		InOutSine,
		InQuad,
		InQuart,
		InSine,
		OutBack,
		OutBounce,
		OutCirc,
		OutCubic,
		OutElastic,
		OutExpo,
		OutQuad,
		OutQuart,
		OutQuint,
		OutSine
	};
	
	
	
	[Tooltip("OPTIONAL -An additional panel which will be positioned underneath the panel you are transitioning to. Commonly used for fading out of any panels or images underneath an overlay (or popup) panel. This can be set to nothing in most cases.")]
	public GameObject backgroundFadePanel ;//add one of these to create a faded background (good for popups)
	
	public bool autoCentrePivotToCanvas;
	
	//private RectTransform myRect;
	private RectTransform rootCanvasRect;
	private Vector3 startPosition;
	private Vector3 startScale;
	
	void Awake () 
	{
		rootCanvasRect = GameObject.Find("EFE_Canvas").GetComponent<RectTransform>();
		startPosition = transform.position;
		startScale = gameObject.transform.localScale;
		
		
		//Experimental auto adjust pivots if user required - NOT USED
		if(autoCentrePivotToCanvas)
		{
			print("parent is "+gameObject.transform.parent.gameObject.name);
			gameObject.GetComponent<RectTransform>().pivot= gameObject.transform.parent.GetComponent<RectTransform>().pivot;
				//rootCanvasRect.anchoredPosition;
			print("Centered pivot");
		}
		
		
	}
	
	void Update () 
	{
	
	}
	
	
	public void SnapToInititialPosition()
	{
		transform.position = startPosition;
	}
	public void DoTransitionIn()
	{
		
		
		
		//StartCoroutine(DoTransitionOffset());
		Invoke("DoTransitionInAfterOffset",transitionInDelay);
		
		
	}
	
	
	//MAIN TRANSITION
	public void DoTransitionInAfterOffset()
	{
		gameObject.transform.localScale = startScale;
		//hide any children that might have transitions
		for(int x=0;x<gameObject.transform.childCount;x++)
		{
			EFE_PanelTransition childTransitionComp = gameObject.transform.GetChild(x).GetComponent<EFE_PanelTransition>();
			if(childTransitionComp)
			{
				//gameObject.transform.GetChild(x).gameObject.SetActive(false);
				gameObject.transform.GetChild(x).transform.localScale = new Vector3(0, 0, 0);//hide it
				
			}
		}
		
		//PositionBackgroundFadePanel();
		SetEasingType();
		
		switch (transitionInType)
		{
		case TransitionType.None:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				transform.position =newPosition;
				break;
			}
		case TransitionType.SlideInFromLeft:
			{
				//Vector3 myposition = this.gameObject.transform.localPosition;
				//myposition.x = myposition.x - myRect.rect.width;
				//this.gameObject.transform.localPosition	=myposition;
				//transform.DOMove(rootCanvasRect.position, transitionSpeed).OnComplete(TransitionComplete);;
				//break;
				
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.x =-rootCanvasRect.anchoredPosition.x;// + myRect.rect.width;
				transform.position =newPosition;
				transform.DOMove(rootCanvasRect.anchoredPosition, transitionSpeed).OnComplete(TransitionInComplete);
				break;
			}
		case TransitionType.SlideInFromRight:
			{

				//Vector3 myposition = this.gameObject.transform.localPosition;
				//myposition.x = myposition.x + myRect.rect.width;
				////myposition.y = myposition.y - myRect.rect.height;
				//this.gameObject.transform.localPosition	=myposition;
				//transform.DOMove(rootCanvasRect.position, transitionSpeed).OnComplete(TransitionComplete);
				
				
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.x =rootCanvasRect.anchoredPosition.x*3;// + myRect.rect.width;
				transform.position =newPosition;
				transform.DOMove(rootCanvasRect.anchoredPosition, transitionSpeed).OnComplete(TransitionInComplete);
				break;
				

			}
		case TransitionType.SlideInFromTop:
			{
				//Vector3 myposition = this.gameObject.transform.localPosition;
				//myposition.y = myposition.y + myRect.rect.height;
				//this.gameObject.transform.localPosition	=myposition;
				//transform.DOMove(rootCanvasRect.position, transitionSpeed).OnComplete(TransitionComplete);;
				//break;
				
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.y =rootCanvasRect.anchoredPosition.y*3;// + myRect.rect.width;
				transform.position =newPosition;
				transform.DOMove(rootCanvasRect.anchoredPosition, transitionSpeed).OnComplete(TransitionInComplete);
				break;
			}
		case TransitionType.SlideInFromBottom:
			{
				
				//Vector3 myposition = this.gameObject.transform.localPosition;
				//myposition.y = myposition.y - myRect.rect.height;
				//this.gameObject.transform.localPosition	=myposition;
				//transform.DOMove(rootCanvasRect.position, transitionSpeed).OnComplete(TransitionComplete);;
				//break;
				
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.y =-rootCanvasRect.anchoredPosition.y;// + myRect.rect.width;
				transform.position =newPosition;
				transform.DOMove(rootCanvasRect.anchoredPosition, transitionSpeed).OnComplete(TransitionInComplete);
				break;
			}
			
		//case TransitionType.FadeIn:
		//	{
		//		//TODO
		//		print("Panel enabled - Fade");
		//		break;
		//	}
		//case TransitionType.ScaleIn:
		//	{
		//		//TODO
		//		print("Panel enabled - ScaleIn");
		//		break;
		//	}
		}
		
		
	}
	
	
	
	public void TransitionInComplete()
	{
		//do events that are called by user via inspector
		transitionInCompletedEvent.Invoke();
		StartCoroutine(DoChildTransitionsIn());
	}
	
	public void TransitionOutComplete()
	{
		//do events that are called by user via inspector
		transitionOutCompletedEvent.Invoke();
		gameObject.transform.localScale = new Vector3(0, 0, 0);
	}
	
	
	IEnumerator DoChildTransitionsIn()
	{
		yield return new WaitForSeconds(0);
		//print("Transition complete");
		//also send message to children that might also have transition scripts
		for(int x=0;x<gameObject.transform.childCount;x++)
		{
			gameObject.transform.GetChild(x).gameObject.SetActive(true);
			//gameObject.transform.GetChild(x).transform.localScale = new Vector3(1, 1, 1);
			gameObject.transform.GetChild(x).SendMessage("DoTransitionIn",SendMessageOptions.DontRequireReceiver);
		}
	}
	
	
	
	public void DoTransitionOut()
	{
		SetEasingType();
		
		switch (transitionOutType)
		{
		case TransitionTypeOut.None:
			{
				
				break;
			}
		case TransitionTypeOut.SlideOutToLeft:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.x =-rootCanvasRect.anchoredPosition.x;// - myRect.rect.width;
				transform.DOMove(newPosition, transitionSpeed).OnComplete(TransitionOutComplete);
				break;
			}
		case TransitionTypeOut.SlideOutToRight:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.x =rootCanvasRect.anchoredPosition.x*3;// + myRect.rect.width;
				transform.DOMove(newPosition, transitionSpeed).OnComplete(TransitionOutComplete);
				break;
			}
		case TransitionTypeOut.SlideOutToTop:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.y =rootCanvasRect.anchoredPosition.y *3;//+ myRect.rect.height;
				transform.DOMove(newPosition, transitionSpeed).OnComplete(TransitionOutComplete);
				break;
			}
		case TransitionTypeOut.SlideOutToBottom:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.y =-rootCanvasRect.anchoredPosition.y ;//- myRect.rect.height;
				transform.DOMove(newPosition, transitionSpeed).OnComplete(TransitionOutComplete);
				break;
			}
		}
	}
	
	public void SetEasingType()
	{
		
		//if(easeType==EaseType.Flash){DOTween.defaultEaseType = Ease.Flash;return;}
		if(easeType==EaseType.InBack){DOTween.defaultEaseType = Ease.InBack;return;}
		if(easeType==EaseType.InBounce){DOTween.defaultEaseType = Ease.InBounce;return;}
		if(easeType==EaseType.InCirc){DOTween.defaultEaseType = Ease.InCirc;return;}
		if(easeType==EaseType.InCubic){DOTween.defaultEaseType = Ease.InCubic;return;}
		if(easeType==EaseType.InElastic){DOTween.defaultEaseType = Ease.InElastic;return;}
		if(easeType==EaseType.InExpo){DOTween.defaultEaseType = Ease.InExpo;return;}
		//if(easeType==EaseType.InFlash){DOTween.defaultEaseType = Ease.InFlash;return;}
		if(easeType==EaseType.InOutBack){DOTween.defaultEaseType = Ease.InOutBack;return;}
		if(easeType==EaseType.InOutBounce){DOTween.defaultEaseType = Ease.InOutBounce;return;}
		if(easeType==EaseType.InOutCirc){DOTween.defaultEaseType = Ease.InOutCirc;return;}
		if(easeType==EaseType.InOutCubic){DOTween.defaultEaseType = Ease.InOutCubic;return;}
		if(easeType==EaseType.InOutCubic){DOTween.defaultEaseType = Ease.InOutElastic;return;}
		if(easeType==EaseType.InOutExpo){DOTween.defaultEaseType = Ease.InOutExpo;return;}
		//if(easeType==EaseType.InOutFlash){DOTween.defaultEaseType = Ease.InOutFlash;return;}
		if(easeType==EaseType.InOutQuad){DOTween.defaultEaseType = Ease.InOutQuad;return;}
		if(easeType==EaseType.InOutQuart){DOTween.defaultEaseType = Ease.InOutQuart;return;}
		if(easeType==EaseType.InOutQuint){DOTween.defaultEaseType = Ease.InOutQuint;return;}
		if(easeType==EaseType.InOutSine){DOTween.defaultEaseType = Ease.InOutSine;return;}
		if(easeType==EaseType.InQuad){DOTween.defaultEaseType = Ease.InQuad;return;}
		if(easeType==EaseType.InQuart){DOTween.defaultEaseType = Ease.InQuart;return;}
		if(easeType==EaseType.InQuart){DOTween.defaultEaseType = Ease.InQuint;return;}
		if(easeType==EaseType.InSine){DOTween.defaultEaseType = Ease.InSine;return;}
		if(easeType==EaseType.Linear){DOTween.defaultEaseType = Ease.Linear;return;}
		if(easeType==EaseType.OutBack){DOTween.defaultEaseType = Ease.OutBack;return;}
		if(easeType==EaseType.OutBounce){DOTween.defaultEaseType = Ease.OutBounce;return;}
		if(easeType==EaseType.OutCirc){DOTween.defaultEaseType = Ease.OutCirc;return;}
		if(easeType==EaseType.OutCubic){DOTween.defaultEaseType = Ease.OutCubic;return;}
		if(easeType==EaseType.OutElastic){DOTween.defaultEaseType = Ease.OutElastic;return;}
		if(easeType==EaseType.OutExpo){DOTween.defaultEaseType = Ease.OutExpo;return;}
		//if(easeType==EaseType.OutFlash){DOTween.defaultEaseType = Ease.OutFlash;return;}
		if(easeType==EaseType.OutQuad){DOTween.defaultEaseType = Ease.OutQuad;return;}
		if(easeType==EaseType.OutQuart){DOTween.defaultEaseType = Ease.OutQuart;return;}
		if(easeType==EaseType.OutQuint){DOTween.defaultEaseType = Ease.OutQuint;return;}
		if(easeType==EaseType.OutSine){DOTween.defaultEaseType = Ease.OutSine;return;}
		
	
	}
	
	public void PositionBackgroundFadePanel()
	{
		if(backgroundFadePanel!=null)
		{
			backgroundFadePanel.SetActive(true);
			backgroundFadePanel.transform.position = rootCanvasRect.position;
			backgroundFadePanel.transform.SetAsLastSibling ();
		}
	}
}
