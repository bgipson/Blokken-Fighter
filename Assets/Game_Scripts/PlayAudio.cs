using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {
    //Plays an Audio Clip for an Animation
    public void PlayClip(AudioClip clip) {
        GameObject a = new GameObject("clip");
        AudioSource source = a.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();

        TimedDestroy destroyer = a.AddComponent<TimedDestroy>();
        destroyer.startOnAwake = true;
        destroyer.time = clip.length;
    }
}
