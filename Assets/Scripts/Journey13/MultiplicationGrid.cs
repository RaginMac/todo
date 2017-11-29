using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicationGrid : MonoBehaviour {

	public Camera cam;

	public GameObject numberBoxPrefab;
	public GameObject snapPointPrefab;

	public List<GameObject> numberBoxes = new List<GameObject>();
	public List<GameObject> snapPoints = new List<GameObject>();

	public List<int> randomNumBoxes = new List<int>();

	private List<GameObject> BlockSet1 =  new List<GameObject>();
	private List<GameObject> BlockSet2 = new List<GameObject>();
	private List<GameObject> BlockSet3 = new List<GameObject>();

	public Transform startPos;

	public int noOfNumberBoxes;
	public int rows;
	public int columns;
	int tempIndex ;
	int tempNeighTile;

	//dragObjects
	public GameObject draggedObject;
	private Vector3 originalObjPos;
	private Vector3 offset;
	private Vector3 snapOffset;

	public Transform[] spawnPoints;
	private int randomNumberBoxIndex;
	private int randomNeighbouringTile;
	private Transform parent;
	public Transform snapParent;
	public Transform numberBoxParent;
	public Transform questionBoxParent;
	public Vector3 reducedSize;
	public Vector3 originalSize;


	public bool g13_1;
	public int noOfRandomBlocks;
	public float startPosValue;
	public int numBoxesLength;
//	public Sprite centerBox;
//	public Color m_color;

	void Awake()
	{
		
	}

	void Start () {
		CreateQuestionGrid ();
		CreateAnswerGrid (startPosValue);
//		CreateSnapPointsGrid ();
		cam = Camera.main;

		if(g13_1){
			StartCoroutine (RemoveBlockSet1 ());
			StartCoroutine (RemoveBlockSet2 ());
			StartCoroutine (RemoveBlockSet3 ());
		}
//		else{
			//FindRandomBlocks(noOfRandomBlocks);
			//ReplaceByTexts ();
//		}
	}

	void Update () {
		DragObject2D ();
	}

	void CreateQuestionGrid()
	{
		Vector3 yOffset = startPos.position;
		Vector3 xOffset = startPos.position;
		xOffset.z -= 3f;
		yOffset.z -= 3f;

		xOffset.x += 0.75f;
		yOffset.y -= 0.75f;

		for (int i = 0; i < rows; i++) {
			GameObject tempObject = Instantiate(numberBoxPrefab, yOffset, Quaternion.identity, questionBoxParent);
			tempObject.GetComponentInChildren<TextMesh> ().text = i.ToString ();
			tempObject.GetComponentInChildren<TextMesh> ().fontSize = 30;
			yOffset.y -= 0.75f;
		}

		for (int i = 0; i < columns; i++) {
			GameObject tempObject = Instantiate(numberBoxPrefab, xOffset, Quaternion.identity, numberBoxParent);
			tempObject.GetComponentInChildren<TextMesh> ().text = i.ToString ();
			tempObject.GetComponentInChildren<TextMesh> ().fontSize = 30;
			xOffset.x += 0.755f;
		}

	}

	public void CreateAnswerGrid(float pos)
	{
		Vector3 xOffset = startPos.position;
		Vector3 snapOffset = startPos.position;
		int iValue = 1;
		int ans = 0;

		xOffset.z -= 3f;
		xOffset.y -= 0.75f;
		snapOffset.z -= 2f;
		snapOffset.y -= 0.75f;

		for (int i = 0; i < rows; i++) {
			xOffset.x = -pos;
			snapOffset.x = -pos;

			for (int j = 0; j < columns; j++) {
				GameObject tempObject = Instantiate(numberBoxPrefab, xOffset, Quaternion.identity, numberBoxParent);
				GameObject tempSnapObject = Instantiate(snapPointPrefab, snapOffset, Quaternion.identity, snapParent);
				xOffset.x += 0.75f;
				snapOffset.x += 0.75f;

				ans = i * j;
				tempObject.GetComponentInChildren<TextMesh> ().text = ans.ToString ();
				tempObject.GetComponentInChildren<TextMesh> ().fontSize = 30;

				tempObject.GetComponent<GridIndexValue> ().indexValue = iValue;
				tempSnapObject.GetComponent<SnapIndexValue> ().indexValue = iValue;
				iValue++;
//
//				if (!g13_1 && i == j) {
//					tempObject.GetComponent<SpriteRenderer> ().color = m_color;
//				}

				numberBoxes.Add (tempObject);
				snapPoints.Add (tempSnapObject);


			}
			xOffset.y -= 0.75f;
			snapOffset.y -= 0.75f;
		}
	}

	public void DragObject2D()
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0))
		{
			if (!draggedObject)
			{
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);
				if (hit.collider.tag == "DraggableObject")
				{
					draggedObject = hit.transform.gameObject;
					originalObjPos = draggedObject.transform.position;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
					offset = draggedObject.transform.position - ray.origin;

					draggedObject.transform.SetParent (numberBoxParent);
					parent = draggedObject.transform;
					CheckBlockSet ();
					parent.transform.localScale = originalSize;

				}
			}
		}

		if(draggedObject) {
			draggedObject.transform.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObject.transform.position.z);
		}

		if (Input.GetMouseButtonUp (0))
		{
			if (draggedObject != null) {
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);
				if (draggedObject && hit.collider.tag != "Snap") {

					draggedObject.transform.position = originalObjPos;
					parent.transform.localScale = reducedSize;
					draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;

				} else {
					DropAnswer (hit.transform.gameObject);
				}

				draggedObject = null;
				parent.transform.SetParent (numberBoxParent);
				parent = null;
			}
		}
	}

	public int UniqueRandomInt(List<int> myList, int min, int max)
	{
		int val = Random.Range (min, max);
		while ((myList.Contains (val) && numberBoxes[val] == null)) {
			val = Random.Range (min, max);
		}
		myList.Add (val);
		return val;
	}

	public void CheckBlockSet()
	{
		if (parent.GetComponent<GridIndexValue> ().BlocksetNo == 1) {
			for (int i = 0; i < BlockSet1.Count; i++) {
				if(BlockSet1 [i] != null)
					BlockSet1 [i].transform.SetParent (parent);
			}
		} 
		else if (parent.GetComponent<GridIndexValue> ().BlocksetNo == 2) {
			for (int i = 0; i < BlockSet2.Count; i++) {
				if(BlockSet2 [i] != null)
					BlockSet2 [i].transform.SetParent (parent);
			}
		}
		else if (parent.GetComponent<GridIndexValue> ().BlocksetNo == 3) {
			for (int i = 0; i < BlockSet3.Count; i++) {
				if(BlockSet3 [i] != null)
					BlockSet3 [i].transform.SetParent (parent);
			}
		}
	}

	public void FindRandomBlocks(int randomNum) {
		for (int i = 0; i < randomNum; i++) {
			UniqueRandomInt(randomNumBoxes, 12, 120);
		}
	}

//	public void ReplaceByTexts()
//	{
//		for (int i = 0; i < noOfRandomBlocks; i++) {
//			randomNumberBoxIndex = randomNumBoxes [i];
//
//			Vector3 tempPos = numberBoxes [randomNumberBoxIndex].transform.position;
//			tempPos.z -= 5f;
//			textBoxes [i].transform.position = tempPos;
//
//			numberBoxes [randomNumberBoxIndex].SetActive (false);
//			textBoxes[i].SetActive (true);
//		}
//	}

	IEnumerator RemoveBlockSet1()
	{
		yield return new WaitForSeconds(0.1f);
		tempIndex = UniqueRandomInt(randomNumBoxes, 22, 100);
		int k = 0;

		for (int i = 0; i < numBoxesLength; i++)
		{
			if (numberBoxes [tempIndex] != null) {
				BlockSet1.Add (numberBoxes [tempIndex]);
				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				BlockSet1 [i].GetComponent<GridIndexValue> ().BlocksetNo = 1;

				int val = Random.Range (0, 3);
				tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);

				while(tempNeighTile <= 0 || randomNumBoxes.Contains(tempNeighTile)) {
					if (k > 5) {
						break;
					}

					val = Random.Range (0, 3); 
					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
					k++;
				}

				if (tempNeighTile != 0) {
					numberBoxes [tempIndex] = null;
					tempIndex = tempNeighTile;
					randomNumBoxes.Add (tempIndex);
				}

			} else {
				continue;
			}
		}

		Vector3 tempPosition = spawnPoints [0].transform.position;

		parent = BlockSet1 [1].transform;
		CheckBlockSet ();
		parent.transform.localScale = reducedSize;
		parent.transform.position = tempPosition;
		parent = null;

	}


	IEnumerator RemoveBlockSet2()
	{
		yield return new WaitForSeconds(0.15f);
		tempIndex = UniqueRandomInt(randomNumBoxes, 22, 100);
		int k = 0;

		for (int i = 0; i < numBoxesLength; i++)
		{
			if (numberBoxes [tempIndex] != null) {
				BlockSet2.Add (numberBoxes [tempIndex]);
				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				BlockSet2 [i].GetComponent<GridIndexValue> ().BlocksetNo = 2;

				int val = Random.Range (0, 3);
				tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);

				while(randomNumBoxes.Contains(tempNeighTile) || tempNeighTile <= 0  || k < 4) {
					val = Random.Range (0, 3); 
					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
					k++;

					if (k > 5) {
						break;
					}
				}

				if (tempNeighTile != 0) {
					numberBoxes [tempIndex] = null;
					tempIndex = tempNeighTile;
					randomNumBoxes.Add (tempIndex);
				}

			} else {
				continue;
			}
		}

		Vector3 tempPosition = spawnPoints [1].transform.position;

		parent = BlockSet2 [1].transform;
		CheckBlockSet ();
		parent.transform.localScale = reducedSize;
		parent.transform.position = tempPosition;
		parent = null;
	}

	IEnumerator RemoveBlockSet3()
	{
		yield return new WaitForSeconds(0.2f);
		tempIndex = UniqueRandomInt(randomNumBoxes, 22, 100);
		int k = 0;

		for (int i = 0; i < numBoxesLength; i++)
		{
			if (numberBoxes [tempIndex] != null) {
				BlockSet3.Add (numberBoxes [tempIndex]);
				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				BlockSet3 [i].GetComponent<GridIndexValue> ().BlocksetNo = 3;
			
				int val = Random.Range (0, 3);
				tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);

				while(tempNeighTile <= 0 || randomNumBoxes.Contains(tempNeighTile)) {
					val = Random.Range (0, 3); 
					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
					k++;

					if (k > 5) {
						break;
					}
				}

				if (tempNeighTile != 0) {
					numberBoxes [tempIndex] = null;
					tempIndex = tempNeighTile;
					randomNumBoxes.Add (tempIndex);
				}

			} else {
				continue;
			}
		}

		Vector3 tempPosition = spawnPoints [2].transform.position;

		parent = BlockSet3 [1].transform;
		CheckBlockSet ();
		parent.transform.localScale = reducedSize;
		parent.transform.position = tempPosition;
		parent = null;
	}
//	bool tempflag = false;

//	public void RemoveBlockSet2()
//	{
//		List<int> tempValues = new List<int>();
//		int k = 0;
//		int count;
//		int boxArrayLength = patternValues.Count;
//
//		while (!tempflag) 
//		{
//			tempIndex = UniqueRandomInt(randomNumBoxes, 12, 100);
//			count = 0;
//			for (int i = 0; i < boxArrayLength; i++) 
//			{
//				if (numberBoxes [tempIndex] != null) 
//				{
//					BlockSet2.Add (numberBoxes [tempIndex]);
//					numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//					snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//					BlockSet2 [i].GetComponent<GridIndexValue> ().BlocksetNo = 2;
//
//					int val = patternValues [k];
//					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
//
//					while (tempValues.Contains(tempNeighTile) || tempNeighTile <= 0 || randomNumBoxIndexes.Contains(tempNeighTile)) {
//						tempValues.Clear ();
//						break;
//					}
//
//					randomNumBoxIndexes.Add (tempIndex);
//					numberBoxes [tempIndex] = null;
//					tempIndex = tempNeighTile;
//					count++;
//					k++;
//				} else {
//					continue;
//				}
//			}
//			if (count >= boxArrayLength - 1) {
//				tempflag = true;
//			}
//
//		}

//		int k = 0;
//		List<int> tempValues = new List<int>();
//		tempIndex = UniqueRandomInt(randomNumBoxes, 60, 88);
//		int boxArrayLength = patternValues.Count;
//
//		for (int i = 0; i < boxArrayLength; i++) 
//		{
//			if (numberBoxes [tempIndex] != null) 
//			{
//				BlockSet2.Add (numberBoxes [tempIndex]);
//				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//				BlockSet2 [i].GetComponent<GridIndexValue> ().BlocksetNo = 2;
//				randomNumBoxIndexes.Add (tempIndex);
//
//				int val = patternValues [k];
//				tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
//
//				while (tempValues.Contains(tempNeighTile) || tempNeighTile <= 0 || randomNumBoxIndexes.Contains(tempNeighTile)) {
//					val = Random.Range (0, 3);
//					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
//				}
//
//				numberBoxes [tempIndex] = null;
//				tempIndex = tempNeighTile;
//				k++;
//
//			} else {
//				continue;
//			}
//		}
//
//		Vector3 tempPosition = spawnPoints [1].transform.position;
//		tempPosition.z -= 10;
//
//		parent = BlockSet2 [1].transform;
//		CheckBlockSet ();
//		parent.transform.localScale = reducedSize;
//		parent.transform.position = tempPosition;
//		parent = null;
//	}

//	public void RemoveBlockSet3()
//	{
//		int k = 0;
//		List<int> tempValues = new List<int>();
//		tempIndex = UniqueRandomInt(randomNumBoxes, 24, 88);
//		int numBoxesLength = patternValues.Count;
//
//		for (int i = 0; i < numBoxesLength; i++) 
//		{
//			if (numberBoxes [tempIndex] != null) 
//			{
//				BlockSet3.Add (numberBoxes [tempIndex]);
//				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//				BlockSet3 [i].GetComponent<GridIndexValue> ().BlocksetNo = 2;
//				randomNumBoxIndexes.Add (tempIndex);
//
//				int val = patternValues [k];
//				tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
//
//				while (tempValues.Contains(tempNeighTile) || tempNeighTile <= 0 || randomNumBoxIndexes.Contains(tempNeighTile)) {
//					val = Random.Range (0, 3);
//					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
//				}
//
//				numberBoxes [tempIndex] = null;
//				tempIndex = tempNeighTile;
//				k++;
//
//			}
//		}
//
//		Vector3 tempPosition = spawnPoints [2].transform.position;
//		tempPosition.z -= 10;
//
//		parent = BlockSet3 [1].transform;
//		CheckBlockSet ();
//		parent.transform.localScale = reducedSize;
//		parent.transform.position = tempPosition;
//		parent = null;
//	}

	public void DropAnswer(GameObject other)
	{
		if (draggedObject != null)
		{
			if (draggedObject.GetComponent<GridIndexValue> ().indexValue == other.GetComponent<SnapIndexValue> ().indexValue)
			{
				if (draggedObject.GetComponent<GridIndexValue> ().BlocksetNo == 1)
				{
					for (int i = 0; i < BlockSet1.Count; i++) {
						if (BlockSet1 [i] != null) {

							int tempIndex = BlockSet1 [i].GetComponent<GridIndexValue> ().indexValue;
							numberBoxes [tempIndex] = BlockSet1 [i];
							BlockSet1 [i] = null;
							numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = false;
						}

					}

				} else if (draggedObject.GetComponent<GridIndexValue> ().BlocksetNo == 2) {
					for (int i = 0; i < BlockSet2.Count; i++) {
						if (BlockSet2 [i] != null) {
							BlockSet2 [i].GetComponent<BoxCollider2D> ().enabled = false;
							int tempIndex = BlockSet2 [i].GetComponent<GridIndexValue> ().indexValue;
							numberBoxes [tempIndex] = BlockSet2 [i];
							BlockSet2 [i] = null;
							numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = false;
						}
					}

				} else if (draggedObject.GetComponent<GridIndexValue> ().BlocksetNo == 3) {
					for (int i = 0; i < BlockSet3.Count; i++) {
						if (BlockSet3 [i] != null) {
							BlockSet3 [i].GetComponent<BoxCollider2D> ().enabled = false;
							int tempIndex = BlockSet3 [i].GetComponent<GridIndexValue> ().indexValue;
							numberBoxes [tempIndex] = BlockSet3 [i];
							BlockSet3 [i] = null;
							numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = false;
						}
					}

				}

				draggedObject.transform.position = other.GetComponent<SnapIndexValue> ().transform.position;
			} else {
				draggedObject.transform.position = originalObjPos;
				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				parent.transform.localScale = reducedSize;
			}
		}
	}
}
