using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameMaster : MonoBehaviour

{
    [Header("Rules")]
    [SerializeField] int MaxStanceChangesAllowed = -1;
    [SerializeField] bool ShowFinalStance = true;
    [SerializeField] int PointsNeededToWin = 3;
    [SerializeField] bool TiesResultInPoints = false;
    [SerializeField] bool CanWinFromTie = true;

    [Header("Round/Match")]
    [SerializeField] bool AutomaticallyStartFirstRound = true;
    [SerializeField] bool AutomaticallyRestartMatch = false;
	
	[Header("Players")]
	[SerializeField] gamePlayerCharacter player1;
    [SerializeField] gamePlayerCharacter player2;

    [Header("UI")]
	[SerializeField] Image HealthBar;
	[SerializeField] TextMeshProUGUI PlayerOneName;
	[SerializeField] TextMeshProUGUI PlayerTwoName;
	[SerializeField] TextMeshProUGUI scoreboard1;
	[SerializeField] TextMeshProUGUI scoreboard2;
	[SerializeField] TextMeshProUGUI TextBoard;
	[SerializeField] GameObject PauseMenu;
	[SerializeField] GameObject GameOverMenu;

	[Header ("Time")]
	[SerializeField] TextMeshProUGUI TimeLimit;
	[SerializeField] float SecondsToPrepare = 0;
	[SerializeField] float PreMatchSeconds = 0;
	[SerializeField] float CooldownSeconds = 0;
	[SerializeField] float WindupSeconds = 0;
	[SerializeField] TextMeshProUGUI TimeText;
	
	[Header ("Countdown Bar")]
	[SerializeField] Image TimeBoard;
	[SerializeField] Color MaxDamageColor;
	[SerializeField] Color MidDamageColor;
	
	[Header ("Attack Modifiers")]
	[SerializeField] float MaxAttackLate = 0;
	[SerializeField] int MaxAttack = 0;
	[SerializeField] float MidAttackLate = 0;
	[SerializeField] int MidAttack = 0;
	
	[Header ("Animation")]
	public Animator animator;
	public Animator animator2;

	// Variables
	float timeAmt = 0;
	float secondsprematch = 0;
	float secondsremaining = 0;
	float secondscooldown = 0;
	float secondswindup = 0;
	float HealthAmt = 0;
	
	int player1stance = 0;
	int player2stance = 0;

	int player1score = 0;
	int player2score = 0;

	int player1stancesleft = 0;
	int player2stancesleft = 0;
	
	int P1MaxAttackOutput = 0;
	int P1MidAttackOutput = 0;
	
	int P2MaxAttackOutput = 0;
	int P2MidAttackOutput = 0;

	int currentround = -1;


	// Game States
	enum GameStates{
		NotStarted, CountDown, PreMatch, Windup, Result, CoolDown, GameOver};

	GameStates gamestate = GameStates.NotStarted;

	
	// Start
	void Start () {
		FindObjectOfType<gameAudioManager>().Play("GonnaSmackYou");
		
		if (AutomaticallyStartFirstRound) {
			gamestate = GameStates.PreMatch;
			secondsprematch = PreMatchSeconds;
			TimeBoard.fillAmount = 0;
			HealthBar.fillAmount = .5f;
			HealthAmt = PointsNeededToWin * 2;
			PlayerOneName.text = player1.CharacterName;
			PlayerTwoName.text = player2.CharacterName;
		}
	}


	// Update
	void Update() {
		if (gamestate == GameStates.PreMatch) {
			if (secondsprematch > 0) {
				secondsprematch -= Time.deltaTime;
			}
			else {
				NextRound();
				timeAmt = SecondsToPrepare;
			}
		}
		
		if (gamestate == GameStates.CountDown) {
			if (secondsremaining > 0) {
				secondsremaining -= Time.deltaTime;
				TimeBoard.fillAmount = secondsremaining / timeAmt;
				TimeText.text = secondsremaining.ToString ("F2");
			}
			else if (secondsremaining <= 0) {
				TimeText.text = "";
				if (ShowFinalStance) {
					player1.FinalStance ();
					player2.FinalStance ();
				}
				
				gamestate = GameStates.Windup;
				secondswindup = WindupSeconds;
			}
		}
		
		if (gamestate == GameStates.Windup) {
			if (secondswindup > 0) {
				secondswindup -= Time.deltaTime;
			}
			else {
				DeterminePoint ();
				gamestate = GameStates.Result;
				secondscooldown = CooldownSeconds; 
			}
		}

		if (gamestate == GameStates.Result) {
			if (secondscooldown > 0) {
				secondscooldown -= Time.deltaTime;
			}
			else{
				NextRound();
				timeAmt = SecondsToPrepare;
			}
		}
		
		if (TimeBoard.fillAmount <= 1 && TimeBoard.fillAmount > MaxAttackLate) {TimeBoard.color = MaxDamageColor;}
		else if (TimeBoard.fillAmount < MaxAttackLate && TimeBoard.fillAmount > MidAttackLate) {TimeBoard.color = MidDamageColor;}
		else {TimeBoard.color = Color.white;}
	}


	// New Match
	public void NewMatch() {
		GameOverMenu.SetActive (false);
		
		player1score = 0;
		player2score = 0;

		scoreboard1.text = "0";
		scoreboard2.text = "0";
		
		currentround = -1;
		HealthBar.fillAmount = .5f;
		gamestate = GameStates.PreMatch;
		secondsprematch = PreMatchSeconds;
		
		player1stance = 0;
		player1.SetStance(player1stance);
		player2stance = 0;
		player2.SetStance(player2stance);
		TextBoard.text = "";
	}


	// Next Round
	void NextRound() {
		if(player1score >= PointsNeededToWin && player2score >= PointsNeededToWin) {
			gamestate = GameStates.GameOver;

			MatchDraw();

		} else if(player1score >= PointsNeededToWin) {
			gamestate = GameStates.GameOver;

			Player1Win();
			

		} else if(player2score >= PointsNeededToWin) {
			gamestate = GameStates.GameOver;

			Player2Win();

		} else {
			TextBoard.text = "CHOOSE YOUR ATTACK";
			
			currentround += 1;
			
			P1MaxAttackOutput = 0;
			P1MidAttackOutput = 0;
			
			P2MaxAttackOutput = 0;
			P2MidAttackOutput = 0;

			secondsremaining = SecondsToPrepare;

			player1stance = 0;
			player1.SetStance(player1stance);
			player2stance = 0;
			player2.SetStance(player2stance);

			player1stancesleft = MaxStanceChangesAllowed;
			player2stancesleft = MaxStanceChangesAllowed;

			gamestate = GameStates.CountDown;
		}
	}


	// Determine Score
	void DeterminePoint() {
		int winner = 0;
		

		if(player1stance == player2stance) {
			winner = 0;
			FindObjectOfType<gameAudioManager>().Play("WhiffedAttack");
		
		} else if(player1stance == 0) {
			winner = 2;
			FindObjectOfType<gameAudioManager>().Play("SuccesfulHit");
			FindObjectOfType<gameAudioManager>().Play("Grunt01");
			AnimationHit2();
		} else if(player2stance == 0) {
			winner = 1;
			FindObjectOfType<gameAudioManager>().Play("SuccesfulKick");
			FindObjectOfType<gameAudioManager>().Play("Grunt02");
			AnimationHit();

		} else if(player1stance == 1 && player2stance == 2) {
			winner = 1;
			FindObjectOfType<gameAudioManager>().Play("SuccesfulHit");
			FindObjectOfType<gameAudioManager>().Play("Grunt02");
			AnimationHit();
		} else if(player1stance == 2 && player2stance == 3) {
			winner = 1;
			FindObjectOfType<gameAudioManager>().Play("SuccesfulHit");
			FindObjectOfType<gameAudioManager>().Play("Grunt02");
			AnimationHit();
		} else if(player1stance == 3 && player2stance == 1) {
			winner = 1;
			FindObjectOfType<gameAudioManager>().Play("SuccesfulKick");
			FindObjectOfType<gameAudioManager>().Play("Grunt02");
			AnimationHit();

		} else if(player2stance == 1 && player1stance == 2) {
			winner = 2;
			FindObjectOfType<gameAudioManager>().Play("SuccesfulHit");
			FindObjectOfType<gameAudioManager>().Play("Grunt01");
			AnimationHit2();
		} else if(player2stance == 2 && player1stance == 3) {
			winner = 2;
			FindObjectOfType<gameAudioManager>().Play("SuccesfulHit");
			FindObjectOfType<gameAudioManager>().Play("Grunt01");
			AnimationHit2();
		} else if(player2stance == 3 && player1stance == 1) {
			winner = 2;
			FindObjectOfType<gameAudioManager>().Play("SuccesfulKick");
			FindObjectOfType<gameAudioManager>().Play("Grunt01");
			AnimationHit2();
		}

		Debug.Log("A winner was found! " + winner);
		switch(winner) {
			case 0:
				if(TiesResultInPoints && !(CanWinFromTie == false && (player1score + 1 == PointsNeededToWin || player2score + 1 == PointsNeededToWin))) {
					player1score += 1;
					player2score += 1;
					
					Debug.Log("Both players got a point!");
					TextBoard.text = "";
				} else {
					Debug.Log("Neither player got the point!");
					TextBoard.text = "";
				}
				break;
			case 1:
				Debug.Log("Player 1 got the point!");
				TextBoard.SetText("");
				
				player1score += 1 + P1MaxAttackOutput + P1MidAttackOutput;
				player2score -= 1 + P1MaxAttackOutput + P1MidAttackOutput;
				player2.Results();
				HealthBar.fillAmount += (1 + P1MaxAttackOutput + P1MidAttackOutput) / HealthAmt;
				
				break;
			case 2:
				Debug.Log("Player 2 got the point!");
				TextBoard.SetText("");
				
				player2score += 1 + P2MaxAttackOutput + P2MidAttackOutput;
				player1score -= 1 + P2MaxAttackOutput + P2MidAttackOutput;
				player1.Results();
				HealthBar.fillAmount -= (1 + P2MaxAttackOutput + P2MidAttackOutput) / HealthAmt;
				
				break;
		}

		scoreboard1.text = player1score.ToString();
		scoreboard2.text = player2score.ToString();
	}



	// Winner
	void Player1Win() {
		Debug.Log("PLAYER 1 WON THE ENTIRE MATCH!");
		TextBoard.SetText("Player 1 WINS!");
		GameOverMenu.SetActive (true);
	}

	void Player2Win() {
		Debug.Log("PLAYER 2 WON THE ENTIRE MATCH!");
		TextBoard.SetText("Player 2 WINS!");
		GameOverMenu.SetActive (true);
	}

	void MatchDraw() {
		Debug.Log("THE MATCH RESULTED IN A DRAW!");
		TextBoard.SetText("TIE GAME!");
		GameOverMenu.SetActive (true);
	}


	// Input
	public void HandleInput(int input) {
		Debug.Log("Input was given: " + input);
		
		if(gamestate == GameStates.CountDown) {
			switch(input) {
			case 10:
			case 11:
			case 12:
			case 13:
				if(player1stancesleft != 0) {
					player1stance = input - 10;
					player1.SetStance(player1stance);
					
					player1stancesleft -= 1;
					
					if (TimeBoard.fillAmount <= 1 && TimeBoard.fillAmount > MaxAttackLate) { P1MaxAttackOutput = MaxAttack; }
					else if (TimeBoard.fillAmount < MaxAttackLate && TimeBoard.fillAmount > MidAttackLate) { P1MidAttackOutput = MidAttack; }
					else { P1MaxAttackOutput = 0; P1MidAttackOutput = 0;}
				}
				break;

			case 20:
			case 21:
			case 22:
			case 23:
				if(player2stancesleft != 0) {
					player2stance = input - 20;
					player2.SetStance(player2stance);

					player2stancesleft -= 1;
					
					if (TimeBoard.fillAmount <= 1 && TimeBoard.fillAmount > MaxAttackLate) { P2MaxAttackOutput = MaxAttack; }
					else if (TimeBoard.fillAmount < MaxAttackLate && TimeBoard.fillAmount > MidAttackLate) { P2MidAttackOutput = MidAttack; }
					else { P2MaxAttackOutput = 0; P2MidAttackOutput = 0;}
				}
				break;
			}
		}
		
		if(gamestate == GameStates.PreMatch || gamestate == GameStates.CountDown || gamestate == GameStates.Result || gamestate == GameStates.CoolDown){
			switch(input){
			case 10:
			if(Input.GetButtonDown("P1StartButton")) 
			{
				if (!PauseMenu.activeInHierarchy) 
				{
					PauseGame();
				}
				if (PauseMenu.activeInHierarchy) 
				{
					ResumeGame();   
				}
			}
			break;
			}
		}
		
	else if(gamestate == GameStates.Result) {
			Debug.Log("CURRENT SCORE: " + player1score + " to " + player2score);
		}
	}
	
	//Animations
	public void AnimationHit()
	{
		animator.SetTrigger("Effect");
	}
	
	public void AnimationHit2()
	{
		animator2.SetTrigger("Effect");
	}
	
	//Menu Functions
	void PauseGame()
	{
		Time.timeScale = 0;
		PauseMenu.SetActive (true);
	}
	
	void ResumeGame()
	{
		Time.timeScale = 1;
		PauseMenu.SetActive (false);
	}
}
