using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{

    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_audioSource.clip)
            _audioSource.Play();
    }
}
