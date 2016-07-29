using UnityEngine;
using System.Collections;

public class CasillaManager : MonoBehaviour {

	private GameManager gameManager;
	// Use this for initialization
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 touchPos = new Vector2 (wp.x, wp.y);
			if (gameObject.GetComponent<BoxCollider2D> () == Physics2D.OverlapPoint (touchPos)) {
				gameObject.GetComponent<SpriteRenderer> ().color = gameManager.currentPlayerGO.PlayerColor;
				//gameObject.tag = gameManager.currentPlayerGO.PlayerNumber;
				MovementDone();
			}	
		} else if (Input.touchCount == 1) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			Vector2 touchPos = new Vector2 (wp.x, wp.y);
			if (gameObject.GetComponent<BoxCollider2D> () == Physics2D.OverlapPoint (touchPos)) {
				gameObject.GetComponent<SpriteRenderer> ().color = Color.magenta;
 
			}
		}
	}

	void MovementDone() {
		gameManager.SendMessage("CheckGameRules");
		gameManager.SendMessage("DoNextPlayer");
	}
}
