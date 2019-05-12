using UnityEngine;
using System.Collections;

public class gameControllerInput : MonoBehaviour {

	[SerializeField] gameMaster game;

	void Start() {

	}

	void Update() {
		// Player 1 Keybboar Input
		if(Input.GetKeyDown(KeyCode.A)) {
			game.HandleInput(10);
		}
		if(Input.GetKeyDown(KeyCode.S)) {
			game.HandleInput(11);
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			game.HandleInput(12);
		}
		if(Input.GetKeyDown(KeyCode.F)) {
			game.HandleInput(13);
		}

		// Player 2 Keyboard Input
		if(Input.GetKeyDown(KeyCode.J)) {
			game.HandleInput(20);
		}
		if(Input.GetKeyDown(KeyCode.K)) {
			game.HandleInput(21);
		}
		if(Input.GetKeyDown(KeyCode.L)) {
			game.HandleInput(22);
		}
		if(Input.GetKeyDown(KeyCode.Semicolon)) {
			game.HandleInput(23);
		}
		
		// Player 1 Dpad Input
		if(Input.GetButtonDown("P1StartButton")) {
			game.HandleInput(10);
		}
		if(Input.GetButtonDown("P1AButton")) {
			game.HandleInput(13);
		}
		if(Input.GetButtonDown("P1XButton")) {
			game.HandleInput(12);
		}
		if(Input.GetButtonDown("P1YButton")) {
			game.HandleInput(11);
		}
		if(Input.GetButtonDown("P1BButton")) {
			game.HandleInput(10);
		}
		
		// Player 2 Dpad Input
		if(Input.GetButtonDown("P2AButton")) {
			game.HandleInput(23);
		}
		if(Input.GetButtonDown("P2XButton")) {
			game.HandleInput(22);
		}
		if(Input.GetButtonDown("P2YButton")) {
			game.HandleInput(21);
		}
		if(Input.GetButtonDown("P2BButton")) {
			game.HandleInput(20);
		}
	}
}
