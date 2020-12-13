using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour{
	//How to put sound where
	//First find the sounds name in Unity Main sceen AudioManager
	//Put this command where it can be trigered to play sound
		//FindObjectOfType<AudioManager>().Play("name_of_sound");

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	[HideInInspector]
	private Sound[] pausedSounds;
	[HideInInspector]
	private int soundArrayLength = 0;


	void Awake(){
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			soundArrayLength++;
		}

		pausedSounds = new Sound[soundArrayLength];
	}

	public void Play(string sound){
		//Debug.LogWarning("Trying to play sound");
		Sound s = FindSound(sound);

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	public void Play(Sound s){
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	public void PauseSound(string sound){
		Sound s = FindSound(sound);
		s.source.Pause();
	}
	public void PauseSound(Sound s){
		s.source.Pause();
	}
	public void Stop(string sound){
		Sound s = FindSound(sound);
		s.source.Stop();
	}
	public void Stop(Sound s){
		s.source.Stop();
	}


	public void PlayBacgroundSound(){
		string startSound = "startingsound";
		Sound s = FindSound(startSound);
		float soundLength = s.clip.length;
		if(!s.source.isPlaying){
			Play(s);
			Invoke("PlayLoop", soundLength);
		}
		Play("running_sound");
	}
	private void PlayLoop(){
		string loopSound = "reapitingsound";
		Play(loopSound);
	}

	public Sound FindSound(string sound){
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null){
			Debug.LogWarning("Sound: " + name + " not found!");
			return null;
		}
		return s;
	}



	public void PauseSounds(){//pauses all plying sounds
		//Clears array
		/*for (int i = 0; i < pausedSounds.Length; i++){
				 pausedSounds[i] = null;
			}*/
		pausedSounds = new Sound[soundArrayLength];
		int i = 0;
		foreach (Sound s in sounds){
			if(s.source.isPlaying){
				s.source.Pause();
				pausedSounds[i] = s;
				i++;
			}
		}
	}
	public void ResumeSounds(){//resumes all paused sounds
		if(pausedSounds[0] != null){
			foreach (Sound s in pausedSounds){
				//Debug.Log("Name: " + s.name);
				if(s != null){
					s.source.UnPause();

				}
			}
		}
	}
	public void StopSounds(){//stops all plying sounds
		foreach (Sound s in sounds){
			if(s.source.isPlaying){
				s.source.Stop();
			}
		}
	}
}
