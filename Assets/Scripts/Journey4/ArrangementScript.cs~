using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementScript : MonoBehaviour {
	public bool ascending, descending;

	public enum difficulty {LevelOne, LevelTwo, LevelThree};
	public difficulty diff;

	public Transform[] snapPoints;
	public Transform[] spawnPoints;
	public TextMesh[] optionTexts;

	public string[] playerAnswer;
	public string[] answerArray;
    public Transform[] boyJumpPoints;
	public Transform[] highlighter;

	public int answerCounter;
	public bool dropFlag;
    public bool jumpFlag;
    public int dropCount;
	public int tempIndex;
    public int jumpPoint;

    public Transform draggedObj;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public Manager manager;
    public GameObject boy;
    public Animator boyAnime;

	public bool ansClicked;

	void Start () 
	{
		manager = GameObject.Find ("Manager").GetComponent<Manager>();
		cam = Camera.main;

		SetStatus ();
		CreateQuestions ();
		ArrangeInOrder();
		StartCoroutine (HighligterBlink());
	}

	void Update () 
	{
		if (!manager.isGameComplete) {
			DragObject ();
		}

		ArrangeInOrder();
		BoyJump();
    }

	void SetStatus()
	{
		if (ascending) {
			dropCount = snapPoints.Length - 1;
			tempIndex =  snapPoints.Length - 1;
			boyAnime.SetTrigger("PointTop");
			jumpPoint = 5;
		} else if (descending) {
			dropCount = 0;
			tempIndex =0;
			boyAnime.SetTrigger("PointDown");
			jumpPoint = -1;
		}
	}

	public void CreateQuestions()
	{
		int firstRandomNumber = 0;

		if (diff == difficulty.LevelOne) {
			firstRandomNumber = Random.Range (0, 15);

			for (int i = 0; i < optionTexts.Length; i++) {
				optionTexts [i].text = firstRandomNumber.ToString ();
				optionTexts [i].GetComponentInParent<OriginalPos> ().indexValue = i;
				optionTexts [i].fontSize = 62;
				firstRandomNumber += 1;
			}
		} else if (diff == difficulty.LevelTwo) {
			firstRandomNumber = Random.Range (21, 94);

			for (int i = 0; i < optionTexts.Length; i++) {
				optionTexts [i].text = firstRandomNumber.ToString ();
				optionTexts [i].GetComponentInParent<OriginalPos> ().indexValue = i;
				optionTexts [i].fontSize = 52;
				firstRandomNumber += 1;
			}
		} else if (diff == difficulty.LevelThree) {
			firstRandomNumber = Random.Range (100, 994);

			for (int i = 0; i < optionTexts.Length; i++) {
				optionTexts [i].text = firstRandomNumber.ToString ();
				optionTexts [i].GetComponentInParent<OriginalPos> ().indexValue = i;
				optionTexts [i].fontSize = 42;
				firstRandomNumber += 1;
			}
		}

		ShuffleOptionsPositions ();
		CreateOptions ();
		CreateAnswerArray ();
	}

	void CreateAnswerArray()
	{
		int indexNum = optionTexts.Length - 1;
		answerArray = new string[optionTexts.Length];

		for (int i = 0; i < optionTexts.Length; i++) {
			answerArray [indexNum] = optionTexts [i].text;
			snapPoints [indexNum].GetComponent<SnapIndexValue> ().indexValue = i;
			indexNum--;
		}
	}

	public void CreateOptions()
	{
		for (int i = 0; i < spawnPoints.Length; i++) {
			Vector3 tempPos = spawnPoints [i].position;
			optionTexts [i].GetComponentInParent<BoxCollider> ().transform.position = tempPos;
		}
	}

	public void ShuffleOptionsPositions() 
	{
		if (spawnPoints.Length > 0) {
			for (int i = 0; i < spawnPoints.Length; i++) {
				Vector3 temp = spawnPoints [i].position;
				int r = Random.Range (i, spawnPoints.Length);
				spawnPoints [i].position = spawnPoints [r].position;
				spawnPoints [r].position = temp;
			}
		}
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
            if (draggedObj == null)
			{
                if (Physics.Raycast(ray, out hit, Mathf.Infinity)  && hit.collider.tag == "DraggableObject")
				{
                    draggedObj = hit.transform;
					hit.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;

					if(!draggedObj.GetComponent<OriginalPos> ().isSnapped)
						draggedObj.GetComponent<OriginalPos> ().originalPos = hit.transform.position;
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
                if (Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
					if ((hit.collider.tag == "Snap" && dropFlag && !draggedObj.GetComponent<OriginalPos> ().isSnapped))
					{
						DropAnswer ();
					} 
					else if ((hit.collider.tag != "Snap" || hit.collider.tag == "Snap") && draggedObj.GetComponent<OriginalPos> ().isSnapped) {
						ResetAnswer ();
						draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
					} 
					else {
						draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
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
		if (draggedObj.GetComponentInChildren<TextMesh> ().text == answerArray [dropCount]) {

			if (jumpPoint >= 1 && ascending && jumpPoint > dropCount) {
				jumpPoint = dropCount;
				boyAnime.SetTrigger ("Jump1");
			} else if (jumpPoint <= 3 && descending && jumpPoint < dropCount) {
				jumpPoint = dropCount;
				boyAnime.SetTrigger ("Jump1");
			}

			jumpFlag = true;

			Vector3 temp = snapPoints [dropCount].transform.position;
			temp.z -= 1f;
			draggedObj.transform.position = temp;
			playerAnswer [dropCount] = draggedObj.gameObject.GetComponentInChildren<TextMesh> ().text;
			draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue = dropCount;
			draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped = true;
				
		} else {
			boyAnime.SetTrigger ("Wrong");
			draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
		}
    }

	public void ResetAnswer()
	{
		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = false;
		dropCount = draggedObj.gameObject.GetComponent<OriginalPos>().indexValue;
		tempIndex = draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue;
		playerAnswer[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = "";
		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = 0;
	}

	public void ArrangeInOrder()
	{
        if (ascending)
		{
			for (int i = snapPoints.Length - 1; i >= 0; i--)
			{
				if(playerAnswer[i] == "")
				{
					dropFlag = true;
                    dropCount = i;
                    break;
				} 
				else {
					dropFlag = false;
                }
			}
        } else if(descending) {
			for (int i = 0; i < snapPoints.Length; i++)
			{
				if(playerAnswer[i] == "")
				{
					dropFlag = true;
					dropCount = i;
                    break;
				} 
				else {
					dropFlag = false;
				}
			}

		}
	}

    public void BoyJump()
    {
		if(descending && jumpFlag)
		{
			Vector2 tempPos = boyJumpPoints [jumpPoint].transform.position;
			tempPos.y += 1.05f;
			boy.transform.position = Vector2.MoveTowards (boy.transform.position, tempPos, 0.1f);
		} 
		else if(ascending && jumpFlag) {

			Vector2 tempPos = boyJumpPoints [jumpPoint].transform.position;
			tempPos.y += 1.05f;
			boy.transform.position = Vector2.MoveTowards (boy.transform.position, tempPos, 0.1f);
		}
	}

	IEnumerator HighligterBlink()
	{
		if (descending) {
			for (int i = 0; i < highlighter.Length; i++) {
				highlighter [i].gameObject.SetActive (true);
				yield return new WaitForSeconds (0.8f);
				highlighter [i].gameObject.SetActive (false);
			}
		} else  if (ascending){
			for (int i = highlighter.Length - 1; i >= 0; i--) {
				highlighter [i].gameObject.SetActive (true);
				yield return new WaitForSeconds (0.8f);
				highlighter [i].gameObject.SetActive (false);
			}
		}
	}
}
