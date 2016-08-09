using UnityEngine;
using System.Collections;

public class CasillaManager : MonoBehaviour {

	private GameManager gameManager;
	public int xPosition;
	public int yPosition;
	public bool isDominated;

	public int XPosition {
		get {
			return xPosition;
		}
		set {
			xPosition = value;
		}
	}

	public int YPosition {
		get {
			return yPosition;
		}
		set {
			yPosition = value;
		}
	}

	// Use this for initialization
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		isDominated = false;
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 touchPos = new Vector2 (wp.x, wp.y);
			if (gameObject.GetComponent<BoxCollider2D> () == Physics2D.OverlapPoint (touchPos)) {
				SquareTouched();
			}	
		} else if (Input.touchCount == 1) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			Vector2 touchPos = new Vector2 (wp.x, wp.y);
			if (gameObject.GetComponent<BoxCollider2D> () == Physics2D.OverlapPoint (touchPos)) {
				SquareTouched();
			}
		}
	}

	void SquareTouched() {
		int squareStatus = gameManager.getBoardStatusPosition (gameObject.GetComponent<CasillaManager> ().XPosition, gameObject.GetComponent<CasillaManager> ().YPosition);
		if (squareStatus == 0) {
			gameObject.GetComponent<SpriteRenderer> ().color = gameManager.currentPlayerGO.PlayerColor;
			MovementDone ();
		}
	}

	void MovementDone() {
		gameManager.SendMessage("MovementDone", gameObject);
	}
}
