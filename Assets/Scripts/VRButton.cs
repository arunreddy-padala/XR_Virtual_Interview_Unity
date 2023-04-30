using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRButton : MonoBehaviour
{
    public int flag;
    public GameObject button;
    public GameObject interviewerContent;
    AudioSource sound;
    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        originalPosition = button.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            Vector3 newPosition;
            if (flag == 1)
            {
                newPosition = new Vector3(originalPosition.x, originalPosition.y - 0.01f, originalPosition.z);
            }
            else
            {
                newPosition = new Vector3(originalPosition.x, originalPosition.y - 0.005f, originalPosition.z);
            }
            button.transform.position = newPosition;
            sound.Play();

            if (flag == 1)
            {
                // set flag in interviewer talks script to 1, to indicate to stop recording
                interviewerContent.GetComponent<InterviewerTalks>().SetStopRecordingFlag();
            }
            else if (flag == 2)
            {
                // increment next panel flag in feedback scene
                interviewerContent.GetComponent<InterviewerFeedbackTalks>().SetNextPanelFlag();
            }
            else if (flag == 3)
            {
                // increment next panel flag in feedback scene
                interviewerContent.GetComponent<InterviewerFeedbackTalks>().SetPreviousPanelFlag();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            button.transform.position = originalPosition;
        }
    }

    // public void CallRecordingService()
    // {
    //     if (!isRecording)
    //     {
    //         // MimicButtonClick();
    //         isRecording = true;
    //     }

    //     else
    //     {
    //         // MimicButtonUnclick();
    //         isRecording = false;
    //     }
    // }

    // private void OnButtonClick()
    // {
    //     // Toggle button state
    //     recoButton.interactable = !recoButton.interactable;
    // }

    // // Call this method to mimic a button click
    // public void MimicButtonClick()
    // {
    //     recoButton.interactable = true;
    //     OnButtonClick();
    // }

    // // Call this method to mimic a button unclick
    // public void MimicButtonUnclick()
    // {
    //     recoButton.interactable = false;
    //     OnButtonClick();
    // }
}
