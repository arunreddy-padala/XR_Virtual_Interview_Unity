using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ReadAndDisplayValue : MonoBehaviour
{

    public InputActionReference inputReference = null;

    public TextMeshProUGUI valueText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        valueText.text = inputReference.action.ReadValue<Vector2>().ToString();
    }
}
