using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public bool PlayAudioFromManager = false, repeatQuestion = false;

//	public string baseURL_eng = "Sounds/English/";
//	public string baseURL_hin = "Sounds/Hindi/";
//	public string baseURL_kan = "Sounds/Kannada/";

	public string baseURL = "Sounds/";
	public string finalAudioPath;
	public AudioSource source;
	public AudioSource source2;

	public AudioClip bubbles;
	public AudioClip rabbit; 

	public string sound;

	[SerializeField]
	private AudioClip clip;

	void Awake()
	{
		if(PlayAudioFromManager && PlayerPrefs.HasKey("Language"))
		{
			PlayAudio(PlayerPrefs.GetString("Language"));
		}
	}

	void Start()
	{
		if (bubbles != null) {
			source2.clip = bubbles;
			InvokeRepeating ("PlayBubbleAudio", 2f, 10f);
		}

		if (rabbit != null) {
			InvokeRepeating ("PlayRabbitAudio", 2f, 10f);
		}
	}

	void Update()
	{
		
	}

	public void SetAudioURL(string whichLanguage = "English")
	{
		finalAudioPath = baseURL + whichLanguage + "/";

		PlayerPrefs.SetString("Language", finalAudioPath);			//save path of audio files for loadin

		//PlayAudio(finalAudioPath);
	}

	public void PlayAudio(string path)
	{
		//AudioClip clip = Resources.Load(path) as AudioClip;
		source.clip = Resources.Load(path+ sound) as AudioClip;
		clip = source.clip;
		source.Play();
		if (source.clip != null) {
			Invoke ("ClearSource", source.clip.length);
		}
	}

	public void ClearSource()
	{
		source.clip = null;
	}

	void CheckIfClipEnded()
	{
		float progress = Mathf.Clamp01(source.time/source.clip.length);

		if(progress==1)
		{
			print("Clip has ended");
		}
	}

	public void PlayAudioAgain()
	{
		if(repeatQuestion && !source.isPlaying)
		{
			source.clip = clip;
			source.Play();
		}

		Manager.Instance.PlayClickAudio ();
	}

	void PlayBubbleAudio()
	{
		print ("bubbles");
//		source2.clip = Manager.Instance.bubblesAudio;
		source2.Play ();
	}

	void PlayRabbitAudio()
	{
		source2.clip = rabbit;
		source2.Play ();
	}
}
