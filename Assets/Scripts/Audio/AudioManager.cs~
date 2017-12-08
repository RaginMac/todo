using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public string baseURL_eng = "Sounds/English/";
	public string baseURL_hin = "Sounds/Hindi/";
	public string baseURL_kan = "Sounds/Kannada/";

	public string baseURL = "Sounds/";
	public string finalAudioPath, audio;
	public AudioSource source;

	public static string language;



	public void SetAudioURL(string whichLanguage)
	{
		finalAudioPath = baseURL + whichLanguage + "/" + audio;
		PlayAudio(finalAudioPath);
	}

	public void PlayAudio(string path)
	{
		AudioClip clip = Resources.Load(path) as AudioClip;
		source.clip = clip;
		source.Play();
	}

}
