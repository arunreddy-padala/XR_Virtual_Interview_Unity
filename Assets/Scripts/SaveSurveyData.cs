using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class SaveSurveyData : MonoBehaviour
{


    public string Qualtrics_URL;
    public GameObject CreditScene;

    // this script will save the survey data as an in an object called surveyData

    public string PID { get; set; }
    public int Q1 { get; set; }
    public int Q2 { get; set; }
    public int Q3 { get; set; }
    public int Q4 { get; set; }
    public int Q5 { get; set; }
    public int Q6 { get; set; }
    public int Q7 { get; set; }
    public int Q8 { get; set; }
    public int Q9 { get; set; }
    public int Q10 { get; set; }
    public int Q11 { get; set; }
    public int Q12 { get; set; }
    public int Q13 { get; set; }
    public int Q14 { get; set; }
    public int Q15 { get; set; }
    public int Q16 { get; set; }
    public int Q17 { get; set; }
    public int Q18 { get; set; }
    public int Q19 { get; set; }
    public int Q20 { get; set; }
    public int Q21 { get; set; }
    public int Q22 { get; set; }
    public int Q23 { get; set; }
    public int Q24 { get; set; }
    public int Q25 { get; set; }
    public int Q26 { get; set; }
    public int Q27 { get; set; }
    public int Q28 { get; set; }
    public int Q29 { get; set; }
    public int Q30 { get; set; }
    public int Q31 { get; set; }
    public int Q32 { get; set; }
    public int Q33 { get; set; }
    public string Q34 { get; set; }
    public string T1 { get; set; }
    public string T2 { get; set; }
    // public float score { get; set; }
    // public float wpm { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        // randomly generate a PID and save it
        PID = Random.Range(100000, 999999).ToString();
        Debug.Log(PID);

        string credit_scene_data = CreditScene.GetComponent<TextMeshProUGUI>().text;
        credit_scene_data = credit_scene_data + "\n\n" + "[Your PID: " + PID.ToString() + "]";
        CreditScene.GetComponent<TextMeshProUGUI>().text = credit_scene_data;
    }

    // function name SaveQuestionData, takes 2 parameters, question number and answer
    public void SaveQuestionData(int questionNumber, int answer)
    {

        Debug.Log("Question Number: " + questionNumber + " Answer: " + answer);
        // switch statement to check which question number was passed in from 1 to 31


        switch (questionNumber)
        {
            case 1:
                Q1 = answer;
                break;
            case 2:
                Q2 = answer;
                break;
            case 3:
                Q3 = answer;
                break;
            case 4:
                Q4 = answer;
                break;
            case 5:
                Q5 = answer;
                break;
            case 6:
                Q6 = answer;
                break;
            case 7:
                Q7 = answer;
                break;
            case 8:
                Q8 = answer;
                break;
            case 9:
                Q9 = answer;
                break;
            case 10:
                Q10 = answer;
                break;
            case 11:
                Q11 = answer;
                break;
            case 12:
                Q12 = answer;
                break;
            case 13:
                Q13 = answer;
                break;
            case 14:
                Q14 = answer;
                break;
            case 15:
                Q15 = answer;
                break;
            case 16:
                Q16 = answer;
                break;
            case 17:
                Q17 = answer;
                break;
            case 18:
                Q18 = answer;
                break;
            case 19:
                Q19 = answer;
                break;
            case 20:
                Q20 = answer;
                break;
            case 21:
                Q21 = answer;
                break;
            case 22:
                Q22 = answer;
                break;
            case 23:
                Q23 = answer;
                break;
            case 24:
                Q24 = answer;
                break;
            case 25:
                Q25 = answer;
                break;
            case 26:
                Q26 = answer;
                break;
            case 27:
                Q27 = answer;
                break;
            case 28:
                Q28 = answer;
                break;
            case 29:
                Q29 = answer;
                break;
            case 30:
                Q30 = answer;
                break;
            case 31:
                Q31 = answer;
                break;
            case 32:
                Q32 = answer;
                break;
            case 33:
                Q33 = answer;
                break;
        }

    }

    public void SaveTextFieldData(int questionNumber, string answer)
    {
        switch (questionNumber)
        {
            case 34:
                Q34 = answer;
                break;
            case 35:
                T1 = answer;
                break;
            case 36:
                T2 = answer;
                break;
        }
    }

    // get the PID, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10
    public string GetSurveyData()
    {
        return $"PID:{PID}, Q1:{Q1}, Q2:{Q2}, Q3:{Q3}, Q4:{Q4}, Q5:{Q5}, Q6:{Q6}, Q7:{Q7}, Q8:{Q8}, Q9:{Q9}, Q10:{Q10}, Q11:{Q11}, Q12:{Q12}, Q13:{Q13}, Q14:{Q14}, Q15:{Q15}, Q16:{Q16}, Q17:{Q17}, Q18:{Q18}, Q19:{Q19}, Q20:{Q20}, Q21:{Q21}, Q22:{Q22}, Q23:{Q23}, Q24:{Q24}, Q25:{Q25}, Q26:{Q26}, Q27:{Q27}, Q28:{Q28}, Q29:{Q29}, Q30:{Q30}, Q31:{Q31}, Q32:{Q32}, Q33:{Q33},Q34:{Q34}, T1:{T1}, T2:{T2} ";
    }

    public void SendData()
    {
        GetSurveyData();
        StartCoroutine(PostToQualtrics());
    }

    IEnumerator PostToQualtrics()
    {

        //https://neu.co1.qualtrics.com/jfe/form/SV_6uvHf2zo6XpKijA?PID=DAT&Q1=1&Q2=1&Q3=4

        float score = PlayerPrefs.GetFloat("interview_score");
        float wpm = PlayerPrefs.GetFloat("interview_wpm");

        float[] i_scores = new float[5];
        float[] i_wpm = new float[5];
        string[] i_questions_asked = new string[5];
        string[] i_responses = new string[5];
        string[] i_feedbacks = new string[5];

        for (int i = 0; i < 5; i++)
        {
            i_scores[i] = PlayerPrefs.GetFloat("interview_score_" + i.ToString());
            i_wpm[i] = PlayerPrefs.GetFloat("interview_wpm_" + i.ToString());
            i_questions_asked[i] = PlayerPrefs.GetString("qa"+ i.ToString());
            i_feedbacks[i] = PlayerPrefs.GetString("f"+ i.ToString());
            i_responses[i] = PlayerPrefs.GetString("r"+ i.ToString());
        }
        
        string responseURL = $"{Qualtrics_URL}?PID={PID}&Q1={Q1}&Q2={Q2}&Q3={Q3}&Q4={Q4}&Q5={Q5}&Q6={Q6}&Q7={Q7}&Q8={Q8}&Q9={Q9}&Q10={Q10}&Q11={Q11}&Q12={Q12}&Q13={Q13}&Q14={Q14}&Q15={Q15}&Q16={Q16}&Q17={Q17}&Q18={Q18}&Q19={Q19}&Q20={Q20}&Q21={Q21}&Q22={Q22}&Q23={Q23}&Q24={Q24}&Q25={Q25}&Q26={Q26}&Q27={Q27}&Q28={Q28}&Q29={Q29}&Q30={Q30}&Q31={Q31}&Q32={Q32}&Q33={Q33}&Q34={Q34}&T1={T1}&T2={T2}&score={score}&wpm={wpm}&iscore1={i_scores[0]}&iscore2={i_scores[1]}&iscore3={i_scores[2]}&iscore4={i_scores[3]}&iscore5={i_scores[4]}&iwpm1={i_wpm[0]}&iwpm2={i_wpm[1]}&iwpm3={i_wpm[2]}&iwpm4={i_wpm[3]}&iwpm5={i_wpm[4]}&iquestion1={i_questions_asked[0]}&iquestion2={i_questions_asked[1]}&iquestion3={i_questions_asked[2]}&iquestion4={i_questions_asked[3]}&iquestion5={i_questions_asked[4]}&iresponse1={i_responses[0]}&iresponse2={i_responses[1]}&iresponse3={i_responses[2]}&iresponse4={i_responses[3]}&iresponse5={i_responses[4]}&ifeedback1={i_feedbacks[0]}&ifeedback2={i_feedbacks[1]}&ifeedback3={i_feedbacks[2]}&ifeedback4={i_feedbacks[3]}&ifeedback5={i_feedbacks[4]}";

        Debug.Log(responseURL);

        // create a new web request
        UnityWebRequest www = UnityWebRequest.Get(responseURL);

        yield return www.SendWebRequest();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
