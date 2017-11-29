using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridIndexValue : MonoBehaviour {
	public int indexValue;
	public int BlocksetNo;
	public int rows;
	public int columns;

	public int[] neighbouringTiles;

	void Awake()
	{
		//blockGame = GameObject.Find ("Manager").GetComponent<BlockGame>();
		neighbouringTiles = new int[4];
	}

	// Use this for initialization
	void Start () {

		neighbouringTiles [0] = indexValue + 1; // right tile
		neighbouringTiles [1] = indexValue - 1; //left tile
		neighbouringTiles [2] = indexValue - columns; //top tile
		neighbouringTiles [3] = indexValue + columns; // bottom tile
		CheckNeighbouringTiles ();

	}

	// Update is called once per frame
	void Update () {

	}

	public void CheckNeighbouringTiles()
	{
		int noOfNumberBoxes = rows * columns;

		if (indexValue % columns == 0)
			neighbouringTiles [0] = 0;

		if ((indexValue - 1) % columns == 0)
			neighbouringTiles [1] = 0;

		if (indexValue >= 0 && indexValue <= columns)
			neighbouringTiles [2] = 0;

		if (indexValue <= noOfNumberBoxes - 1 && indexValue >= ((noOfNumberBoxes - 1) - columns)) 
			neighbouringTiles [3] = 0;
	}
}
