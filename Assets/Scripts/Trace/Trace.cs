using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour {

	public int isAnswered = 0;
	public FishManager fishManager;
	public Vector3 startpos;
	//public string[] traceCollider;
	public List<string> traceCollider;
	public List<GameObject> traceColliderObjects = new List<GameObject>(); 

	public GameObject toDrag, parent, toSpawn;
	public GameObject[] toSpawnC, toDragC;
	public Vector3[] startPosC;
	int whichCollider =0;

	Vector3 scaleVal = new Vector3 (1f, 1f, 1f);

	public TraceHelper[] helper;
	public string[] answer;
	// Use this for initialization
	void Start () {
		fishManager = GameObject.Find ("FishManager").GetComponent<FishManager>();
		startpos = toDrag.transform.position;

		for (int i = 0; i < toDragC.Length; i++) {
			startPosC [i] = toDragC [i].transform.position;
		}
		//fishManager.GetComponent<TraceDrag> ().draggedObj = toDrag;
	}
	
	// Update is called once per frame
	public void Update () {
			
	}

	public void UpdateTraceArray(string colliderName, GameObject colliderObject)
	{
		if (!colliderObject.GetComponent<TraceHelper>().hasEntered)
		{
		//	print (colliderName);
			traceCollider.Add(colliderName);
			colliderObject.GetComponent<TraceHelper> ().hasEntered = true;
		}
	}



//	public void CheckAnswer()
//	{
//		if (answer.Length > traceCollider.Count) {
//			
//		}
//		for (int i = 0; i < answer.Length; i++) {
//			if ((traceCollider [i] == answer [i])&&(traceCollider.Count>=answer.Length)) {
//				print ("Correct");
//
//				//fishManager.RewardStars (fishManager.fishQuestionNumber);
//			}else {
//				print ("Wrong");
//			}
//		}
//		fishManager.NextQuestion ();
//		fishManager.RewardStars (fishManager.fishQuestionNumber);
//	}

	public void ResetPosition()
	{
		//Destroy (toDrag);
		for (int i = 0; i < toDragC.Length; i++) {
			Destroy (toDragC[i]);
			//toDragC[i].SetActive(false);
		}

//		GameObject temp = Instantiate (toSpawn, startpos, Quaternion.identity);
//		toDrag = temp;
//		toDrag.transform.localScale = scaleVal;
//		toDrag.transform.SetParent (this.transform);
//		toDrag.GetComponent<TraceCollision> ().traceScript = this;

		for (int i = 0; i < toSpawnC.Length; i++) {
			Destroy (toDragC[i]);
			GameObject temp = Instantiate (toSpawnC[i], startPosC[i], Quaternion.identity);
			toDragC[i] = temp;
			toDragC[i].transform.localScale = scaleVal;
			toDragC [i].GetComponent<TraceCollision> ().dragObjectNo = i + 1;

			if (toDragC [i].GetComponent<TraceCollision> ().dragObjectNo == 2) {
				toDragC [i].GetComponent<TraceCollision> ().lookAtObject = 2;
			}

			toDragC[i].transform.SetParent (this.transform);
			//transform.lossyScale = scaleVal;

			toDragC[i].GetComponent<TraceCollision> ().traceScript = this;
		}
		traceCollider.Clear ();
//		traceCollider.Count = 0;
//		for (int i = 0; i < traceCollider.Count; i++) {
//			traceCollider.Remove (traceCollider[i]);
//		}
	}

}
