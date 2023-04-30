using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    public AudioClip recordedClip;
    AudioSource audioSource;
    AudioListener audioListener;

    void Start()
    {


    Debug.Log("Start called");
    audioListener = Camera.main.GetComponent<AudioListener>();
    AudioListener.pause = false;
    audioSource = GetComponent<AudioSource>();
    recordedClip = Microphone.Start(null, false, 2000, 44100);
    PlayRecording();
        
    }

    void PlayRecording() {

    Debug.Log("Play recording called");
    audioSource.clip = recordedClip;
    audioSource.Play();
}


}