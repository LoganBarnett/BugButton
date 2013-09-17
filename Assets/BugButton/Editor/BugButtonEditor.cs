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
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BugButton))]
public class BugButtonEditor : Editor {
	void OnEnable() {
		var bugButton = (BugButton)target;
		if(bugButton.bugIcon == null) {
			bugButton.bugIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/BugButton/BugIcon.psd", typeof(Texture));
		}
		
		if(bugButton.progressImages == null || bugButton.progressImages.Length == 0) {
			var progress1 = (Texture)AssetDatabase.LoadAssetAtPath("Assets/BugButton/Progress1.psd", typeof(Texture));
			var progress2 = (Texture)AssetDatabase.LoadAssetAtPath("Assets/BugButton/Progress2.psd", typeof(Texture));
			var progress3 = (Texture)AssetDatabase.LoadAssetAtPath("Assets/BugButton/Progress3.psd", typeof(Texture));
			var progress4 = (Texture)AssetDatabase.LoadAssetAtPath("Assets/BugButton/Progress4.psd", typeof(Texture));
			
			var progressTextures = new [] {progress1, progress2, progress3, progress4};
			
			bugButton.progressImages = progressTextures;
		}
		
		if(bugButton.bugSkin == null) {
			var skin = (GUISkin)AssetDatabase.LoadAssetAtPath("Assets/BugButton/BugSkin.guiskin", typeof(GUISkin));
			bugButton.bugSkin = skin;
		}
	}
}