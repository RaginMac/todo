using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicationGrid : MonoBehaviour {

	public Camera cam;

	public GameObject numberBoxPrefab;
	public GameObject snapPointPrefab;

	public List<GameObject> numberBoxes = new List<GameObject>();
	public List<GameObject> snapPoints = new List<GameObject>();

	private List<int> patternValues = new List<int>();
	private List<int> randomNumBoxes = new List<int>();
	List<int> tempValues = new List<int>();

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


	public int noOfRandomBlocks;
	public float startPosValue;
	public int numBoxesLength;
//	public Sprite centerBox;
//	public Color m_color;

	void Awake()
	{
		
	}

	void Start () {
		ShuffleOptionsPositions ();
		CreateQuestionGrid ();
		CreateAnswerGrid (startPosValue);

//		CreateSnapPointsGrid ();
		cam = Camera.main;

		StartCoroutine (RemoveBlockSet1 ());
		StartCoroutine (RemoveBlockSet2 ());
		StartCoroutine (RemoveBlockSet3 ());
	}

	void Update () {
		DragObject2D ();
	}

	void ShuffleOptionsPositions() 
	{
		
		for (int i = 0; i < spawnPoints.Length; i++) {
			Transform temp = spawnPoints [i];
			int r = Random.Range (i, spawnPoints.Length);
			spawnPoints [i] = spawnPoints [r];
			spawnPoints [r] = temp;
			//Debug.Log (r);
		}
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
			UniqueRandomInt(randomNumBoxes, 1, 10);
		}
	}

	IEnumerator RemoveBlockSet1()
	{
		yield return new WaitForSeconds(0.1f);
		tempIndex = UniqueRandomInt(tempValues, 1, 20);
		int k = 0;

		for (int i = 0; i < numBoxesLength; i++)
		{
			if (numberBoxes [tempIndex] != null) {
				
				int val = Random.Range(0, 4);
				tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
				while(tempNeighTile <= 0 || randomNumBoxes.Contains(tempNeighTile)) {
					if (k > 10) {
						break;
					}

					val = Random.Range(0, 4); 
					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);
					k++;
				}

				patternValues.Add (val);
				randomNumBoxes.Add (tempIndex);
				BlockSet1.Add (numberBoxes [tempIndex]);
				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				BlockSet1 [i].GetComponent<GridIndexValue> ().BlocksetNo = 1;

				if (tempNeighTile != 0) {
					numberBoxes [tempIndex] = null;
					tempIndex = tempNeighTile;
					tempValues.Add (tempIndex);
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
		int val = 0;
		int patternIndex = 0;
		int counter = 0;
	 	
		do
		{
			BlockSet2.Clear();
			patternIndex = 0;
			counter = 0;

			tempIndex = UniqueRandomInt(tempValues, 30, 50);

			for (int i = 0; i < numBoxesLength; i++)
			{
				if(numberBoxes [tempIndex] != null) 
				{
					val = patternValues[patternIndex];
					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);

					if(randomNumBoxes.Contains(tempNeighTile) || tempNeighTile <= 0 || tempNeighTile == null || tempNeighTile == 0)
					{
						break;
					} else {

						BlockSet2.Add (numberBoxes [tempIndex]);
						randomNumBoxes.Add (tempIndex);
						snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;

						tempIndex = tempNeighTile;
						tempValues.Add (tempIndex);

						patternIndex++;
						counter++;
					}
				}
			}
		} 
		while(counter <= 3);

		for (int i = 0; i < BlockSet1.Count; i++) {
			numberBoxes [BlockSet2 [i].GetComponent<GridIndexValue>().indexValue] = null;
			BlockSet2 [i].GetComponent<BoxCollider2D> ().enabled = true;
			BlockSet2 [i].GetComponent<GridIndexValue> ().BlocksetNo = 2;	
		}

		Vector3 tempPosition = spawnPoints [2].transform.position;

		parent = BlockSet2 [0].transform;
		CheckBlockSet ();
		parent.transform.localScale = reducedSize;
		parent.transform.position = tempPosition;
		parent = null;
	}


	IEnumerator RemoveBlockSet3()
	{
		yield return new WaitForSeconds(0.15f);
		int val = 0;
		int patternIndex = 0;
		int counter = 0;

		do
		{
			BlockSet3.Clear();
			patternIndex = 0;
			counter = 0;

			tempIndex = UniqueRandomInt(tempValues, 60, 90);

			for (int i = 0; i < numBoxesLength; i++)
			{
				if(numberBoxes [tempIndex] != null) 
				{
					val = patternValues[patternIndex];
					tempNeighTile = (numberBoxes [tempIndex].GetComponent<GridIndexValue> ().neighbouringTiles [val]);

					if(randomNumBoxes.Contains(tempNeighTile) || tempNeighTile <= 0 || tempNeighTile == null || tempNeighTile == 0)
					{
						break;
					} else {

						BlockSet3.Add (numberBoxes [tempIndex]);
						randomNumBoxes.Add (tempIndex);
						snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;

						tempIndex = tempNeighTile;
						tempValues.Add (tempIndex);

						patternIndex++;
						counter++;
					}
				}
			}
		} 
		while(counter <= 3);

		for (int i = 0; i < BlockSet1.Count; i++) {
			numberBoxes [BlockSet2 [i].GetComponent<GridIndexValue>().indexValue] = null;
			BlockSet3 [i].GetComponent<BoxCollider2D> ().enabled = true;
			BlockSet3 [i].GetComponent<GridIndexValue> ().BlocksetNo = 3;	
		}

		Vector3 tempPosition = spawnPoints [1].transform.position;

		parent = BlockSet3 [0].transform;
		CheckBlockSet ();
		parent.transform.localScale = reducedSize;
		parent.transform.position = tempPosition;
		parent = null;
	}

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
						//print ("BS2");
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
					//print ("BS2");
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
