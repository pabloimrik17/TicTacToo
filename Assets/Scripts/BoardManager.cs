﻿using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class BoardManager : MonoBehaviour {

	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count {
		public int minimum;
		//Minimum value for our Count class.
		public int maximum;
		//Maximum value for our Count class.
			
			
		//Assignment constructor.
		public Count (int min, int max) {
			minimum = min;
			maximum = max;
		}
	}

		
	public static int columns = 6;
	public static int rows = 6;
	public GameObject[] floorTiles;

	private Transform boardHolder;
	//A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();
	//A list of possible locations to place tiles.



	void InitialiseList ()
		{
			//Clear our list gridPositions.
			gridPositions.Clear ();
			
			//Loop through x axis (columns).
			for(int x = 1; x < columns-1; x++)
			{
				//Within each column, loop through y axis (rows).
				for(int y = 1; y < rows-1; y++)
				{
					//At each index add a new Vector3 to our list with the x and y coordinates of that position.
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}
		
		
		//Sets up the outer walls and floor (background) of the game board.
		void BoardSetup ()
		{
			//Instantiate Board and set boardHolder to its transform.
			boardHolder = new GameObject ("Board").transform;
			int xPosition, yPosition;
			xPosition = yPosition = 0;
			//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		for(int y = 4; y > -(rows/2); y--)
			
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			for(int x = -4; x < columns/2; x++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
					toInstantiate.GetComponent<CasillaManager>().XPosition = xPosition;
					toInstantiate.GetComponent<CasillaManager>().YPosition = yPosition;
					
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					
					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (boardHolder);
					yPosition++;
				}
				xPosition++;
				yPosition = 0;
			}
		}
		
		
	
		
		//SetupScene initializes our level and calls the previous functions to lay out the game board
		public void SetupScene ()
		{
			//Creates the outer walls and floor.
			BoardSetup ();
			
			//Reset our list of gridpositions.
			//InitialiseList ();

		}


}
