using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	//Time to wait before starting level, in seconds.
	public int turnDelay;
	//Delay between each Player turn.
	public int playerFoodPoints = 100;
	//Starting value for Player food points.
	public static GameManager instance = null;
	//Static instance of GameManager which allows it to be accessed by any other script.
	[HideInInspector] public bool playersTurn = true;
	//Boolean to check if it's players turn, hidden in inspector but public.
	public Color[] colors;
	public int numberOfPlayers;
	public GameObject playerPrefab;
	public Text timer;
	public Image currentPlayerColor;
	public PlayerManager currentPlayerGO;
	
	private Text levelText;
	//Text to display current level number.
	private GameObject levelImage;
	//Image to block out level as levels are being set up, background for levelText.
	private BoardManager boardScript;

	private bool doingSetup;
	private bool doingNextPlayer;
	private List<GameObject> players;
	private int currentPlayer;
	private float totalTimePerTurn = 4f;
	private float timeRemaining;


	//Boolean to check if we're setting up board, prevent Player from moving during setup.


	void Awake() {
		//Check if instance already exists
		if (instance == null)
				
				//if not, set instance to this
				instance = this;
			
			//If instance already exists and it's not this:
			else if (instance != this)
				
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy (gameObject);	
			
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);
			
			
		//Get a component reference to the attached BoardManager script
		boardScript = GetComponent<BoardManager> ();
			
		//Call the InitGame function to initialize the first level 
		InitGame ();
	}

	void Start() {
		currentPlayer = 0;
		turnDelay = 2;
		InitTimer();
	}

	void Update() {
		if (doingSetup == true || doingNextPlayer == true) {
			return;
		}
		UpdateTimer();
		if (timeRemaining <= 0) {
			Debug.Log ("Siguiente Jugador");	
			DoNextPlayer();
		} else {
			if (currentPlayerGO.IsPlaying) {
			}
		}

	}

	void InitGame() {
		//While doingSetup is true the player can't move, prevent player from moving while title card is up.
		doingSetup = true;
			
		//Get a reference to our image LevelImage by finding it by name.
			
		//Call the SetupScene function of the BoardManager script, pass it current level number.
		boardScript.SetupScene ();

		InitPlayers();

		Debug.Log(players[0].GetComponent<PlayerManager>().PlayerNumber);

		doingSetup = false;

			
	}

	void InitPlayers() {
		players = new List<GameObject> ();
		for (int i = 0; i < numberOfPlayers; i++) {
			GameObject player = Instantiate (playerPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			player.name = "Jugador " + (i+1);
			player.GetComponent<PlayerManager>().PlayerColor = colors[i];
			player.GetComponent<PlayerManager>().PlayerNumber = (i+1);
			this.players.Add (player);
			if (i == 0) {
				currentPlayerGO = player.GetComponent<PlayerManager>();
				currentPlayerColor.color = currentPlayerGO.PlayerColor;
			}
		}


	}

	void InitTimer() {
		timeRemaining = totalTimePerTurn;
		timer.text = "Tiempo:" + Mathf.Round (timeRemaining); // TODO

	}

	void UpdateTimer() {
		//if (timeRemaining > 0) {
			timeRemaining -= Time.deltaTime;
			timer.text = "Tiempo:" + Mathf.Round (timeRemaining); // TODO
		//}
	}

	void DoNextPlayer() {
		doingNextPlayer = true;
		StartCoroutine (NextPlayer ());
	}


	IEnumerator NextPlayer() {
		currentPlayer = (currentPlayer + 1) % numberOfPlayers;
		currentPlayerGO = players [currentPlayer].GetComponent<PlayerManager> ();
		currentPlayerColor.color = currentPlayerGO.PlayerColor;
		InitTimer();
		yield return new WaitForSeconds(turnDelay);
		doingNextPlayer = false;
	}

	void CheckGameRules() {
		CheckWinCondition();
	}

	void CheckWinCondition() {

	}
}
