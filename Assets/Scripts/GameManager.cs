using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	public int turnNumber;
	public int turnDelay;
	public static GameManager instance = null;
	public  Color[] colors;
	public Color baseColor;
	public static int numberOfPlayers;
	public GameObject playerPrefab;
	public Text timer;
	public Image currentPlayerColor;
	public PlayerManager currentPlayerGO;
	
	private Text levelText;
	private GameObject levelImage;
	private BoardManager boardScript;

	private bool doingSetup;
	private bool doingNextPlayer;
	private List<GameObject> players;
	public List<List<int>> boardStatus;
	private int currentPlayer;
	private float totalTimePerTurn = 30f;
	private float timeRemaining;
	private int squaresToWin = 5;
	private int squaresToDominateArea = 3; 

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
		//DontDestroyOnLoad (gameObject);
	}

	void Start() {
		numberOfPlayers = 2;
		currentPlayer = 0;
		turnDelay = 2;
		turnNumber = 0;
		boardScript = GetComponent<BoardManager> ();
		//Call the InitGame function to initialize the first level 
		InitGame ();
		//InitTimer();
	}

	void Update() {
		if (doingSetup == true || doingNextPlayer == true) {
			return;
		}
		UpdateTimer();
		if (timeRemaining <= 0) {
			turnNumber++;
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

		InitBoardStatus();

		InitTimer();

		//Debug.Log(players[0].GetComponent<PlayerManager>().PlayerNumber);

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

	void InitBoardStatus() {
		boardStatus = new List<List<int>>();
		for (int i = 0; i < BoardManager.rows; i++) {
			boardStatus.Add(new List<int>());
			for (int j = 0; j < BoardManager.columns; j++) {
				boardStatus[i].Add(0);
			}
		}
	}

	void UpdateTimer() {
		if (timeRemaining > 0) {
			timeRemaining -= Time.deltaTime;
			timer.text = "Tiempo:" + Mathf.Round (timeRemaining); // TODO
		}
	}

	void UpdateBoardStatus(GameObject square) {
		boardStatus[square.GetComponent<CasillaManager>().XPosition][square.GetComponent<CasillaManager>().YPosition] = currentPlayerGO.PlayerNumber;
	}

	void MovementDone(GameObject square) {
		UpdateBoardStatus (square);
		if (CheckWinRules ()) {
			gameObject.GetComponent<LevelManager> ().LoadLevel ("Victoria");		
		} else {
			if (CheckLooseRules ()) {
				gameObject.GetComponent<LevelManager> ().LoadLevel ("Derrota");		
			} else {
				CheckCoolRules ();
				turnNumber++;
				DoNextPlayer ();
			}
		}
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

	bool CheckWinRules() {
		return CheckWinCondition();
	}

	bool CheckWinCondition() {
		bool columnsWin, rowsWin, diagonalWin;
		columnsWin = rowsWin = diagonalWin = false;
		columnsWin = CheckColumns();
		rowsWin = CheckRows();
		diagonalWin = CheckDiagonals();

		return (columnsWin || rowsWin || diagonalWin);
	}

	bool CheckColumns() {
		int playerSquaresCount = 0;
		for (int j = 0; j < BoardManager.columns; j++) {
			for (int i = 0; i < BoardManager.rows; i++) {
				if (boardStatus [i] [j] == currentPlayerGO.PlayerNumber) {
					playerSquaresCount++;
					if (playerSquaresCount >= squaresToWin) {
						return true;
					}
					if (playerSquaresCount == squaresToDominateArea) {
						DominatedArea dominatedArea = CalculateDominatedAreaCorner(j, i, "columns");
						currentPlayerGO.GetComponent<PlayerManager>().playerDominatedAreas.Add(dominatedArea);
					}

				} else {
					playerSquaresCount = 0;
				}
			}
		}
		return false;
	}

	bool CheckRows() {
		int playerSquaresCount = 0;
		for (int i = 0; i < BoardManager.rows; i++) {
			for (int j = 0; j < BoardManager.columns; j++) {
				if (boardStatus [i] [j] == currentPlayerGO.PlayerNumber) {
					playerSquaresCount++;
					if (playerSquaresCount >= squaresToWin) {
						return true;
					}
					if (playerSquaresCount == squaresToDominateArea) {
						DominatedArea dominatedArea = CalculateDominatedAreaCorner(i, j, "rows");
						currentPlayerGO.GetComponent<PlayerManager>().playerDominatedAreas.Add(dominatedArea);
					}
				} else {
					playerSquaresCount = 0;
				}
			}
		}

		return false;
	}

	bool CheckDiagonals() {
		return CheckRDiagonal() || CheckLDiagonal();
	}

	bool CheckRDiagonal() {
		int playerSquaresCount = 0;
		for (int i = 0; i < BoardManager.rows; i++) {
			for (int j = 0; j < BoardManager.columns; j++) {
				if (i == j) {
					if (boardStatus [i] [j] == currentPlayerGO.PlayerNumber) {
						playerSquaresCount++;
						if (playerSquaresCount >= squaresToWin) {
							return true;
						}
					} else {
						playerSquaresCount = 0;
					}
				}
			}
		}
		return false;
	}

	bool CheckLDiagonal() {
		int playerSquaresCount = 0;
		for (int i = (BoardManager.rows -1); i > 0; i--) {
			for (int j = (BoardManager.columns -1); j > 0; j--) {
				if (i == j) {
					if (boardStatus [i] [j] == currentPlayerGO.PlayerNumber) {
						playerSquaresCount++;
						if (playerSquaresCount >= squaresToWin) {
							return true;
						}
					} else {
						playerSquaresCount = 0;
					}
				}
			}
		}
		return false;
	}

	public int getBoardStatusPosition(int x, int y) {
		
		return boardStatus[x][y];
	}

	bool CheckLooseRules() {
		bool loose = true;
		for (int i = 0; i < BoardManager.rows; i++) {
			for (int j = 0; j < BoardManager.columns; j++) {
				if (boardStatus [i] [j] == 0) {
					loose = false;
				}
			}
		}

		return loose;
	}

	DominatedArea CalculateDominatedAreaCorner(int i, int j, string searchMode) {
		int xLeftCorner, yLeftCorner, xRightCorner, yRightCorner;
		xLeftCorner = yLeftCorner = xRightCorner = yRightCorner = 0;
		switch (searchMode) {
			case "rows":
				xLeftCorner = i - (squaresToDominateArea - 2);
				yLeftCorner = j - (squaresToDominateArea - 1);

				xRightCorner = i + (squaresToDominateArea - 2);
				yRightCorner = j;
				break;
			case "columns":
				xLeftCorner = i - (squaresToDominateArea - 2);
				yLeftCorner = j - (squaresToDominateArea - 1);

				xRightCorner = i + (squaresToDominateArea - 2);
				yRightCorner = j;
				break;
			case "rDiagonal":
				xLeftCorner = i - (squaresToDominateArea - 2);
				yLeftCorner = j - (squaresToDominateArea - 1);

				xRightCorner = i + (squaresToDominateArea - 2);
				yRightCorner = j;
				break;
			case "lDiagonal":
				xLeftCorner = i - (squaresToDominateArea - 2);
				yLeftCorner = j - (squaresToDominateArea - 1);

				xRightCorner = i + (squaresToDominateArea - 2);
				yRightCorner = j;
				break;
		}

		if (xLeftCorner < 0) {
			xLeftCorner = 0;
		}

		if (xRightCorner >= BoardManager.columns) {
			xRightCorner = BoardManager.columns - 1;
		}

		DominatedArea dominatedArea = new DominatedArea(xLeftCorner, yLeftCorner, xRightCorner, yRightCorner);

		return dominatedArea;
	}

	void DrawDominatedArea(DominatedArea dominatedArea) {

	}
		
	



}
