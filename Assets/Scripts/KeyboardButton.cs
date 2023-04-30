using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardButton : MonoBehaviour
{
    // tmp pro input field
    public TMP_InputField TextField;

    public void alphabetFunction(string alphabet)
    {

        Debug.Log("alphabetFunction clicked");

        TextField.text = TextField.text + alphabet;

    }

    public void BackSpace()
    {

        if (TextField.text.Length > 0) TextField.text = TextField.text.Remove(TextField.text.Length - 1);

    }
}
