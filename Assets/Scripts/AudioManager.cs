using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	// Static reference to current instance of AudioManager that is present in scene
	public static AudioManager instance;
	public AudioMixerGroup masterMixer;
	// Similar to Start() but goes before it
	void Awake(){

		// Checks if there an audiomanager already exists, ensures no duplicate audiomanagers
		if(instance == null){
			instance = this;
		}
		else{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		// Loop through sounds in array and add an audio source component to the sound
		foreach (Sound s in sounds){
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup = masterMixer;
		}
	}

	void Start(){
		Play("ThemeSong");
	}
    
    public void Play(String name){
    	// Find sound in the sounds array given the name
    	Sound s = Array.Find(sounds, sound => sound.name == name);
    	if (s == null){
    		Debug.LogWarning("Sound: " + name + " not found!");
    		return;
    	}
    	s.source.Play();
    }
}
