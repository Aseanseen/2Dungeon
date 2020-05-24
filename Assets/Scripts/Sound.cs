using UnityEngine.Audio;
using UnityEngine;

// When creating a new class and want it to appear need to mark as serializable
[System.Serializable]
public class Sound
{
	public string name;
    public AudioClip clip;

    // Adds sliders for volume and pitch
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;
    public bool loop;
    // Do not want this to show in inspector but is public
    [HideInInspector]
    public AudioSource source;
}
