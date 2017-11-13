using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementScript : MonoBehaviour {
	public bool ascending, descending;

	public Transform[] snapPoints;
	public string[] playerAnswer;
	public string[] answerArray;
    public Transform[] boyJumpPoints;
	public Transform[] highlighter;

	public int answerCounter;
	public bool dropFlag;
    public bool jumpFlag;
    public int dropCount;
    public int jumpPoint;

    public Transform draggedObj;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public Manager manager;
    public GameObject boy;
    public Animator boyAnime;

	public bool ansClicked;

	void Start () {
		manager = GameObject.Find ("Manager").GetComponent<Manager>();
		cam = Camera.main;
		ArrangeInOrder();
        if (ascending)
        {
            dropCount = snapPoints.Length - 1;
            boyAnime.SetTrigger("PointTop");
			jumpPoint = 5;
        }

		if (descending) {
			dropCount = 0;
			boyAnime.SetTrigger("PointDown");
			jumpPoint = -1;
		}

		StartCoroutine (HighligterBlink());
	}

	void Update () {
		

		DragObject ();
		ArrangeInOrder();

		BoyJump();
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
					if ((hit.collider.tag == "Snap" && dropFlag && !draggedObj.GetComponent<OriginalPos> ().isSnapped)) {
						
						if (jumpPoint >= 1 && ascending  && jumpPoint > dropCount) {
							jumpPoint = dropCount;
							boyAnime.SetTrigger ("Jump1");
						} else if (jumpPoint <= 3 && descending  && jumpPoint < dropCount) {
							jumpPoint = dropCount;
							boyAnime.SetTrigger ("Jump1");
						}

						DropAnswer ();
					} 
					else if ((hit.collider.tag != "Snap" || hit.collider.tag == "Snap") && draggedObj.GetComponent<OriginalPos> ().isSnapped) 
					{
						ResetAnswer ();
						draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
					} else 
					{
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
        Vector3 temp = snapPoints[dropCount].transform.position;
		temp.z -= 1f;
		draggedObj.transform.position = temp;
		playerAnswer[dropCount] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = dropCount;
        draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;

		jumpFlag = true;
    }

	public void ResetAnswer()
	{
			draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = false;
			dropCount = draggedObj.gameObject.GetComponent<OriginalPos>().indexValue;
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
            
           
        }

		if(descending)
		{
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
			boy.transform.position = Vector2.MoveTowards (boy.transform.position, tempPos, 0.06f);
		} 
		else if(ascending && jumpFlag) {

			Vector2 tempPos = boyJumpPoints [jumpPoint].transform.position;
			tempPos.y += 1.05f;
			boy.transform.position = Vector2.MoveTowards (boy.transform.position, tempPos, 0.06f);
			//jumpFlag = false;
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
		}else if (ascending) {
			for (int i = highlighter.Length - 1; i >= 0; i--) {
				highlighter [i].gameObject.SetActive (true);
				yield return new WaitForSeconds (0.8f);
				highlighter [i].gameObject.SetActive (false);
			}
		}
	}
}
