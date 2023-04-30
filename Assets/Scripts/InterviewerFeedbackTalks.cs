using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Meta.WitAi.TTS.Data;
using Meta.WitAi.TTS.Utilities;
using Meta.WitAi.TTS.Samples;
using UnityEngine.InputSystem;

public class InterviewerFeedbackTalks : MonoBehaviour
{
    public bool preRecorded;

    public AudioClip maleFeedbackClip;
    public AudioClip femaleFeedbackClip;

    public TextMeshProUGUI qaScore;
    public TextMeshProUGUI qaRate;
    public TextMeshProUGUI qaScoreTitle;
    public TextMeshProUGUI qaRateTitle;
    public GameObject questionDialog;
    public GameObject answerDialog;
    public GameObject feedbackDialog;
    public TextMeshProUGUI questionDialogText;
    public TextMeshProUGUI answerDialogText;
    public TextMeshProUGUI feedbackDialogText;
    public TextMeshProUGUI TTSField;
    public TTSSpeaker _speaker;
    public Button TTSButton;
    // public InputActionReference nextSceneReference;
    public GameObject Avatar;
    
    private string filePathQuestionsAsked;
    private string filePathResponses;
    private string filePathFeedbacks;
    private string filePathTimings;

    private string[] questions = new string[5];
    private string[] responses = new string[5];
    private string[] feedbacks = new string[5];
    private float[] timings = new float[5];

    private float[] f_scores = new float[5];
    private float[] wpm_scores = new float[5];

    float avgScore = 0.0f;
    float avgWPM = 0.0f;

    int currentPanelFlag = -1;

    string gender;
        
    // Start is called before the first frame update
    void Start()
    {
        gender = GameObject.Find("ConfigFromMenu").GetComponent<GetConfigurationSettings>().GetGender();
        Avatar.GetComponent<Animator>().SetInteger("animeState", 1);
        
        for (int i = 0; i < 5; i++)
        {
            string pref_var = "qa"+ i.ToString();
            questions[i] = PlayerPrefs.GetString(pref_var);
        }

        for (int i = 0; i < 5; i++)
        {
            string pref_var = "r"+ i.ToString();
            responses[i] = PlayerPrefs.GetString(pref_var);
        }
        
        for (int i = 0; i < 5; i++)
        {
            string pref_var = "f"+ i.ToString();
            feedbacks[i] = PlayerPrefs.GetString(pref_var);
        }
       
        for (int i = 0; i < 5; i++)
        {
            string pref_var = "t"+ i.ToString();
            timings[i] = PlayerPrefs.GetFloat(pref_var);
        }

        float sumScore = 0f;
        float sumWPM = 0f;

        for (int i = 0; i < 5; i++) 
        {
            // float wpm = (countWordsInString(responses[i])/(float.Parse(timings[i].Trim())))*60.0f;
            float wpm = (countWordsInString(responses[i])/(timings[i]))*60.0f;
            float score = GetScore(feedbacks[i]);

            PlayerPrefs.SetFloat("interview_score_" + i.ToString(), score);
            PlayerPrefs.SetFloat("interview_wpm_" + i.ToString(), wpm);

            sumScore += score;
            sumWPM += wpm;

            f_scores[i] = score;
            wpm_scores[i] = wpm;
        }

        avgScore = sumScore/5.0f;
        avgWPM = (float)System.Math.Round(sumWPM/5.0f, 3);

        PlayerPrefs.SetFloat("interview_score", avgScore);
        PlayerPrefs.SetFloat("interview_wpm", avgWPM);

        StartCoroutine(InterviewerGivesFeedback());
    }

    int countWordsInString(string myString)
    {
        int wordCount = 0;

        // Loop through each character in the string
        for (int i = 0; i < myString.Length; i++)
        {
            // If the current character is a whitespace and the previous character is not a whitespace, then it's a new word
            if ((i > 0 && char.IsWhiteSpace(myString[i]) && !char.IsWhiteSpace(myString[i - 1])) || (i == 0 && !char.IsWhiteSpace(myString[i])))
            {
                wordCount++;
            }
        }

        return wordCount;
    }

    void emulateTTSButtonClick()
    {
        // Emulate a button click
        TTSButton.onClick.Invoke();
    }

    float GetScore(string myString)
    {
        Match match = Regex.Match(myString, @"\d+");
        if (match.Success)
        {
            string numberString = match.Value;
            int number = int.Parse(numberString);
            return number/1f;
        }
        else
        {
            Debug.Log("No numbers found in the string.");
            return 1f;
        }
    }

    string WPMCategory(float rate)
    {
        if (rate >= 120f && rate <= 150f)
        {
            return "Optimum";
        }

        if (rate < 120f)
        {
            return "Below the optimum rate";
        }

        return "Above the optimum rate";
    }

    string ValueRangeWPMCategory(float rate)
    {
        if (rate >= 120f && rate <= 150f)
        {
            return "120-150";
        }

        if (rate < 120f)
        {
            return "Less than 120";
        }

        return "More than 150";
    }

    IEnumerator InterviewerGivesFeedback()
    {
        GameObject overlayPanel = GameObject.Find("SurveyTriggerButton");
        // overlayPanel.SetActive(false);

        qaScoreTitle.text = "Overall Score";
        qaRateTitle.text = "Overall Words-per-minute Rate";
        qaScore.text = "Average Rating for responses: " + avgScore.ToString() + " / 10";
        qaRate.text = "Average Words-per-minute for responses: " + avgWPM.ToString() + "\n(" + WPMCategory(avgWPM) + ": " + ValueRangeWPMCategory(avgWPM) + ")";
        questionDialog.SetActive(false);
        feedbackDialog.SetActive(false);
        answerDialog.SetActive(false);

        // Now, give a TTS report of the final metrics
        if (!preRecorded)
        {
            TTSField.text = "Here is your feedback for the interview conducted, please take a look. Your overall rating for your responses is " + avgScore.ToString() + " out of 10. Your overall words-per-minute rate is " + avgWPM.ToString() + " which is " + WPMCategory(avgWPM);
            Invoke("emulateTTSButtonClick", 0.01f);
            yield return StartCoroutine(WaitForTTStoStart());
            yield return StartCoroutine(WaitForTTStoEnd());
        }

        else 
        {   AudioSource src;
            if (gender == "male")
            {
                yield return new WaitForSeconds(1);
                Avatar.GetComponent<AudioSource>().clip = maleFeedbackClip;
                src = Avatar.GetComponent<AudioSource>();
                src.PlayOneShot(src.clip);
                Avatar.GetComponent<Animator>().SetInteger("animeState", 6);
                yield return new WaitWhile (()=> src.isPlaying);
                Avatar.GetComponent<Animator>().SetInteger("animeState", 1);
            }
            else
            {
                yield return new WaitForSeconds(1);
                // attach audio clip to Avatar audio source and play it
                Avatar.GetComponent<AudioSource>().clip = femaleFeedbackClip;
                src = Avatar.GetComponent<AudioSource>();
                src.PlayOneShot(src.clip);
                Avatar.GetComponent<Animator>().SetInteger("animeState", 6);
                yield return new WaitWhile (()=> src.isPlaying);
                Avatar.GetComponent<Animator>().SetInteger("animeState", 1);
            }
            yield return 1;
        }

        // overlayPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // void Pressed(InputAction.CallbackContext context)
    // {
    //     if(!_speaker.IsSpeaking)
    //     {
    //         Debug.Log("Moving to survey scene");
    //         // GameObject.Find("OverlayInstruction").SetActive(false);
    //         // use scene manager to move to survey scene
    //         SceneManager.LoadScene("SurveyScene");
    //     }
    // }

    // void Released(InputAction.CallbackContext context)
    // {

    // }

    IEnumerator WaitForTTStoStart()
    {
        Debug.Log("Waiting for TTS to start...");
        yield return new WaitUntil(() => _speaker.IsSpeaking);
        Debug.Log("TTS has started!");
    }

    IEnumerator WaitForTTStoEnd()
    {
        Debug.Log("Waiting for TTS to end...");
        yield return new WaitUntil(() => !_speaker.IsSpeaking);
        Debug.Log("TTS has ended!");
    }

    // // called when the game shuts down or switches to another Scene
    // void OnDestroy()
    // {
    //     nextSceneReference.action.started -= Pressed;
    //     nextSceneReference.action.canceled -= Released;
    // }

    public void SetNextPanelFlag()
    {
        currentPanelFlag += 1;
        if (currentPanelFlag > 4)
        {
            currentPanelFlag = -1;
        }
        SetPanelContent();
    }

    public void SetPreviousPanelFlag()
    {
        currentPanelFlag -= 1;
        if (currentPanelFlag < -1)
        {
            currentPanelFlag = 4;
        }
        SetPanelContent();
    }

    void SetPanelContent()
    {
        if (currentPanelFlag == -1)
        {
            qaScoreTitle.text = "Overall Score";
            qaRateTitle.text = "Overall Words-per-minute Rate";
            qaScore.text = "Average Rating for responses: " + avgScore.ToString() + " / 10";
            qaRate.text = "Average Words-per-minute for responses: " + avgWPM.ToString() + "\n(" + WPMCategory(avgWPM) + ": " + ValueRangeWPMCategory(avgWPM) + ")";

            questionDialog.SetActive(false);
            feedbackDialog.SetActive(false);
            answerDialog.SetActive(false);
        }

        else
        {
            questionDialog.SetActive(true);
            feedbackDialog.SetActive(true);
            answerDialog.SetActive(true);

            questionDialogText.text = questions[currentPanelFlag];
            answerDialogText.text = responses[currentPanelFlag];
            feedbackDialogText.text = feedbacks[currentPanelFlag];

            qaScoreTitle.text = "Score";
            qaRateTitle.text = "Words-per-minute Rate";
            qaScore.text = "Rating for your response: " + f_scores[currentPanelFlag].ToString() + " / 10";
            qaRate.text = "Words-per-minute rate for response: " + System.Math.Round(wpm_scores[currentPanelFlag], 3).ToString();
        }
    }
}
