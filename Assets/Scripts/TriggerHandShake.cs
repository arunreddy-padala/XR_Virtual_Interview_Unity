using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerHandShake : MonoBehaviour
{
    public GameObject Avatar;

    ShakeHandAndSitInChair shakeHandAndSitInChair;

    void Awake()
    {
        // PlayerPrefs.SetString("handshakeDone", "no");
    }

    // trigger hand shake on collision with button
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            Debug.Log("HandController entered the trigger");
            // trigger hand shake

            shakeHandAndSitInChair = Avatar.GetComponent<ShakeHandAndSitInChair>();

            shakeHandAndSitInChair.CallShakeHand();

            // disable the gameObject
            gameObject.SetActive(false);
            GameObject.Find("InterviewerContent").GetComponent<InterviewerTalks>().SetHandshakeFlag();
        }
    }
}
