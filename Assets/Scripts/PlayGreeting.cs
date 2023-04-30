using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayGreeting : MonoBehaviour
{
    public Button greetingButton;

    void Start()
    {
        // Call the emulateButtonClick() method after a delay of 1 second
        Invoke("emulateGreetingButtonClick", 0.01f);
    }

    void emulateGreetingButtonClick()
    {
        // Emulate a button click
        greetingButton.onClick.Invoke();
    }
}