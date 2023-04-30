using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class preferenceSelection : MonoBehaviour
{
    
    private string role;
    private string interviewer_gender;
    private Color interviewer_skin_tone_color;
    private Color skin_tone_color;
    private float selectedColorR;
    private float selectedColorG;
    private float selectedColorB;
    private string skinTextureName;

    private float selectedColorRI;
    private float selectedColorGI;
    private float selectedColorBI;

    private int darkSkinInterviewer;

    private ToggleGroup roleGroup;
    private ToggleGroup genderGroup;
    private ToggleGroup skinTypeGroup;
    private ToggleGroup interviewerSkinTypeGroup;
    // Start is called before the first frame update
    void Start()
    {
        // role = "Software Development Engineer";
        // interviewer_gender = "female";
        // float selectedColorR = 96.0f;
        // float selectedColorG = 79.0f;
        // float selectedColorB = 69.0f;
        // skin_tone_color = new Color(selectedColorR, selectedColorG, selectedColorB, 1);

        roleGroup = GameObject.Find("RoleToggleGroupPanel").GetComponent<ToggleGroup>();
        genderGroup = GameObject.Find("GenderToggleGroupPanel").GetComponent<ToggleGroup>();
        skinTypeGroup = GameObject.Find("SkinTypeToggleGroupPanel").GetComponent<ToggleGroup>();
        interviewerSkinTypeGroup = GameObject.Find("SkinTypeToggleGroupPanel (1)").GetComponent<ToggleGroup>();
        darkSkinInterviewer = 0;

        Debug.Log(roleGroup);
        Debug.Log(genderGroup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getSelectedToggles() {

        foreach(Toggle t in genderGroup.ActiveToggles()) {
            if (t.isOn) {
                if (t.name == "ToggleFemale") {
                    interviewer_gender = "female";
                }
                else {
                    interviewer_gender = "male";
                }
            }
        }
        Debug.Log("selected gender is " + interviewer_gender);

        foreach(Toggle t in roleGroup.ActiveToggles()) {
            if (t.isOn) {
                if (t.name == "SDE_Toggle") {
                    role = "Software Development Engineer";
                }
                else if (t.name == "MLE_Toggle") {
                    role = "Machine Learning Engineer";
                }
                else {
                    role = "XR Engineer";
                }
            }
        }
        Debug.Log("selected role is " + role);

        foreach(Toggle t in skinTypeGroup.ActiveToggles()) {
            if (t.isOn) {
                // darkest to lighest
                if (t.name == "SkinType1_Toggle") {
                    selectedColorR = 96.0f;
                    selectedColorG = 79.0f;
                    selectedColorB = 69.0f;
                    skinTextureName = "Skin6-Albedo";
                }
                else if (t.name == "SkinType2_Toggle") {
                    selectedColorR = 158.0f;
                    selectedColorG = 113.0f;
                    selectedColorB = 88.0f;
                    skinTextureName = "Skin5-Albedo";
                }
                else if (t.name == "SkinType3_Toggle") {
                    selectedColorR = 174.0f;
                    selectedColorG = 129.0f;
                    selectedColorB = 87.0f;
                    skinTextureName = "Skin4-Albedo";
                }
                else if (t.name == "SkinType4_Toggle") {
                    selectedColorR = 250.0f;
                    selectedColorG = 221.0f;
                    selectedColorB = 192.0f;
                    skinTextureName = "Skin3-Albedo";
                }
                else {
                    selectedColorR = 228.0f;
                    selectedColorG = 225.0f;
                    selectedColorB = 166.0f;
                    skinTextureName = "Skin2-Albedo";
                }
                skin_tone_color = new Color(selectedColorR, selectedColorG, selectedColorB, 1);
            }
        }
        Debug.Log("selected color");
        Debug.Log(skin_tone_color);


        foreach(Toggle t in interviewerSkinTypeGroup.ActiveToggles()) {
            if (t.isOn) {
                if (t.name == "SkinType1_Toggle (1)") {
                    selectedColorRI = 96.0f;
                    selectedColorGI = 79.0f;
                    selectedColorBI = 69.0f;
                    darkSkinInterviewer = 2;
                }
                else if (t.name == "SkinType3_Toggle (1)") {
                    selectedColorRI = 174.0f;
                    selectedColorGI = 129.0f;
                    selectedColorB = 87.0f;
                    darkSkinInterviewer = 1;
                }
                else {
                    selectedColorRI = 228.0f;
                    selectedColorGI = 225.0f;
                    selectedColorBI = 166.0f;
                    darkSkinInterviewer = 0;
                }
                interviewer_skin_tone_color = new Color(selectedColorRI, selectedColorGI, selectedColorBI, 1);
            }
        }
        Debug.Log("interviewer's selected color");
        Debug.Log(interviewer_skin_tone_color);


        PlayerPrefs.SetString("interview_for_role", role);
        PlayerPrefs.SetString("interviewer_gender", interviewer_gender);
        PlayerPrefs.SetFloat("skinToneColorR", selectedColorR);
        PlayerPrefs.SetFloat("skinToneColorG", selectedColorG);
        PlayerPrefs.SetFloat("skinToneColorB", selectedColorB);
        PlayerPrefs.SetString("skinTextureName", skinTextureName);
        PlayerPrefs.SetFloat("skinToneColorRI", selectedColorRI);
        PlayerPrefs.SetFloat("skinToneColorGI", selectedColorGI);
        PlayerPrefs.SetFloat("skinToneColorBI", selectedColorBI);
        PlayerPrefs.SetString("handshakeDone", "no");
        PlayerPrefs.SetInt("darkSkin", darkSkinInterviewer);
        PlayerPrefs.Save();

        // Load the new scene while retaining the current scene's objects
        // SceneManager.LoadScene("NewFemaleScene", LoadSceneMode.Additive);
        if (interviewer_gender == "female")
        {
            SceneManager.LoadScene("NewFemaleAnime");
        }
        else
        {
            SceneManager.LoadScene("NewMaleAnime");
            // Debug.Log("Not yet");
        }

    }

    // public void setGender(string gender) {
    //     interviewer_gender = gender;
    // }

    // public void setRole(string practise_role) {
    //     role = practise_role;
    // }

    // public void setSkinToneColor(Color color) {
    //     skin_tone_color = color;
    // }

    // public Color getSkinToneColor() {
    //     return skin_tone_color;
    // }

    // public string getRole() {
    //     return role;
    // }

    // public string getGender() {
    //     return interviewer_gender;
    // }
}
