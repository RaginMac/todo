using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScript : MonoBehaviour {

	public Subtraction subScript;
	public bool exchangePanel;
	public bool counterPanel100, counterPanel10, counterPanel1;
	public Animator exchangePanelAnimation;

	public int counter;

	// Use this for initialization
	void Start () {
		if (exchangePanel) 
			exchangePanelAnimation = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (exchangePanel && subScript.subWithBorrow) {
			Exchange ();
		} else if (counterPanel100 || counterPanel10 || counterPanel1) {
			//Counter ();
		}
	}

	void Exchange ()
	{
		if (subScript.exchange10) {
			exchangePanelAnimation.SetBool ("xchange10", true);
		} else if (!subScript.exchange10) {
			exchangePanelAnimation.SetBool ("xchange10", false);
		}

		if (subScript.exchange100) {
			exchangePanelAnimation.SetBool ("xchange100", true);
		} else if (!subScript.exchange100) {
			exchangePanelAnimation.SetBool ("xchange100", false);
		}
	}

	void Counter()
	{
		if (counterPanel1 && counter >= subScript.thirdDigit) {
			this.GetComponent<BoxCollider2D> ().enabled = false;

		} else if (counterPanel10 && counter >= subScript.secondDigit) {
			this.GetComponent<BoxCollider2D> ().enabled = false;

		} else if (counterPanel100 && counter >= subScript.firstDigit) {
			this.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

	public void ResetCounters()
	{
		counter = 0;
		//subScript.CheckCounter ();
	}

}
