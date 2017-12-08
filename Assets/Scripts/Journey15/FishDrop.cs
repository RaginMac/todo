using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishDrop : MonoBehaviour {	//j15

	public enum QuestionType {withoutRemainder, withRemainder};
	public QuestionType qType;

	public Transform[] spawnPoints, dropBowls, bowlFishes;
	public GameObject objectToSpawn;

	public int noToSpawn, dropIndex, answer, answerInPot, clickedAns, clickedRemainder;

	public GameObject draggedObject;
	public Camera cam;
	public Vector3 offset;

	public Sprite wrong, correct;
	public Image checkButton;

	// Use this for initialization
	void Start () {
		GameObject.Find("Keypad").GetComponent<KeyPadShow>().isPlaying = 0;
		cam = Camera.main;
		ShuffleSpawns();
		SpawnFishes();
	}
	
	// Update is called once per frame
	void Update () {
		if(!Manager.Instance.isGameComplete){
			DragObject2D();
		}
	}

	public void ShuffleSpawns(){
		if (spawnPoints.Length > 0) {
			for (int i = 0; i < spawnPoints.Length; i++) {
				Transform temp = spawnPoints [i];
				int r = Random.Range (i, spawnPoints.Length);
				spawnPoints [i] = spawnPoints [r];
				spawnPoints [r] = temp;
			}
		}
	}


	public void SpawnFishes(){
		if(qType==QuestionType.withRemainder){
			noToSpawn = Random.Range(3, 9);	
			answer = noToSpawn % 3;
			SetDropLimits();
		}
		else if(qType==QuestionType.withoutRemainder){
			answer = noToSpawn/3;
		}
		for (int i = 0; i < noToSpawn; i++) {
			spawnPoints[i].gameObject.SetActive(true);
		}
	}

	public void SetDropLimits(){
		for (int i = 0; i < dropBowls.Length; i++) {
			dropBowls[i].GetComponent<DropLimit>().dropLimit = (noToSpawn-(noToSpawn%3))/3;
		}
		answerInPot = (noToSpawn-(noToSpawn%3))/3;
	}

	public void DragObject2D()
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0))
		{
			if (!draggedObject)
			{
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);

				if (hit != null && hit.collider!=null && hit.collider.tag == "DraggableObject")
				{
					print(hit.collider.tag);
					draggedObject = hit.transform.gameObject;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
					offset = draggedObject.transform.position - ray.origin;
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

				if(hit.collider!=null && hit.collider.tag=="Snap")
				{
					DropObjects(hit.collider.transform, draggedObject.transform);
				}
				else if(hit.collider==null || hit.collider.tag!="Snap")
				{
					draggedObject.transform.position = draggedObject.gameObject.GetComponent<OriginalPos> ().originalPos;
				}


				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				draggedObject = null;

			}
		}
	}

	void DropObjects(Transform hit, Transform drag){
		dropIndex++;
		drag.transform.position = drag.gameObject.GetComponent<OriginalPos> ().originalPos;
		drag.gameObject.SetActive(false);
		int temp = hit.GetComponent<DropLimit>().noOfFish;
		hit.GetChild(temp).gameObject.SetActive(true);
		temp++;
		hit.GetComponent<DropLimit>().noOfFish = temp;
		if(temp>=hit.GetComponent<DropLimit>().dropLimit){
			hit.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	public void ShowFishAnim(bool isCorrect){
		if(isCorrect){
			checkButton.sprite = correct;
			GameObject.Find("Keypad").GetComponent<Animator>().SetTrigger("HideKeypad");
			return;
		}else{
			checkButton.sprite = wrong;

		}

		for (int i = 0; i < bowlFishes.Length; i++) {
			if(isCorrect){
				print("correct");
				bowlFishes[i].GetComponent<Animator>().SetTrigger("FishHappy");
			}
			else{
				bowlFishes[i].GetComponent<Animator>().SetTrigger("FishSad");
			}
		}
	}

}
