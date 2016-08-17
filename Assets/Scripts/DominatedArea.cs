using UnityEngine;
using System.Collections;

public class DominatedArea : MonoBehaviour {

	private int xLeftCorner, yLeftCorner;
	private int xRightCorner, yRightCorner;

	public DominatedArea(int xLeftCorner, int yLeftCorner, int xRightCorner, int yRightCorner) {
		this.xLeftCorner = xLeftCorner;
		this.yLeftCorner = yLeftCorner;

		this.xRightCorner = xRightCorner;
		this.yRightCorner = yRightCorner;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
