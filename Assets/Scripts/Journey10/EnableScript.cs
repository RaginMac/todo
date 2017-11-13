using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScript : MonoBehaviour {

	public Subtraction subScript;
	public bool exchangePanel;
	public bool counterPanel;
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
		} else if (counterPanel) {
			Counter ();
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
		} else if (!subScript.exchange10) {
			exchangePanelAnimation.SetBool ("xchange100", false);
		}
	}

	void Counter()
	{
		if (counter >= 9) {
			this.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

}
