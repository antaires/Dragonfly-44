using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioRecorder : MonoBehaviour {

    public string audioName = "audioRecording_01";

	// Use this for initialization
	void Start () {

        AudioSource aud = GetComponent<AudioSource>();
        aud.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
        aud.Play();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.O)) {
            Debug.Log("O key pressed");
        }


    }

    //void SaveRecording(AudioSource aud)
    //{
     //   SavWav.Save(Application.persistantDataPath + "/Resources/" + aud.clip.name, aud.clip);
    //}



}
