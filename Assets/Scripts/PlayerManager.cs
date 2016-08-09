using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	public bool isIA;
	private bool isPlaying;
	private int playerNumber;
	private Color playerColor;
	private List<List<int[][]>> playerDominatedAreas;

	public PlayerManager (int number, Color color) {
		this.PlayerNumber = number;
		this.PlayerColor = color;
	}

	public bool IsPlaying {
		get {
			return isPlaying;
		}
		set {
			isPlaying = value;
		}
	}

	public int PlayerNumber {
		get {
			return playerNumber;
		}
		set {
			playerNumber = value;
		}
	}

	public Color PlayerColor {
		get {
			return playerColor;
		}
		set {
			playerColor = value;
		}
	}

	// Use this for initialization
	void Start () {
		isPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
