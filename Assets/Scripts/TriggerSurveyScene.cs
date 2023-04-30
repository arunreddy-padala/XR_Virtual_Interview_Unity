using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSurveyScene : MonoBehaviour
{

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

            SceneManager.LoadScene("SurveyScene");
        }
    }
}
