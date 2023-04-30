using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinking : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public string blendshapeLeftEye;
    public string blendshapeRightEye;

    [Header("Properties")]
    public float blinkInterval          = 2.0f;  //time until the next blink
    public float blinkEyeCloseDuration  = 0.06f; //how long the eye stays closed during blinking
    public float blinkOpeningSeconds    = 0.03f; //the speed of eye opening in the animation
    public float blinkClosingSeconds    = 0.1f;  //the speed of eye closing in the animation

    public Coroutine blinkCoroutine;

    private void Awake()
    {
        // blendshapeIndex = GetBlendshapeIndex("Fcl_EYE_Close");
    }

    private int GetBlendshapeIndex(string blendshapeName)
    {
        Mesh mesh = skinnedMesh.sharedMesh;
        int blendshapeIndex = mesh.GetBlendShapeIndex(blendshapeName);
        return blendshapeIndex;
    }

    private IEnumerator BlinkRoutine(int blendshapeIndex)
    {
        //This is an infinite loop coroutine
        while (true)
        {
            //Wait until we need to blink
            yield return new WaitForSeconds(blinkInterval);

            //Close eyes
            var value = 0f;
            var closeSpeed = 1.0f / blinkClosingSeconds;
            while (value < 1)
            {
                skinnedMesh.SetBlendShapeWeight(blendshapeIndex, value * 100);
                value += Time.deltaTime * closeSpeed;
                yield return null;
            }
            skinnedMesh.SetBlendShapeWeight(blendshapeIndex, 100);

            //Wait to open our eyes
            yield return new WaitForSeconds(blinkEyeCloseDuration);

            //Open eyes
            value = 1f;
            var openSpeed = 1.0f / blinkOpeningSeconds;
            while (value > 0)
            {
                skinnedMesh.SetBlendShapeWeight(blendshapeIndex, value * 100);
                value -= Time.deltaTime * openSpeed;
                yield return null;
            }
            skinnedMesh.SetBlendShapeWeight(blendshapeIndex, 0);
        }
    }

    void Start()
    {
        StartCoroutine(BlinkRoutine(GetBlendshapeIndex(blendshapeLeftEye)));
        StartCoroutine(BlinkRoutine(GetBlendshapeIndex(blendshapeRightEye)));
    }

    // private void OnEnable()
    // {
    //     blinkCoroutine1 = StartCoroutine(BlinkRoutine(GetBlendshapeIndex(blendshapeLeftEye)));
    //     blinkCoroutine2 = StartCoroutine(BlinkRoutine(GetBlendshapeIndex(blendshapeRightEye)));
    // }

    // private void OnDisable()
    // {
    //     if (blinkCoroutine1 != null)
    //     {
    //         StopCoroutine(blinkCoroutine1);
    //         blinkCoroutine1 = null;
    //     }

    //     if (blinkCoroutine2 != null)
    //     {
    //         StopCoroutine(blinkCoroutine2);
    //         blinkCoroutine2 = null;
    //     }
    // }
}