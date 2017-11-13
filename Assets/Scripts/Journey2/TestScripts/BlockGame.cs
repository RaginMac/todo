using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockGame : MonoBehaviour {

	public Camera cam;

	public GameObject[] numberBoxes;
	public GameObject[] BlockSet1;
	public GameObject[] BlockSet2;
	public GameObject[] BlockSet3;
	public Transform[] snapPoints;
	public int noOfNumberBoxes;
	public int columns;
	int tempIndex ;
	int tempNeighTile;

//	public int topTile;
//	public int bottomTile;
//	public int leftTile;
//	public int rightTile;

	//dragObjects
	public GameObject draggedObject;
	private Vector3 originalObjPos;
	private Vector3 offset;

	public Transform[] spawnPoints;
	public int randomNumberBoxIndex;
	public int randomNeighbouringTile;
	public Transform parent;
	public Transform mainParent;

	void Awake()
	{
		
	}

	void Start () {
		
		CreateGrid ();
		CreateSnapPointsGrid ();

		cam = Camera.main;
		Invoke("RemoveBlockSet1", 0.01f);
		Invoke("RemoveBlockSet2", 0.01f);
		//Invoke("RemoveBlockSet3", 0.01f);
	}

	void Update () {
		DragObject2D ();
	}

	public void CreateGrid()
	{
		Vector3 xOffset = numberBoxes [0].transform.position;
		xOffset.z -= 3f;
		for (int i = 1; i < noOfNumberBoxes; i++) {
			numberBoxes [i].gameObject.SetActive (true);
			numberBoxes [i].GetComponentInChildren<TextMesh> ().fontSize = 28;
			numberBoxes [i].GetComponentInChildren<TextMesh> ().text = i.ToString();
			//numberBoxes [i].GetComponentInChildren<TextMesh> ().text = "";
			numberBoxes [i].GetComponent<IndexValue> ().indexValue = i;
			numberBoxes [i].transform.position = xOffset;

			xOffset.x += 1.31f;

			if (i % columns == 0) {
				xOffset.y -= 1.45f;
				xOffset.x = numberBoxes [0].transform.position.x;
			}
		}
	}

	public void CreateSnapPointsGrid()
	{
		Vector3 xOffset = snapPoints [0].transform.position;
		xOffset.z -= 1.5f;
		for (int i = 1; i < noOfNumberBoxes; i++) {
			snapPoints [i].gameObject.SetActive (true);
			snapPoints [i].transform.position = xOffset;
			snapPoints [i].GetComponent<SnapIndexValue> ().indexValue = i; 

			xOffset.x += 1.31f;

			if (i % columns == 0) {
				xOffset.y -= 1.45f;
				xOffset.x = snapPoints [0].transform.position.x;
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


					parent = draggedObject.transform;
					draggedObject.transform.parent = null;

					CheckBlockSet ();
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
				} else {
				
					DropAnswer (hit.transform.gameObject);
				}
				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				draggedObject = null;
				parent.transform.SetParent (mainParent);
				parent = null;
			}
		}
	}

	public void RemoveBlockSet1()
	{
		randomNumberBoxIndex = Random.Range (1, 18);
		tempIndex = randomNumberBoxIndex;
		parent = numberBoxes [tempIndex].transform;

		for (int i = 0; i < 3; i++)
		{
			if (numberBoxes [tempIndex] != null) {
				BlockSet1 [i] = numberBoxes [tempIndex];
				numberBoxes [tempIndex].transform.SetParent (parent);
				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				BlockSet1 [i].GetComponent<IndexValue> ().BlocksetNo = 1;

				for (int j = 0; j < 4; j++) {
					randomNeighbouringTile = Random.Range (0, 4);
					tempNeighTile = numberBoxes [tempIndex].GetComponent<IndexValue> ().neighbouringTiles [randomNeighbouringTile];

					if (tempNeighTile <= 0) {
						continue;
					} else if (tempNeighTile > 0) {
						numberBoxes [tempIndex] = null;
						tempIndex = tempNeighTile;
						break;
					}
				}

			} else {
				continue;
			}

		}
		Vector3 tempPosition = spawnPoints [0].transform.position;
		tempPosition.z -= 10;
		parent.transform.position = tempPosition;
	}

	public void RemoveBlockSet2()
	{
		randomNumberBoxIndex = Random.Range (18, 36);
		tempIndex = randomNumberBoxIndex;
		parent = numberBoxes [tempIndex].transform;

		for (int i = 0; i < 3; i++)
		{
			if (numberBoxes [tempIndex] != null) {
				BlockSet2 [i] = numberBoxes [tempIndex];
				numberBoxes [tempIndex].transform.SetParent (parent);
				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
				BlockSet2 [i].GetComponent<IndexValue> ().BlocksetNo = 2;

				for (int j = 0; j < 4; j++) {
					randomNeighbouringTile = Random.Range (0, 4);
					tempNeighTile = numberBoxes [tempIndex].GetComponent<IndexValue> ().neighbouringTiles [randomNeighbouringTile];

					if (tempNeighTile <= 0 && numberBoxes [tempNeighTile] == null) {
						continue;
					} else if (tempNeighTile > 0 && numberBoxes [tempNeighTile] != null) {
						numberBoxes [tempIndex] = null;
						tempIndex = tempNeighTile;
						break;
					}
				}

			} else {
				continue;
			}

		}
		Vector3 tempPosition = spawnPoints [1].transform.position;
		tempPosition.z -= 10;
		parent.transform.position = tempPosition;
	}

//	public void RemoveBlockSet3()
//	{
//		randomNumberBoxIndex = Random.Range (9, 27);
//		tempIndex = randomNumberBoxIndex;
//
//		if (numberBoxes [tempIndex] != null) {
//			parent = numberBoxes [randomNumberBoxIndex].transform;
//			for (int i = 0; i < 3; i++) {
//				BlockSet3 [i] = numberBoxes [tempIndex];
//				numberBoxes [tempIndex].transform.SetParent (parent);
//				numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//				snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//				randomNeighbouringTile = Random.Range (0, 4);
//				 tempNeighTile = numberBoxes [tempIndex].GetComponent<IndexValue> ().neighbouringTiles [randomNeighbouringTile];
//				numberBoxes [tempIndex] = null;
//				if (tempNeighTile < 0) {
//					randomNeighbouringTile = Random.Range (0, 4);
//				} else {
//					tempIndex = tempNeighTile;
//				}
//			}
//
//			parent.transform.position = spawnPoints [2].transform.position;
//		} else {
//			randomNumberBoxIndex = Random.Range (9, 27);
//			tempIndex = randomNumberBoxIndex;
//
//		parent = numberBoxes [randomNumberBoxIndex].transform;
//		for (int i = 0; i < 3; i++) {
//			BlockSet3 [i] = numberBoxes [tempIndex];
//			numberBoxes [tempIndex].transform.SetParent (parent);
//			numberBoxes [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//			snapPoints [tempIndex].GetComponent<BoxCollider2D> ().enabled = true;
//			randomNeighbouringTile = Random.Range (0, 4);
//			tempNeighTile = numberBoxes [tempIndex].GetComponent<IndexValue> ().neighbouringTiles [randomNeighbouringTile];
//			numberBoxes [tempIndex] = null;
//			if (tempNeighTile < 0) {
//				randomNeighbouringTile = Random.Range (0, 4);
//			} else {
//				tempIndex = tempNeighTile;
//			}
//		}
//
//		parent.transform.position = spawnPoints [2].transform.position;
//		}
//	}

	public void DropAnswer(GameObject other)
	{
		if (draggedObject != null)
		{
			if (draggedObject.GetComponent<IndexValue> ().indexValue == other.GetComponent<SnapIndexValue> ().indexValue)
			{
				if (draggedObject.GetComponent<IndexValue> ().BlocksetNo == 1)
				{
					for (int i = 0; i < BlockSet1.Length; i++) {
						if (BlockSet1 [i] != null) {
							int tempIndex = BlockSet1 [i].GetComponent<IndexValue> ().indexValue;
							numberBoxes [tempIndex] = BlockSet1 [i];
							BlockSet1 [i] = null;
						}
					}
							
				} else if (draggedObject.GetComponent<IndexValue> ().BlocksetNo == 2) {
					for (int i = 0; i < BlockSet2.Length; i++) {
						if (BlockSet2 [i] != null) {
							int tempIndex = BlockSet2 [i].GetComponent<IndexValue> ().indexValue;
							numberBoxes [tempIndex] = BlockSet2 [i];
							BlockSet2 [i] = null;
						}
					}
								
				}
				draggedObject.transform.position = other.GetComponent<SnapIndexValue> ().transform.position;
			} else {
				draggedObject.transform.position = originalObjPos;
			}
		}
	}

	public void CheckBlockSet()
	{
		if (parent.GetComponent<IndexValue> ().BlocksetNo == 1) {
			for (int i = 0; i < BlockSet1.Length; i++) {
				if(BlockSet1 [i] != null)
					BlockSet1 [i].transform.SetParent (parent);
			}
		} else if (parent.GetComponent<IndexValue> ().BlocksetNo == 2) {
			for (int i = 0; i < BlockSet2.Length; i++) {
				if(BlockSet2 [i] != null)
					BlockSet2 [i].transform.SetParent (parent);
			}
		}
	}
}
