----------------------------------------------
     Text Ext: Text Extensions
            Version 1.3.1
       eternalsoul@foxmail.com
----------------------------------------------

Thank you for buying Text Ext!

This asset extend Unity UI Text component, include:	
	1. hypertext
	2. underline
	3. gradient
	4. font manage
	5. Unity image font generation tool and document

How to use?
- hypertext:
"Please Click [url=http://unity3d.com/cn/]Here[/url] to enter Unity home page".
"As early as 2009 [link_player=1971]<color=#FBAF9F>[u]Elon Musk[/u]</color>[/link_player] indicated..."
you can add you link type to UGUIText_Extend.linkAction(see LinkTest.Start())

- underline:
"here is [u]underline[/u] text".
at Unity 4.6 - 5.1, don't write [u] out of <color=...> (eg. [u]<color=#00507C>Falcon[/u]</color>).

- gradient:
add "UIGradient" to Text, adjust "Top Color" and "Bottom Color".

- font manage:
menu->Text Ext->Font Manager
you can replace target font or missing font in Text.

- Unity image font generation:
use some font image to generate Unity Custom Font and use in Text.
at Unity 4.6 - 5.1, Text use custom font need set Text's material to custom font material.

If you have any questions, suggestions, comments or feature requests, please
send email to eternalsoul@foxmail.com.


-----------------
 Version History
-----------------

1.3.1
- FIX: link area is updated when font size or rectTransform changed.
- FIX: Modify #define to be compatible with the new version after UNITY_5_3(eg.UNITY_5_4).

1.3.0
- NEW: Font Manager support TextMesh.
- FIX: underline and link text doesn't work on mobile devices.
- FIX: text underlining will disappear when save scene or compile project over.

1.2.0
- NEW: UIGradient add "Horizontal" and "Vertical" style, another is "Text Type" option, apply a gradient on text per line.
- NEW: Add "underlineOffsetY" and "underlineHeightScale" to adjust underline position and thickness.
- FIX: underline position error in Canvas Scaler->UI Scale Model->Scale With Screen Size.

1.1.0
- FIX: Hypertext with "Content Size Fitter" component click area is not precise.

