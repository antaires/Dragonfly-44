using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SetHighScore : MonoBehaviour {

    //scores
	public Text highscore1; 
	public Text highscore2;
	public Text highscore3;
	public Text highscore4;
	public Text highscore5;

    //player names
    public Text name1;
    public Text name2;
    public Text name3;
    public Text name4;
    public Text name5;


	// Use this for initialization
	void Start () {

		highscore1.text = PlayerPrefs.GetInt ("Highscore1", 0).ToString ();
		highscore2.text = PlayerPrefs.GetInt ("Highscore2", 0).ToString ();
		highscore3.text = PlayerPrefs.GetInt ("Highscore3", 0).ToString ();
		highscore4.text = PlayerPrefs.GetInt ("Highscore4", 0).ToString ();
		highscore5.text = PlayerPrefs.GetInt ("Highscore5", 0).ToString ();

        name1.text = PlayerPrefs.GetString("name1", "ANT");
        name2.text = PlayerPrefs.GetString("name2", "ANT");
        name3.text = PlayerPrefs.GetString("name3", "ANT");
        name4.text = PlayerPrefs.GetString("name4", "ANT");
        name5.text = PlayerPrefs.GetString("name5", "ANT");
    }

}
