using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetConfigurationSettings : MonoBehaviour
{
    SkinnedMeshRenderer leftHandControllerRenderer;
    SkinnedMeshRenderer rightHandControllerRenderer;
    string interview_for_role;
    string interviewer_gender;

    // Start is called before the first frame update
    void Start()
    {

        GameObject objl = GameObject.Find("hands:Lhand");
        GameObject objr = GameObject.Find("hands:Rhand");
        leftHandControllerRenderer = objl.GetComponent<SkinnedMeshRenderer>();
        rightHandControllerRenderer = objr.GetComponent<SkinnedMeshRenderer>();
        //rightHandControllerRenderer = GameObject.Find("hands:Rhand").GetComponentInChildren<SkinnedMeshRenderer>();

        interview_for_role = PlayerPrefs.GetString("interview_for_role");
        interviewer_gender = PlayerPrefs.GetString("interviewer_gender");
        float skinToneColorR = PlayerPrefs.GetFloat("skinToneColorR");
        float skinToneColorG = PlayerPrefs.GetFloat("skinToneColorG");
        float skinToneColorB = PlayerPrefs.GetFloat("skinToneColorB");
        string skinToneTextureName = PlayerPrefs.GetString("skinTextureName");

        float skinToneColorRI = PlayerPrefs.GetFloat("skinToneColorRI");
        float skinToneColorGI = PlayerPrefs.GetFloat("skinToneColorGI");
        float skinToneColorBI = PlayerPrefs.GetFloat("skinToneColorBI");

        int darkSkin = PlayerPrefs.GetInt("darkSkin");

        Debug.Log(interview_for_role);
        Debug.Log(interviewer_gender);
        Debug.Log(skinToneColorR);
        Debug.Log(skinToneColorG);
        Debug.Log(skinToneColorB);

        Color skinColor = new Color(skinToneColorR/255f, skinToneColorG/255f, skinToneColorB/255f);
        Color skinColorInterviewer = new Color(skinToneColorRI/255f, skinToneColorGI/255f, skinToneColorBI/255f);

        setSkinTone(skinColor, skinToneTextureName);
        gameObject.GetComponent<ModifySkinColor>().SetInterviewerSkinColor(skinColorInterviewer, darkSkin, interviewer_gender);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setSkinTone(Color skinColor, string skinToneTextureName) {

        // Create a new material based on the current material
        Material newMaterialLeft = new Material(leftHandControllerRenderer.material);
        Texture skinTexture = Resources.Load<Texture>("Hand_BaseColor");
        Texture skinTexNormal = Resources.Load<Texture>("Hand_Normal");
        Texture skinTexMetallic = Resources.Load<Texture>("Hand_Metallic");
        
        // Set the new color and texture in the new material
        newMaterialLeft.mainTexture = skinTexture;
        // Set the metallic map texture
        newMaterialLeft.EnableKeyword("_METALLICGLOSSMAP");
        newMaterialLeft.SetTexture("_MetallicGlossMap", skinTexMetallic);

        // Set the normal map texture
        newMaterialLeft.EnableKeyword("_NORMALMAP");
        newMaterialLeft.SetTexture("_BumpMap", skinTexNormal);
        newMaterialLeft.color = skinColor;
        
        // Apply the new material to the Skinned Mesh Renderer
        leftHandControllerRenderer.material = newMaterialLeft;

        // Create a new material based on the current material
        Material newMaterialRight = new Material(rightHandControllerRenderer.material);
        
        // Set the new color in the new material
        newMaterialRight.mainTexture = skinTexture;
        newMaterialRight.EnableKeyword("_NORMALMAP");
        newMaterialRight.SetTexture("_BumpMap", skinTexNormal);
        newMaterialRight.EnableKeyword("_METALLICGLOSSMAP");
        newMaterialRight.SetTexture("_MetallicGlossMap", skinTexMetallic);
        newMaterialRight.color = skinColor;
        
        // Apply the new material to the Skinned Mesh Renderer
        rightHandControllerRenderer.material = newMaterialRight;

        // leftHandControllerRenderer.material.color = skinColor;
        // rightHandControllerRenderer.material.color = skinColor;

        Debug.Log("set skin color and texture...!!");
    }

    public string GetRole()
    {
        return interview_for_role;
    }

    public string GetGender()
    {
        return interviewer_gender;
    }
}
