using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {
	public AudioSource click;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GoToLevel(string levelName)
	{
		click.Play ();
		SceneManager.LoadScene (levelName);
	}
}
