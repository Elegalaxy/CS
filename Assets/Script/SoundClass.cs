using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundClass
{
    public string name; //Name of this obj
    
    public AudioClip clip; //Audio

    [Range(0f,1f)] //Volume slider
    public float volume;

    [Range(.1f, 3f)] //Pitch slider
    public float pitch;

    public bool loop;

    [HideInInspector] //Placeholder for audio source
    public AudioSource source;
}
