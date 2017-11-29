using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawGrid : MonoBehaviour {

	public bool instantiatePlaceValues = false;
	public bool didPlacevalueDrop = false;

	public Transform startCell, startPlaceVal, startPlaceValVert, gridParent, placeValParent;
	public Image cell, y_cell, b_cell, celPlaceVal;
	public Text number1, number2, answer;
	public int columns, rows, no1, no2, ansNumber, x, y, x1, y1;
	public int clickedAnswer;
	public List<Image> gridCells,activatedHunCells, activatedTensCells, activatedOnesCells, placeValueGridHor, placeValueGridVert ;
	//public List<int> activatedHunCells;
	public GameObject draggedObject, hunGrid, tenGridVert, tenGridHor, oneGrid;
	public Camera cam;
	public Vector3 offset;

	public int indexHundred, indexTen, indexOne, tempIndex, indexCounter;

	public int index10x10, indexVer, indexHor, index1x1;

	public int tempHori, tempVeri;
	public bool dropHorPlace = false, dropVertPlace = false, dragVerFull = false, dragHorFull = false;

	//public string latestDrop;

	void Awake(){
		CreateQuestion();

	}

	// Use this for initialization
	void Start () {

		if (!instantiatePlaceValues)
			didPlacevalueDrop = true;

		cam = Camera.main;
		//gridCells = new Image[columns*rows];
		gridCells = new List<Image>();

		InstantiateGrid(rows, columns);
		InstantiateOneColumnNRow();

		Horizontal();
		Vertical();
		CheckGrid10x10 ();
	}
	
	// Update is called once per frame
	void Update () {
		DragObject2D();
	}

	public void CreateQuestion(){
		rows = Random.Range(12, 16);
		columns = Random.Range(10, 16);
		no1 = rows;
		no2 = columns;
		ansNumber = no1 * no2;

		number1.text = no1.ToString();
		number2.text = no2.ToString();
		//startPlaceValVert.position = new Vector3(startCell.position.x - ((columns)*0.45f), startCell.position.y, 1);
	}

	public void InstantiateGrid(int rows, int columns){
		for (int j = 0; j < rows; j++) {
			for (int i = 0; i < columns; i++) {
				Image temp  = Instantiate(cell,new Vector3(startCell.position.x + (i*0.45f),startCell.position.y- (j*0.45f), 1f), Quaternion.identity, gridParent);
				temp.GetComponent<SetSnapStatus>().row = j;
				temp.GetComponent<SetSnapStatus>().column = i;
				gridCells.Add(temp);
			}
		}


	}

	public void InstantiateOneColumnNRow(){
		
		if(no2%10!=0){
			x = ((no2%10));
			y = (no2-x);
		}

		if(no1%10!=0){
			x1 = no1%10;
			y1 = no1 - x1;
		}

		if (!instantiatePlaceValues) {
			for (int i = 0; i < no2; i++) {
				if (i < y) {
					Instantiate (b_cell, new Vector3 (startPlaceVal.position.x + (i * 0.45f), startPlaceVal.position.y, 1), Quaternion.identity, placeValParent);
				} else {
					Instantiate (y_cell, new Vector3 ((startPlaceVal.position.x + (i * 0.45f)), startPlaceVal.position.y, 1), Quaternion.identity, placeValParent);
				}
			}
			for (int j = 0; j < no1; j++) {
				if (j < y1) {
					Instantiate (b_cell, new Vector3 (startPlaceValVert.position.x, startPlaceValVert.position.y - (j * 0.45f), 1), Quaternion.identity, placeValParent);
				} else {
					Instantiate (y_cell, new Vector3 ((startPlaceValVert.position.x), startPlaceValVert.position.y - (j * 0.45f), 1), Quaternion.identity, placeValParent);
				}
			}
		}
		else if (instantiatePlaceValues)
		{
			for (int i = 0; i < no2; i++) {
				Image temp = Instantiate (cell, new Vector3 (startPlaceVal.position.x + (i * 0.45f), startPlaceVal.position.y, 1), Quaternion.identity, placeValParent);
				placeValueGridHor.Add (temp);
			}
			for (int j = 0; j < no1; j++) {
				Image temp = Instantiate (cell, new Vector3 (startPlaceValVert.position.x, startPlaceValVert.position.y - (j * 0.45f), 1), Quaternion.identity, placeValParent);
				placeValueGridVert.Add (temp);
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
			//	print(hit.collider.tag);
				if (hit != null && hit.collider!=null && hit.collider.tag == "DraggableObject")
				{
					draggedObject = hit.transform.gameObject;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
					offset = draggedObject.transform.position - ray.origin;

					if (didPlacevalueDrop) {
						CheckDropIndex (draggedObject.gameObject.name, draggedObject.gameObject);
					}
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

//				if((hit.collider!=null && hit.collider.tag=="Snap2" && !hit.collider.GetComponent<SetSnapStatus>().isSnapped)){
//					DropObjects(hit.collider.transform, draggedObject.transform);
//				}

				if(hit.collider!=null && hit.collider.tag=="Snap" && !hit.collider.GetComponent<SetSnapStatus>().isSnapped)
				{
					DropObjects(hit.collider.transform, draggedObject.transform);

					if (didPlacevalueDrop) {
						Horizontal ();
						Vertical ();
						CheckGrid10x10 ();
					}
					
				}
				else if(hit.collider==null || hit.collider.tag!="Snap" || hit.collider.GetComponent<SetSnapStatus>().isSnapped){
					draggedObject.transform.position = draggedObject.gameObject.GetComponent<OriginalPos> ().originalPos;
				}

				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				draggedObject = null;

			}
		}
	}
//
	public void DropObjects(Transform hit, Transform drag) {
		

		if ((!didPlacevalueDrop)) {
			
			print ("hit");
			drag.transform.position = drag.gameObject.GetComponent<OriginalPos> ().originalPos;

			if (drag.gameObject.name == "10gridHor" && !dropHorPlace && !dragHorFull && !dropVertPlace) {
				dropHorPlace = true;
				for (int j = 0; j < y; j++) {
					placeValueGridHor [j].transform.GetChild (1).gameObject.SetActive (true);
					tempHori = j;
				}
			}

			if (drag.gameObject.name == "10gridVert" && !dropVertPlace && !dropHorPlace && !dragVerFull) {
				dropVertPlace = true;
				for (int i = 0; i < y1; i++) {
					placeValueGridVert [i].transform.GetChild (1).gameObject.SetActive (true);
					tempVeri = i;
				}
			}

			if (drag.gameObject.name == "1grid" && dropHorPlace && !dragHorFull) {
				tempHori++;
				placeValueGridHor [tempHori].transform.GetChild (2).gameObject.SetActive (true);
				if (tempHori >= columns-1) {
					dragHorFull = true;
					dropHorPlace = false;
				}
			}

			if (drag.gameObject.name == "1grid" && dropVertPlace && !dragVerFull ) {
				tempVeri++;
				placeValueGridVert [tempVeri].transform.GetChild (2).gameObject.SetActive (true);
				if (tempVeri >= rows-1) {
					dragVerFull = true;
					dropVertPlace = false;
				}
			}

			if(dragHorFull && dragVerFull)
			{
				didPlacevalueDrop = true;
			}
		}



		if (didPlacevalueDrop) 
		{
			drag.transform.position = drag.gameObject.GetComponent<OriginalPos> ().originalPos;
			int temp = index10x10;
			if (drag.gameObject.name == "100grid" && index10x10 >= 0) {
				for (int j = 0; j < 10; j++) {
					for (int i = 0; i < 10; i++) {
						gridCells [temp + i].transform.GetChild (0).gameObject.SetActive (true);
						activatedHunCells.Add (gridCells [temp + i].transform.GetChild (0).GetComponent<Image> ());
						//	gridCells[indexHundred+i].GetComponent<BoxCollider2D>().enabled = false;
						gridCells [temp + i].GetComponent<SetSnapStatus> ().isSnapped = true;
					}

					temp += columns;
				}
			}
			if (drag.gameObject.GetComponent<OriginalPos> ().isDroppabble && drag.gameObject.name == "10gridVert" && indexVer >= 0) {
				//print(indexTen);
				for (int i = 0; i < 10; i++) {
					gridCells [indexVer + (i * columns)].transform.GetChild (1).gameObject.SetActive (true);
					gridCells [indexVer + (i * columns)].GetComponent<SetSnapStatus> ().isSnapped = true;
					activatedHunCells.Add (gridCells [indexVer + (i * columns)].transform.GetChild (1).GetComponent<Image> ());
					//gridCells[indexHundred+i].GetComponent<BoxCollider2D>().enabled = false;
					//drag.gameObject.GetComponent<OriginalPos>().isDroppabble = false;
				}
			}
			if (drag.gameObject.GetComponent<OriginalPos> ().isDroppabble && drag.gameObject.name == "10gridHor" && indexHor >= 0) {
				//print(indexTen);
				for (int i = 0; i < 10; i++) {
					gridCells [indexHor + i].transform.GetChild (1).gameObject.SetActive (true);
					activatedHunCells.Add (gridCells [indexHor + i].transform.GetChild (1).GetComponent<Image> ());
					//gridCells[indexTen+i].GetComponent<BoxCollider2D>().enabled = false;
					gridCells [indexHor + i].GetComponent<SetSnapStatus> ().isSnapped = true;
				}

			}

			if (drag.gameObject.GetComponent<OriginalPos> ().isDroppabble && drag.gameObject.name == "1grid" && index1x1 >= 0) {
				gridCells [index1x1].transform.GetChild (2).gameObject.SetActive (true);
				gridCells [index1x1].GetComponent<SetSnapStatus> ().isSnapped = true;
				activatedHunCells.Add (gridCells [index1x1].transform.GetChild (2).GetComponent<Image> ());
			}
		}


	}
//

	void CheckDropIndex(string obj, GameObject dragObj){
		if(obj=="10gridHor"){
			for (int i = 0; i < gridCells.Count; i++)
			{
				if(gridCells[i].GetComponent<SetSnapStatus>().horizontalDrop)
				{
					indexHor = i;
					dragObj.GetComponent<OriginalPos>().isDroppabble = true;
					//print(i);
					break;
				} else{
					//go back
				}
			}

		}

		if(obj=="10gridVert") {
				for (int i = 0; i < gridCells.Count; i++) {
				if(gridCells[i].GetComponent<SetSnapStatus>().verticalDrop)
				{
					indexVer = i;
					dragObj.GetComponent<OriginalPos>().isDroppabble = true;
					break;
				} else{
					//go back
				}
			}
		}

		if(obj=="1grid"){
			for (int i = 0; i < gridCells.Count; i++) {
				if(!gridCells[i].GetComponent<SetSnapStatus>().isSnapped)
				{
					index1x1 = i;
					dragObj.GetComponent<OriginalPos>().isDroppabble = true;
					break;
				}
			}
		}

		if(obj=="100grid"){
			for (int i = 0; i < gridCells.Count; i++) {
				if(gridCells[i].GetComponent<SetSnapStatus>().grid10x10)
				{
					index10x10 = i;
					dragObj.GetComponent<OriginalPos>().isDroppabble = true;
					break;
				}
			}
		}
	}

	public void Horizontal()
	{
		for (int i = 0; i < gridCells.Count - 10; i++)
		{
			int tempi = i;
			for (int j = 0; j < 10 ; j++)
			{
				if(gridCells[i].GetComponent<SetSnapStatus>().row == gridCells[tempi].GetComponent<SetSnapStatus>().row && !gridCells[tempi].GetComponent<SetSnapStatus>().isSnapped)
				{
					gridCells[i].GetComponent<SetSnapStatus>().horizontalDrop = true;
					tempi += 1;
				} else{
					gridCells[i].GetComponent<SetSnapStatus>().horizontalDrop = false;
				}
			}
		}
	}


	public void Vertical()
	{
		int gridsConsidered = 0;
		if (columns < 10 || rows < 10)
			gridsConsidered = columns;
		else
			gridsConsidered = ((rows - 9) * columns);

		for (int i = 0; i < gridsConsidered; i++)
		{
			int tempi = i;

			for (int j = 0; j < 10 ; j++)
			{
				if(gridCells[i].GetComponent<SetSnapStatus>().column == gridCells[tempi].GetComponent<SetSnapStatus>().column && !gridCells[tempi].GetComponent<SetSnapStatus>().isSnapped)
				{
					gridCells [i].GetComponent<SetSnapStatus> ().verticalDrop = true;
					tempi += columns;
				} else {
					gridCells[i].GetComponent<SetSnapStatus>().verticalDrop = false;
				}
			}
		}
	}


	public void CheckGrid10x10()
	{
		for (int j = 0; j < gridCells.Count; j++) {
			if(gridCells[j].GetComponent<SetSnapStatus>().verticalDrop && gridCells[j].GetComponent<SetSnapStatus>().horizontalDrop)
			{
				gridCells[j].GetComponent<SetSnapStatus>().grid10x10 = true;
			} else{
				gridCells[j].GetComponent<SetSnapStatus>().grid10x10 = false;
			}
		}
	}

	public void ClearGrid(){
		for (int i = 0; i < activatedHunCells.Count; i++) {
			activatedHunCells[i].gameObject.SetActive(false);

		}
		for (int i = 0; i < gridCells.Count; i++) {
			gridCells[i].GetComponent<SetSnapStatus>().isSnapped = false;
		}
		activatedHunCells.Clear();
		index10x10 = -1;
		indexVer = -1;
		indexHor = -1;
		index1x1 = -1;

		Horizontal();
		Vertical();
		CheckGrid10x10 ();

	}

}
