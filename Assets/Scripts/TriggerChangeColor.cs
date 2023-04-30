using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerChangeColor : MonoBehaviour
{

    public InputActionReference _triggerChangeColorReference;
    // Start is called before the first frame update

    // game object sphere reference
    public GameObject _wandSphere;
    public AudioSource _audioSource;


    void Awake()
    {
        _triggerChangeColorReference.action.started += ChangeColor;
    }

    void Start()
    {

        _audioSource = GetComponent<AudioSource>();

    }

    private void ChangeColor(InputAction.CallbackContext context)
    {
        Debug.Log("trigger pressed");
        // gameObject.SetActive(false);
        if (gameObject.GetComponent<XRGrabInteractable>().isSelected)
        {
            Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            _wandSphere.GetComponent<MeshRenderer>().material.color = newColor;

            // play clip if found
            if (_audioSource.clip)
                _audioSource.Play();


        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
