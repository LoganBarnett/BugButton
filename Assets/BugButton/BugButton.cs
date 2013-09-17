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
	public Texture[] progressImages;
	public Vector2 screenLocation;
	public GUISkin bugSkin;
	
	Rect iconRect;
	GUIContent iconContent;
	
	public string windowTitleText = "Report a Bug";
	public string windowInstructionsText = "Tell us what happened.";
	public string windowSubmittingText = "Submitting...";
	public string windowSubmittedText = "Submitted! Thanks for your help!";
	public string windowSubmitButtonText = "Submit";
	public string windowCancelButtonText = "Cancel";
	int windowId = 23987395; // if you have an ID that matches this, change it to something else.
	string windowDescriptionText = "";
	
	Rect windowRect;
//	Rect windowTitleRect;
	Rect windowInstructionsRect;
	Rect windowDescriptionRect;
	Rect windowSubmitButtonRect;
	Rect windowCancelButtonRect;
	Rect windowProgressRect;
	
//	GUIStyle titleStyle;
	GUIStyle instructionsStyle;
	GUIStyle descriptionStyle;
	
	bool bugReportOpen = false;
	bool submitted = false;
	bool submitting = false;
	bool configured = false;
	
	// TODO: Automatically reinvoke when screen resolution changes
	public void ConfigureRects() {
//		var labelHeight = skin.label.CalcHeight(new GUIContent(" "), 10f);
		
		iconRect = new Rect(screenLocation.x, screenLocation.y, bugIcon.width, bugIcon.height);
		iconContent = new GUIContent(bugIcon, windowTitleText);
		
		windowRect = new Rect(10f, 10f, Screen.width - 20f, Screen.height - 20f);
		var currentHeight = (float)bugSkin.window.padding.top;
		
//		titleStyle = bugSkin.GetStyle("BugReportTitle");
//		var titleSize = titleStyle.CalcSize(new GUIContent(windowTitleText));
//		windowTitleRect = new Rect(windowRect.width / 2f, currentHeight + (titleSize.y * 2f), titleSize.x, titleSize.y);
//		currentHeight += windowTitleRect.y + windowTitleRect.height;
		
		// TODO: Use the same method that description does to get its style
		instructionsStyle = bugSkin.GetStyle("BugReportInstructions");
		var instructionSize = instructionsStyle.CalcSize(new GUIContent(windowInstructionsText));
		// not sure what text alignment does if I have to position it as if it were the top-left corner
		var instructionX = (windowRect.width / 2f) - (instructionSize.x / 2f);
		windowInstructionsRect = new Rect(instructionX, currentHeight, instructionSize.x, instructionSize.y);
		currentHeight += windowInstructionsRect.y + windowInstructionsRect.height;
		
		// TODO: submit button style like done for the description below
		var submitButtonSize = bugSkin.button.CalcSize(new GUIContent(windowSubmitButtonText));
		var bottomRowHeight = windowRect.height - ((float)bugSkin.window.padding.bottom + submitButtonSize.y);
		var submitX = (windowRect.width / 2f) - submitButtonSize.x;
		windowSubmitButtonRect = new Rect(submitX, bottomRowHeight, submitButtonSize.x, submitButtonSize.y);
		
		// TODO: submit button style like done for the description below
		var cancelButtonSize = bugSkin.button.CalcSize(new GUIContent(windowCancelButtonText));
		var cancelX = (windowRect.width / 2f);
		windowCancelButtonRect = new Rect(cancelX, bottomRowHeight, cancelButtonSize.x, cancelButtonSize.y);
		
		// do the description last, since it occupies remaining space
		descriptionStyle = bugSkin.GetStyle("BugReportDescription");
		if(string.IsNullOrEmpty(descriptionStyle.name)) descriptionStyle = bugSkin.textArea;
		windowDescriptionRect = new Rect(10f, currentHeight, windowRect.width - 20f, bottomRowHeight - currentHeight);
		configured = true;
	}
	
	void Start() {
	}
	
	void OnGUI() {
		if(bugSkin == null) bugSkin = GUI.skin;
		var originalSkin = GUI.skin;
		
		if(!configured) ConfigureRects();
		
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
//		GUI.Label(windowTitleRect, windowTitleText, titleStyle);
		GUI.Label(windowInstructionsRect, windowInstructionsText, instructionsStyle);
		windowDescriptionText = GUI.TextArea(windowDescriptionRect, windowDescriptionText, descriptionStyle);
		if(submitted) {
//			GUI.Label(currentStatusRect, currentStatusText, currentStatusStyle);
		}
		else {
			if(GUI.Button(windowSubmitButtonRect, windowSubmitButtonText)) {
				Debug.Log("Submit clicked!");
			}
		}
		// close
		if(GUI.Button(windowCancelButtonRect, windowCancelButtonText)) {
			Debug.Log("Cancel clicked!");
			bugReportOpen = false;
		}
	}
	
	void CloseWindow() {
		submitted = false;
		submitting = false;
		bugReportOpen = false;
	}
}