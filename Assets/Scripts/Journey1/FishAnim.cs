using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnim : MonoBehaviour {

	public bool moveAround = false;
	public Transform gm1, gm2;

	public bool turn = false;

	public Animator animator;
	public RuntimeAnimatorController questionController, fishController;

	public float speed;

	public Vector3 startPos;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		startPos = transform.position;
		animator.SetTrigger ("FishIdle");
	}
	
	// Update is called once per frame
	void Update () {
		if (moveAround && gm1!=null && gm2!=null) {
			FishSwim ();
		}
	}

	public void PlayAnim(string isCorrect)
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Fish");

		if (isCorrect == "Correct") {
			for (int i = 0; i < temp.Length; i++) {
				temp [i].GetComponent<Animator> ().SetTrigger ("FishHappy");
			}
		} else if (isCorrect == "Wrong") {
			for (int i = 0; i < temp.Length; i++) {
				temp [i].GetComponent<Animator> ().SetTrigger ("FishSad");
			}
		}
	}


	void FishSwim()
	{
		if (transform.position.x>gm1.position.x) {
//			transform.position = Vector3.MoveTowards (transform.position, gm1.position, 53f * Time.deltaTime);
			//transform.Translate(Vector3.right * 20 * Time.deltaTime);
			turn = true;
		}

		else if (transform.position.x<gm2.position.x) {
//			transform.position = Vector3.MoveTowards (transform.position, gm2.position, 53f * Time.deltaTime);
//			transform.Translate(Vector3.right * 20 * Time.deltaTime);
			turn = false;
		}

		if (!turn) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
//			transform.Translate(Vector3.right * 70 * Time.deltaTime);
		}
		if (turn) {
			print ("Turn");
			transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
//			transform.Translate(-Vector3.right * 70 * Time.deltaTime);
		}
		transform.Translate(Vector3.right * speed * Time.deltaTime);
		//transform.position = Vector3.Lerp (startPos, gm1.position, Mathf.PingPong (Time.deltaTime, 4f));

	}
}
