using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SubmitQuestion : MonoBehaviour
{


    public GameObject ToggleGroupPanel;
    // component in the togglegrouppanel object

    public int QuestionNumber;

    public GameObject SceneData;

    public TMP_InputField InputField;




    public void SubmitQuestionData()
    {
        // // get the toggle that is currently active
        //  ToggleGroupPanel.GetComponent<ToggleGroup>().ActiveToggles();

        string activeToggle = ToggleGroupPanel.GetComponent<ToggleGroup>().GetFirstActiveToggle().ToString();

        activeToggle = activeToggle.Substring(0,1);
        int value = Convert.ToInt32(activeToggle);
        Debug.Log(value);


        SceneData.GetComponent<SaveSurveyData>().SaveQuestionData(QuestionNumber, value);
        string Result = SceneData.GetComponent<SaveSurveyData>().GetSurveyData();
        Debug.Log(Result);

    }

    public void SubmitTextFieldData()
    {
        string text = InputField.text;
        SceneData.GetComponent<SaveSurveyData>().SaveTextFieldData(QuestionNumber, text);
        string Result = SceneData.GetComponent<SaveSurveyData>().GetSurveyData();
        Debug.Log(Result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
