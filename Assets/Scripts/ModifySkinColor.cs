using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifySkinColor : MonoBehaviour
{
    public SkinnedMeshRenderer _skinnedMeshRenderer;

    public Material[] maleMaterials;
    public Material[] femaleMaterials;

    public Material[] maleMidMaterials;
    public Material[] femaleMidMaterials;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInterviewerSkinColor(Color newBaseMapColor, int darkSkin, string gender)
    {
        // Material[] matArray = _skinnedMeshRenderer.materials;

        // for (int i = 0; i < matArray.Length; i++)
        // {
        //     matArray[i].color = newBaseMapColor;
        // }
        if (gender == "male")
        {
            if (darkSkin == 2)
            {
                _skinnedMeshRenderer.materials = maleMaterials;
            }
            else if(darkSkin == 1)
            {
                _skinnedMeshRenderer.materials = maleMidMaterials;
            }
        }

        else 
        {
            if (darkSkin == 2)
            {
                _skinnedMeshRenderer.materials = femaleMaterials;
            }
            else if(darkSkin == 1)
            {
                _skinnedMeshRenderer.materials = femaleMidMaterials;
            }
        }
    }
}
