using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public SoundClass[] musicList; //Array for sounds
    public static SoundManager instance;

    private void Awake() {
        if(instance == null) instance = this; //If no audio manager, this audio manager will be the only one
        else {
            Destroy(gameObject); //If yes, destroy this one
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(SoundClass s in musicList) { //Add audio source to all sounds and initialize the clip, volume and pitch
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume; //Set values on audio sources same as inspector
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        //play("BGM"); //Play BGM on start, change the name
    }

    public void play(string name) { //Play audio by name
        SoundClass s = Array.Find(musicList, musicList => musicList.name == name); //Find the sound
        if(s == null) {
            Debug.LogWarning(name + "Not Found!"); //Warning if sound not found
            return;
        }
        s.source.Play(); //Play the sound
    }
}
