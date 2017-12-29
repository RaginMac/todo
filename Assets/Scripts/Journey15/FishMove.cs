using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour {

	public bool turn;
	public Transform gm1, gm2;
	public float speed;
	public float[] speeds;
	// Use this for initialization
	void Start () {
		speed = speeds [Random.Range (0, speeds.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		FishSwim ();
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
