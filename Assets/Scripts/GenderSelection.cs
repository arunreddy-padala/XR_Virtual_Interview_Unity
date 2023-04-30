using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderSelection : MonoBehaviour
{
    private string selectedGender = "female";

    public void selectMale() {
        selectedGender = "male";
        Debug.Log("Selected gender: "+selectedGender);
    }

    public void selectFemale() {
        selectedGender = "female";
        Debug.Log("Selected gender: "+selectedGender);
    }
}
