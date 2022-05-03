using UnityEngine.Audio;
using UnityEngine;

//Listing parameters for sound  clips
[System.Serializable]
public class AudioSound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
