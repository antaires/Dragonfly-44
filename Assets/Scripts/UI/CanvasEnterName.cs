using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasEnterName : MonoBehaviour {

    public GameObject menuScreen;
    public GameObject enterNameScreen;


    //public InputField nameInput;
    public InputField inputfield;
    public Text text;

    private bool nameEntered = false;

	// Use this for initialization
	void Start () {

        enterNameScreen.SetActive(false);

        //Debug.Log("currentName is ");
        //Debug.Log(PlayerPrefs.GetString("currentName"));

    }
	
	// Update is called once per frame
	void Update () {

        if (PlayerPrefs.GetString("currentName") != "none") {

            //disable menu screen
            menuScreen.SetActive(false);
            //enable enterNameScreen
            enterNameScreen.SetActive(true);

            //change this to button?
            //when enter key is pressed, store value from input field as new name
            if (Input.GetKey(KeyCode.Return)) {
                //get name from input field
                string newNamefull = text.text;
                //short name to 3 char
                string newName = newNamefull.Substring(0, 3);
                //save name to change
                string nameToChange = PlayerPrefs.GetString("currentName");
                PlayerPrefs.SetString(nameToChange, newName);
                //Debug.Log("Set name as new playerPref");
                //Debug.Log(PlayerPrefs.GetString("currentName"));
                PlayerPrefs.Save();
                nameEntered = true;
            }

            if (nameEntered == true) {
                //disable enterNamescreen
                enterNameScreen.SetActive(false);

                //enable menuScreen
                menuScreen.SetActive(true);
            }


        }


    }
}
