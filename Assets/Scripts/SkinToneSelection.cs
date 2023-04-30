using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinToneSelection : MonoBehaviour
{
    private float selectedColorR = 228.0f;
    private float selectedColorG = 225.0f;
    private float selectedColorB = 166.0f;

    public void selectSkinTone1() {
        selectedColorR = 228.0f;
        selectedColorG = 225.0f;
        selectedColorB = 166.0f;
    }

    public void selectSkinTone2() {
        selectedColorR = 250.0f;
        selectedColorG = 221.0f;
        selectedColorB = 192.0f;
    }

    public void selectSkinTone3() {
        selectedColorR = 174.0f;
        selectedColorG = 129.0f;
        selectedColorB = 87.0f;
    }

    public void selectSkinTone4() {
        selectedColorR = 158.0f;
        selectedColorG = 113.0f;
        selectedColorB = 88.0f;
    }

    public void selectSkinTone5() {
        selectedColorR = 96.0f;
        selectedColorG = 79.0f;
        selectedColorB = 69.0f;
    }

}
