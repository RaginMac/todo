using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour {
	public void DropComplete(){
		this.gameObject.SetActive(false);
	}
}
