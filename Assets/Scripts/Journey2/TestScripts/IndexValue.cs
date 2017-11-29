using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexValue : MonoBehaviour {

	public int indexValue;
	public int BlocksetNo;
	public int[] neighbouringTiles;

	public BlockGame blockGame;

	void Awake()
	{
		//blockGame = GameObject.Find ("Manager").GetComponent<BlockGame>();
		neighbouringTiles = new int[4];
	}

	// Use this for initialization
	void Start () {
		
		neighbouringTiles [0] = indexValue + 1;
		neighbouringTiles [1] = indexValue - 1;
		neighbouringTiles [2] = indexValue - blockGame.columns;
		neighbouringTiles [3] = indexValue + blockGame.columns;
		CheckNeighbouringTiles ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckNeighbouringTiles()
	{
		if (indexValue % blockGame.columns == 0)
			neighbouringTiles [0] = 0;

		if ((indexValue - 1) % blockGame.columns == 0)
			neighbouringTiles [1] = 0;

		if (indexValue >= 1 && indexValue <= blockGame.columns)
			neighbouringTiles [2] = 0;

		if (indexValue <= blockGame.noOfNumberBoxes - 1 && indexValue >= ((blockGame.noOfNumberBoxes - 1) - blockGame.columns))
			neighbouringTiles [3] = 0;
	}
}
