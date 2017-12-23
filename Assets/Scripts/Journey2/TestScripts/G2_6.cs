using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiDimensinalArray
{
	public GameObject[] column;
}

public class G2_6 : MonoBehaviour {
	public Camera cam;

	public MultiDimensinalArray[] numberBoxGrid;
	public GameObject[] snapPoints;

	public List<int> patternValues = new List<int>();

	public List<GameObject> blockSet1 = new List<GameObject>();
	public List<GameObject> blockSet2 = new List<GameObject>();

	public Transform startpos;
	public int row;
	public int col;

	public GameObject draggedObject;
	private Vector3 originalObjPos;
	private Vector3 offset;

	public Transform parent;
	public Vector3 reducedSize;
	public Vector3 originalSize;
	public Transform mainParent;

	void Start () {
		CreateGrid ();
		CreateSnapPointsGrid ();
	}
	

	void Update () {
		
	}

	public void CreateGrid()
	{
		int textValue = 1;
		Vector3 xOffset = startpos.position;
		xOffset.z -= 3f;

		for (int i = 0; i < row; i++) {
			for (int j = 0; j < col; j++) {
				numberBoxGrid [i].column [j].SetActive (true);
			
				numberBoxGrid [i].column [j].GetComponentInChildren<TextMesh> ().fontSize = 28; 
				numberBoxGrid [i].column [j].GetComponentInChildren<TextMesh> ().text = textValue.ToString();

				numberBoxGrid [i].column [j].GetComponent<IndexValue> ().indexValue = i;
				numberBoxGrid [i].column [j].transform.position = xOffset;

				xOffset.x += 1.31f;
				textValue++;
			}

			xOffset.y -= 1.45f;
			xOffset.x = startpos.position.x;
		}
	}

	public void CreateSnapPointsGrid()
	{
		Vector3 xOffset = snapPoints [0].transform.position;
		xOffset.z -= 1.5f;
		for (int i = 1; i < snapPoints.Length; i++) {
			snapPoints [i].gameObject.SetActive (true);
			snapPoints [i].transform.position = xOffset;
			snapPoints [i].GetComponent<SnapIndexValue> ().indexValue = i; 

			xOffset.x += 1.31f;

			if (i % col == 0) {
				xOffset.y -= 1.45f;
				xOffset.x = snapPoints [0].transform.position.x;
			}
		}
	}

	public int UniqueRandomInt(List<int> myList, int min, int max)
	{
		int val = Random.Range (min, max);
		while (myList.Contains (val)) {
			val = Random.Range (min, max);
		}
		myList.Add (val);
		return val;
	}

	public void CheckBlockSet()
	{
		if (parent.GetComponent<IndexValue> ().BlocksetNo == 1) {
			for (int i = 0; i < blockSet1.Count; i++) {
				if(blockSet1 [i] != null)
					blockSet1 [i].transform.SetParent (parent);
			}
		} else if (parent.GetComponent<IndexValue> ().BlocksetNo == 2) {
			for (int i = 0; i < blockSet2.Count; i++) {
				if(blockSet2 [i] != null)
					blockSet2 [i].transform.SetParent (parent);
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

					//DropAnswer (hit.transform.gameObject);
				}

				draggedObject = null;
				parent.transform.SetParent (mainParent);
				parent = null;
			}
		}
	}
}
