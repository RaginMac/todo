using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartPositions : MonoBehaviour {

	public float startPos, pos1, pos2, pos3;
	public int count, count2, answer;
	public GameObject set1, set2, answerSet;

	public GameObject[] set1Array, set2Array, answerSetArray;

	public int answerMaxVal, answerMinVal, answerPressed ;

	public int dropCount;
	public Transform[] snapPoints;
	public Transform draggedObj;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		answer = Random.Range(answerMinVal, answerMaxVal);
//		if(PlayerPrefs.HasKey("PreviousAnswer")){							//Avoid two continuos questions with same answers//Have to clear too
//			if (answer == PlayerPrefs.GetInt ("PreviousAnswer")) {
//				answer = Random.Range (answerMinVal, answerMaxVal);
//			}
//		}
		answer = Random.Range (answerMinVal, answerMaxVal);
	//	PlayerPrefs.SetInt ("PreviousAnswer", answer);

		count = Random.Range(1, answer-1);
		count2 = answer - count;

		ShowObjects();
		SetPositions(count, set1);
		SetPositions(count2, set2);
		SetPositions(count+count2, answerSet);
	}
	
	// Update is called once per frame
	void Update () {
		DragObject ();
	}

	public void SetPositions(int c, GameObject obj){
		if(c<=3){
			startPos = pos1;
			Vector3 temp = obj.transform.position;
			temp.x = obj.transform.position.x;
			temp.y = startPos;
			temp.z = obj.transform.position.z;
			obj.transform.position = temp;
		}
		else if(c>3 &&c<6){
			startPos = pos2;
			Vector3 temp = obj.transform.position;
			temp.x = obj.transform.position.x;
			temp.y = startPos;
			temp.z = obj.transform.position.z;
			obj.transform.position = temp;
		}
		else if(c>6)
		{
			startPos = pos3;
			Vector3 temp = obj.transform.position;
			temp.x = obj.transform.position.x;
			temp.y = startPos;
			temp.z = obj.transform.position.z;
			obj.transform.position = temp;
		}
	}

	public void ShowObjects(){
		for (int i = 0; i < count; i++) {
			set1Array[i].SetActive(true);
		}
		for (int i = 0; i < count2; i++) {
			set2Array[i].SetActive(true);
		}
		for (int i = 0; i < count+count2; i++) {
			answerSetArray[i].SetActive(true);
		}
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			//print ("pressing");
			if(!draggedObj)
			{
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)  && hit.collider.tag == "DraggableObject")
				{
					//print (hit.collider.tag);
					draggedObj = hit.transform;
					hit.transform.GetComponent<AudioSource> ().Play ();
					hit.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;
				}
			}
		}

		if(draggedObj) {
			draggedObj.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObj.position.z);
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(draggedObj)
			{
				//print ("start to drop");
				if (Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
				//	print ("casting" + hit.collider.tag);
					if(hit.collider.tag == "Snap")
					{
					//	print ("drop collider" + hit.collider.tag);
						DropAnswer ();
					} else {

						//draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
						draggedObj.transform.position = draggedObj.parent.transform.position;
					}
				}
			}

			if (draggedObj != null) {
				draggedObj.gameObject.GetComponent<BoxCollider> ().enabled = true;
			}

			draggedObj = null;
		}
	}

	public void DropAnswer ()
	{
		//print ("dropping");
		Vector3 temp = snapPoints[dropCount].transform.position;
		//temp.z -= 1f;
		draggedObj.transform.position = temp;
		//playerAnswer[dropCount] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = dropCount;
		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;
		//draggedObj.gameObject.SetActive (false);
		if (dropCount < count + count2) {
			dropCount++;
		}
	}

	public void PlayNumberAudio(){
		print ("DDDD");
		this.GetComponent<AudioSource> ().Play ();
	}
}
