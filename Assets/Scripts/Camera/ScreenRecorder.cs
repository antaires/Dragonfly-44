﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class ScreenRecorder : MonoBehaviour {
	
	//public Camera camera; //render camera to select

	//4k = 3840 x 2160   
	// 1080p = 1920 x 1080
	public int captureWidth = 1920;
	public int captureHeight = 1080; 
	//hide objects during screenshot
	public GameObject hideGameObject; 
	public bool optimizeForManyScreenshots = true;
	//configure with raw, jpg, png, or ppm (simple raw format)
	public enum Format {RAW, JPG, PNG, PPM};
	public Format format = Format.JPG;
	//folder to write output (defaults to data path)
	public string folder; 

	//private vars for screenshot
	private Rect rect;
	private RenderTexture renderTexture;
	private Texture2D screenShot;
	private int counter = 0; //image #

	//commands
	private bool captureScreenshot = false;
	private bool captureVideo = false;

	//create unique filename using one-up variable
	private string uniqueFilename(int width, int height){
		if (folder == null || folder.Length == 0) {
			folder = Application.dataPath;
			if (Application.isEditor) {
				var stringPath = folder + "/.."; 
				folder = Path.GetFullPath (stringPath);
			}
			folder += "/screenshots";

			System.IO.Directory.CreateDirectory (folder);
			string mask = string.Format ("screen_{0}x{1}*.{2}", width, height, format.ToString ().ToLower ());
			counter = Directory.GetFiles (folder, mask, SearchOption.TopDirectoryOnly).Length;
		}
		//use width, height, and counter for unique filename
		var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", folder, width, height, counter, format.ToString().ToLower());
		//up counter
		++counter;
		//return unique filename
		return filename;
	}

	public void CaptureScreenshot(){
		captureScreenshot = true; 
	}

	// Update is called once per frame
	void Update () {
		//check for 'k' for single shot and holding 'v' for continuous shots
		captureScreenshot |= Input.GetKeyDown("k");
        captureVideo = Input.GetKey ("v");
        //captureVideo = true;

		if (captureScreenshot || captureVideo) {
			captureScreenshot = false;
			//hide optional game object if set
			if (hideGameObject != null) hideGameObject.SetActive(false);

			//creates screenshot objects if needed
			if(renderTexture == null){
				//creates off screen render texture that can be rendered onto
				rect = new Rect (0,0, captureWidth, captureHeight);
				renderTexture = new RenderTexture (captureWidth, captureHeight, 24); 
				screenShot = new Texture2D (captureWidth, captureHeight,TextureFormat.RGB24, false);
			}

			//get main camera manually render scene into rt
			Camera camera = this.GetComponent<Camera>();
			camera.targetTexture = renderTexture; 
			camera.Render (); 
		
			//read pixels will read from currently active redner texture so make ours offscreen
			//render texture active and then read the pixels
			RenderTexture.active = renderTexture; 
			screenShot.ReadPixels (rect, 0, 0);

			//reset active camera texture and render textures
			camera.targetTexture = null;
			RenderTexture.active = null;

			//get unique filename
			string filename = uniqueFilename((int) rect.width, (int) rect.height);
            Debug.Log(filename);

			//pull in our file header/data bytes for the specified image for mat (has to be done from main thread)
			byte[] fileHeader = null;
			byte[] fileData = null;
			if (format == Format.RAW) {
				fileData = screenShot.GetRawTextureData ();
			} else if (format == Format.PNG) {
				fileData = screenShot.EncodeToPNG ();
			} else if (format == Format.JPG) {
				fileData = screenShot.EncodeToJPG ();
			} else {
				//create a file header for ppm formatted file
				string headerStr = string.Format("P6\n{0} {1}\n255\n", rect.width, rect.height);
				fileHeader = System.Text.Encoding.ASCII.GetBytes (headerStr);
				fileData = screenShot.GetRawTextureData (); 
			}

			//create new thread to save the image to file (only operation that can be done in the background)
			new System.Threading.Thread (() => {
				//create file and write optional header with image bytes
				var f = System.IO.File.Create (filename);
				if (fileHeader != null)
					f.Write (fileHeader, 0, fileHeader.Length);
				f.Write (fileData, 0, fileData.Length);
				f.Close ();
				Debug.Log (string.Format ("Wrote screenshot {0} of size {1}", filename, fileData.Length));
			}).Start (); 

			//unhide optional game object if set
			if(hideGameObject != null) hideGameObject.SetActive(true); 

			//cleanup if needed
			if (optimizeForManyScreenshots == false){
				Destroy (renderTexture);
				renderTexture = null;
				screenShot = null;
			}
		}
	}
}
