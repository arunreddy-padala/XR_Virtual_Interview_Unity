using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class DestroyObject : MonoBehaviour
{
    public InputActionReference _destroyReference;
    public ParticleSystem _destroyEffectReference;
    public AudioClip explosionSound;

    private ParticleSystem explosionEffect;
    private AudioSource audioSource;
    float timer;
    float duration = 1f; // duration of the particle explosion, in seconds

    // Start is called before the first frame update
    void Awake()
    {
        _destroyReference.action.started += Destroyed;
    }

    void Start() // add this method
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (explosionEffect != null)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                explosionEffect.Stop();
                timer = 0f;
            }
        }
    }

    private void Destroyed(InputAction.CallbackContext context)
    {
        audioSource.clip = explosionSound; // play the explosion sound
        audioSource.Play();
        if (gameObject.GetComponent<XRGrabInteractable>().isSelected)
        {
            gameObject.SetActive(false);

            if (!gameObject.activeSelf)
            {
                explosionEffect = Instantiate(_destroyEffectReference) as ParticleSystem;
                explosionEffect.transform.position = transform.position;
                //play it
                explosionEffect.loop = false;
                explosionEffect.Play();
                timer = 0f;

            }
        }
    }
}