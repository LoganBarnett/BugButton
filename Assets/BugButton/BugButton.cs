/*
The MIT License (MIT)

Copyright (c) 2013 Logan Barnett

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;

public class BugButton : MonoBehaviour {
	public Texture bugIcon;
	public Vector2 screenLocation;
	public GUISkin bugSkin;
	
	Rect iconRect;
	GUIContent iconContent;
	
	public string windowTitleText = "Report a Bug";
	public string windowInstructionsText = "Tell us what happened.";
	int windowId = 23987395; // if you have an ID that matches this, change it to something else.
	Rect windowRect;
	Rect windowTitleRect;
	Rect windowInstructionsRect;
	GUIStyle titleStyle;
	GUIStyle instructionsStyle;
	
	bool bugReportOpen = false;
	bool submitted = false;
	bool submitting = false;
	
	// TODO: Automatically reinvoke when screen resolution changes
	public void ConfigureRects() {
		var skin = bugSkin ?? GUI.skin;
//		var labelHeight = skin.label.CalcHeight(new GUIContent(" "), 10f);
		
		iconRect = new Rect(screenLocation.x, screenLocation.y, bugIcon.width, bugIcon.height);
		iconContent = new GUIContent(bugIcon, "Report a bug");
		
		windowRect = new Rect(10f, 10f, Screen.width - 20f, Screen.height - 20f);
		var currentHeight = 0f;
		
		titleStyle = skin.GetStyle("BugReportTitle");
		var titleSize = titleStyle.CalcSize(new GUIContent(windowTitleText));
		windowTitleRect = new Rect(windowRect.width / 2f, currentHeight + (titleSize.y * 2f), titleSize.x, titleSize.y);
		currentHeight += windowTitleRect.y + windowTitleRect.height;
		
		instructionsStyle = skin.GetStyle("BugReportInstructions");
		var instructionSize = instructionsStyle.CalcSize(new GUIContent(windowInstructionsText));
		windowInstructionsRect = new Rect(windowRect.width / 2f, currentHeight, instructionSize.y, instructionSize.y);
		currentHeight += windowInstructionsRect.y + windowInstructionsRect.height;
	}
	
	void Start() {
		ConfigureRects();
	}
	
	void OnGUI() {
		var originalSkin = GUI.skin;
		if(bugSkin != null) GUI.skin = bugSkin;
		
		try {
			if(bugReportOpen) {
				GUI.Window(windowId, windowRect, DrawBugReportWindow, windowTitleText);
			}
			else {
				if(GUI.Button(iconRect, iconContent)) {
					bugReportOpen = true;
				}
			}
		}
		finally {
			GUI.skin = originalSkin;
		}
	}
	
	void DrawBugReportWindow(int unusedWindowId) {
		GUI.Label(windowTitleRect, windowTitleText, titleStyle);
		GUI.Label(windowInstructionsRect, windowInstructionsText, instructionsStyle);
//		GUI.TextArea(windowDescriptionRect, windowDescriptionText, descriptionStyle);
		if(submitted) {
//			GUI.Label(currentStatusRect, currentStatusText, currentStatusStyle);
		}
		else {
			// submit
//			GUI.Button();
		}
		// close
//		GUI.Button();
	}
	
	void CloseWindow() {
		submitted = false;
		submitting = false;
		bugReportOpen = false;
	}
}