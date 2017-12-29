using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockGame : MonoBehaviour {

	public Camera cam;
	public Transform startPos;

	public GameObject[] numberBoxes;
	public List<GameObject> BlockSet1 =  new List<GameObject>();
	public List<GameObject> BlockSet2 = new List<GameObject>();
	public GameObject[] BlockSet3;
	public Transform[] snapPoints;


	public List<int> randomNumBoxes = new List<int>();
	public List<int> randomNeighNumBoxes = new List<int>();
	public List<int> patternValues = new List<int>();

	public int noOfNumberBoxes;
	public int columns;
	int tempIndex ;
	int tempNeighTile;

	//dragObjects
	public GameObject draggedObject;
	private Vector3 originalObjPos;
	private Vector3 offset;

	public Transform[] spawnPoints;
	public int randomNumberBoxIndex;
	public int randomNeighbouringTile;
	public Transform parent;
	public Vector3 reducedSize;
	public Vector3 originalSize;
	public Transform mainParent;
	public int numBoxesLength = 3;
	public int firstNum;

	void Awake()
	{
		
	}

	void Start () {
		
		CreateGrid ();
		CreateSnapPointsGrid ();

		cam = Camera.main;

		StartCoroutine (RemoveBlockSet1 ());
		StartCoroutine (RemoveBlockSet2 ());
	}

	void Update () {
		DragObject2D ();
	}

	public void CreateGrid()
	{
		Vector3 xOffset = startPos.transform.position;
		xOffset.z -= 3f;
		for (int i = 1; i < noOfNumberBoxes; i++) {
			numberBoxes [i].gameObject.SetActive (true);
			numberBoxes [i].GetComponentInChildren<TextMesh> ().fontSize = 28;
			numberBoxes [i].GetComponentInChildren<TextMesh> ().text = firstNum.ToString();
			numberBoxes [i].GetComponent<IndexValue> ().indexValue = i;
			numberBoxes [i].transform.position = xOffset;
			firstNum++;

			xOffset.x += 1.31f;

			if (i % columns == 0) {
				xOffset.y -= 1.45f;
				xOffset.x = startPos.transform.position.x;
			}
		}
	}

	public void CreateSnapPointsGrid()
	{
		Vector3 xOffset = startPos.transform.position;
		xOffset.z -= 1.5f;
		for (int i = 1; i < noOfNumberBoxes; i++) {
			snapPoints [i].gameObject.SetActive (true);
			snapPoints [i].transform.position = xOffset;
			snapPoints [i].GetComponent<SnapIndexValue> ().indexValue = i; 

			xOffset.x += 1.31f;

			if (i % columns == 0) {
				xOffset.y -= 1.45f;
				xOffset.x = startPos.transform.position.x;
			}
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
				if (hit != null && hit.collider.tag == "DraggableObject")
				{
					draggedObject = hit.transform.gameObject;
					originalObjPos = draggedObject.transform.position;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
					offset = draggedObject.transform.position - ray.origin;

					draggedObject.transform.SetParent (mainParent);
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
				parent.transform.SetParent (mainParent);
				parent = null;
			}
		}
	}

	public int UniqueRandomInt(List<int> myList, int min, int max)
	{
		int val = Random.Range (min, max);
		while (myList.Contains (val) || numberBoxes[val] == null) {
			val = Random.Range (min, max);
		}
		//myList.Add (val);
		return val;
	}


	IEnumerator RemoveBlockSet1()
	{
		yield return new WaitForSeconds(0.01f);

		tempIndex = UniqueRandomInt (randomNumBoxes, 1, 7);
		int k = 0;

		for (int i = 0; i < numBoxesLength; i++)
		{
			if (numberBoxes [tempIndex] != null) {

				int val = Random.Range(0, 4);
				tempNeighTile = (numberBoxes [tempIndex].GetComponent<IndexValue> ().neighbouringTiles [val]);
				while(tempNeighTile <= 0 || randomNumBoxes.Contains(tempNeighTile)) {
					if (k > 10) {
						break;
					}

					val = Random.Range(0, 4); 
					tempNeighTile = (numberBoxes [tempIndex].GetComponent<IndexValue> ().neighbouringTiles [val]);
					k++;
				}

				if (i < numBoxesLength - 1) {
					patternValues.Add (val);
				}

				randomNumBoxes.Add (tempIndex);
				BlockSet1.Add (numberBoxes [tempIndex]);
				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				BlockSet1 [i].GetComponent<IndexValue> ().BlocksetNo = 1;

				if (tempNeighTile != 0) {
					numberBoxes [tempIndex] = null;
					tempIndex = tempNeighTile;
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

	public List<int> tempIndexes = new List<int>();
	IEnumerator RemoveBlockSet2()
	{
		yield return new WaitForSeconds(0.02f);
		int k = 0;
		int val = 0;

		tempIndex = UniqueRandomInt (randomNumBoxes, 16, 20);

		tempIndexes.Add (tempIndex);
		BlockSet2.Add (numberBoxes [tempIndex]);

		for (int i = 0; i < numBoxesLength - 1; i++) {
			val = patternValues[i];
			tempNeighTile = (numberBoxes [tempIndex].GetComponent<IndexValue> ().neighbouringTiles [val]);

			if (tempNeighTile != 0) {
				numberBoxes [tempIndex] = null;
				tempIndex = tempNeighTile;
			}
			tempIndexes.Add (tempIndex);
			BlockSet2.Add (numberBoxes [tempIndex]);

		}

		for (int i = 0; i < tempIndexes.Count; i++) {
			numberBoxes [BlockSet2 [i].GetComponent<IndexValue>().indexValue] = null;
			BlockSet2 [i].GetComponent<BoxCollider2D> ().enabled = true;
			snapPoints [tempIndexes[i]].GetComponent<BoxCollider2D> ().enabled = true;
			BlockSet2 [i].GetComponent<IndexValue> ().BlocksetNo = 2;	
		}

		Vector3 tempPosition = spawnPoints [1].transform.position;

		parent = BlockSet2 [0].transform;
		CheckBlockSet ();
		parent.transform.localScale = reducedSize;
		parent.transform.position = tempPosition;
		parent = null;
	}

	public void DropAnswer(GameObject other)
	{
		if (draggedObject != null)
		{
			if (draggedObject.GetComponent<IndexValue> ().indexValue == other.GetComponent<SnapIndexValue> ().indexValue)
			{
				if (draggedObject.GetComponent<IndexValue> ().BlocksetNo == 1)
				{
					for (int i = 0; i < BlockSet1.Count; i++) {
						if (BlockSet1 [i] != null) {
							
							int tempIndex = BlockSet1 [i].GetComponent<IndexValue> ().indexValue;
							numberBoxes [tempIndex] = BlockSet1 [i];
							BlockSet1 [i] = null;
							numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = false;
						}

					}
							
				} else if (draggedObject.GetComponent<IndexValue> ().BlocksetNo == 2) {
					for (int i = 0; i < BlockSet2.Count; i++) {
						if (BlockSet2 [i] != null) {
							BlockSet2 [i].GetComponent<BoxCollider2D> ().enabled = false;
							int tempIndex = BlockSet2 [i].GetComponent<IndexValue> ().indexValue;
							numberBoxes [tempIndex] = BlockSet2 [i];
							BlockSet2 [i] = null;
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

		Manager.Instance.PlayDragDropAudio();
	}

	public void CheckBlockSet()
	{

		if (parent.GetComponent<IndexValue> ().BlocksetNo == 1) {
			for (int i = 0; i < BlockSet1.Count; i++) {
				if(BlockSet1 [i] != null)
					BlockSet1 [i].transform.SetParent (parent);
			}
		} else if (parent.GetComponent<IndexValue> ().BlocksetNo == 2) {
			for (int i = 0; i < BlockSet2.Count; i++) {
				if(BlockSet2 [i] != null)
					BlockSet2 [i].transform.SetParent (parent);
			}
		}
	}
}
