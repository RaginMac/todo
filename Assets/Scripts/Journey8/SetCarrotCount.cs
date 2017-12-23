using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetCarrotCount : MonoBehaviour {

	public GameObject[] carrots, rabbitCarrots;
	public List<GameObject> newCarrots;
	public Text number1, number2, answerText;
	public string clickedAnswer;

	public int no1, no2;
	public string answer;

	public int dropCount;
	//public Transform[] snapPoints;
	public Transform draggedObj, parent;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public AudioSource source;
	public AudioSource carrotBite;

	public AudioClip[] clips;
	public string[] audioClips;
	public int isAnswered = 0;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		CreateQuestions ();
		DisplayCarrots ();
//		PlayerPrefs.SetString("Language", "Sounds/English/");
		source.clip = Resources.Load(PlayerPrefs.GetString("Language") + audioClips[no2-1]) as AudioClip;  //clips [no2 - 1];
		Invoke ("PlayRabbitAudio", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!Manager.Instance.isGameComplete){
			DragObject ();
		}
	}

	void PlayRabbitAudio(){
		if (Manager.Instance.noOfQuestionsAnswered <= Manager.Instance.totalNoOfQuestions) {
			source.Play ();
		} else if(Manager.Instance.noOfQuestionsAnswered > Manager.Instance.totalNoOfQuestions){
			source.Stop ();
		}
	}
	void CreateQuestions(){
		no1 = Random.Range (1, 9);
		no2 = Random.Range (1, no1);


		number1.text = no1.ToString ();
		number2.text = no2.ToString ();
		answer = (no1 - no2).ToString ();
	}

	public void DisplayCarrots(){
		for (int i = 0; i < no1; i++) {
			carrots [i].SetActive (true);
		}
		for (int j = 0; j < no2; j++) {
			rabbitCarrots [j].SetActive (true);			
		}
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			//ArrangeInOrder ();
			//print ("pressing");
			if(!EventSystem.current.IsPointerOverGameObject())
			{
				if(!draggedObj)
				{
					if(Physics.Raycast(ray, out hit, Mathf.Infinity)  && hit.collider.tag == "DraggableObject")
					{
						draggedObj = hit.transform;
						hit.transform.GetComponent<BoxCollider>().enabled = false;
						offset = draggedObj.position - ray.origin;
					}
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
					if(hit.collider.tag == "Snap")
					{
                       // hit.transform.GetComponent<BoxCollider>().enabled = false;
						draggedObj.transform.SetParent(hit.collider.transform);
					
						DropAnswer (hit.collider, draggedObj.gameObject);

					} else {
								
						draggedObj.transform.GetComponent<BoxCollider>().enabled = true;
						draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;

						//draggedObj.transform.SetParent (parent);
					}
				}
			}

			draggedObj = null;
		}
	}

	public void DropAnswer (Collider dropCol, GameObject drag)
	{
		newCarrots.Add(drag);
		//draggedObj.GetComponent<Animator>().SetTrigger("CarrotBite");
		Vector3 temp = rabbitCarrots[dropCount].transform.position;
		temp.z -= 1f;
		draggedObj.transform.position = temp;
		if (dropCount < no2-1) {
			dropCount++;
		}else{
			dropCol.enabled = false;
		}
		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = dropCount;
		//finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;
		CheckIfAllDragged();

	}

	public void CheckIfAllDragged()
	{
		if(newCarrots.Count>=no2)
		{
			for (int i = 0; i < newCarrots.Count; i++) {
				newCarrots[i].GetComponent<Animator>().SetTrigger("CarrotBite");
			}

			carrotBite.Play ();
		}
	}

}
