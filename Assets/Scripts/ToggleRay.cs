using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// to be attached to the controller for which you want to toggle RayInteractor
/// switches between RayInteractor and DirectInteractor
/// </summary>
public class ToggleRay : MonoBehaviour
{
    // define a public InputActionReference for toggle button
    public InputActionReference toggle_reference;

    // and a reference to the rayInteractor GameObject to be toggled

    public GameObject ray_interactor;

    XRDirectInteractor _direct_interactor;


    // need a global variable for the XRDirectInteractor reference
    void Awake()
    {

        toggle_reference.action.started += Pressed;

        toggle_reference.action.canceled += Released;

        // add Pressed and Released events to action's .started and .canceled states
        // get a reference to the XRDirectInteractor attached to current gameObject
        _direct_interactor = GetComponent<XRDirectInteractor>();
    }

    private void OnDestroy()
    {
        // remove event listeners when destroyed
    }

    void Pressed(InputAction.CallbackContext context)
    {
        // toggle the Ray 
        Toggle();
    }

    void Released(InputAction.CallbackContext context)
    {
        // toggle the Ray
        Toggle();
    }

    void Toggle()
    {

        // get a bool, isToggled, for the current state of the rayInteractor
        bool isToggled = ray_interactor.activeSelf;


        // setActive of the rayInteractor based on the bool value
        if (!isToggled)
        {
            _direct_interactor.enabled = false;
            ray_interactor.SetActive(true);
        }
        else
        {
            _direct_interactor.enabled = true;
            ray_interactor.SetActive(false);
        }
        // set enable of the directInteractor based on the bool value
        Debug.Log("toggled");
    }
}
