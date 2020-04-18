using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//game states
public enum GameState {INTRO, MAIN_MENU, PLAY, GAME_OVER}
public delegate void OnStateChangeHandler(); 

public class SimpleGameManager : MonoBehaviour {

	protected SimpleGameManager(){}
	private static SimpleGameManager instance = null;
	public event OnStateChangeHandler OnStateChange;
	public GameState gameState { get; private set; }

	public static SimpleGameManager Instance{
		get { 
			if (SimpleGameManager.instance == null) {
				DontDestroyOnLoad (SimpleGameManager.instance); 
				SimpleGameManager.instance = new SimpleGameManager ();
			}
			return SimpleGameManager.instance; 
		}
	}

	public void SetGameState(GameState state){
		this.gameState = state;
		OnStateChange (); 
	}

	public void OnApplicationQuit(){
		SimpleGameManager.instance = null; 
	}

}


