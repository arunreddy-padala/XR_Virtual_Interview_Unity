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

public class InterviewerTalks : MonoBehaviour
{
    public bool preRecorded;

    public AudioClip[] sde_questions = new AudioClip[4];
    public AudioClip[] xr_questions = new AudioClip[4];
    public AudioClip[] ml_questions = new AudioClip[4];

    private AudioClip[] audio_questions;

    public AudioClip[] leadup = new AudioClip[4];

    public AudioClip[] intro = new AudioClip[3];

    public AudioClip sendoff;

    public GameObject AvatarModel;

    public string[] pre_recorded_sde = new string[4];
    private string[] pre_recorded_ml = {
        "What is overfitting in machine learning?",
"What is regularization and why is it used in machine learning?",
"What is cross-validation and how is it used in machine learning?",
"What is the difference between supervised and unsupervised learning?"};
    private string[] pre_recorded_xr = {
        "What is the difference between VR and AR?",
"What is the FOV and why is it important in VR/AR?",
"What is the difference between positional and rotational tracking in VR/AR?",
"What is stereoscopic vision and how is it used in VR/AR?"};

    private string[] pre_recorded_questions;

    public TextMeshProUGUI inputField;
    public TextMeshProUGUI outputField;
    public TTSSpeaker _speaker;
    public Button greetingButton;
    public Button interviewQuestionButton;
    public Button recordButton;
    public InputActionReference recordReference;

    private int flag = 0;
    private string[] questions;
    public string[] questionsLeadUp = new string[4];
    private float elapsedTime = 0f;
    private string filePathQuestionsAsked;
    private string filePathResponses;
    private string filePathTimings;
    private string filePathFeedbacks;
    float startTime;
    float endTime;
    string role;
    string[] questions_asked;
    string[] responses;
    string[] feedbacks;
    float[] timings;
    bool hsFlag = false;

    // Start is called before the first frame update
    void Start()
    {        
        hsFlag = false;
           
        // Get correct question list based on config settings
        role = GameObject.Find("ConfigFromMenu").GetComponent<GetConfigurationSettings>().GetRole();
        Debug.Log(role);
        if (role == "Software Development Engineer") 
        {
            questions = GetComponent<DataStore>().GetSDEQuestions();
            pre_recorded_questions = pre_recorded_sde;
            audio_questions = sde_questions;
        }
        else if (role == "Machine Learning Engineer") 
        {
            questions = GetComponent<DataStore>().GetMLEQuestions();
            pre_recorded_questions = pre_recorded_ml;
            audio_questions = ml_questions;
        }
        else 
        {
            questions = GetComponent<DataStore>().GetXRQuestions();
            pre_recorded_questions = pre_recorded_xr;
            audio_questions = xr_questions;
        }
        
        ShuffleOrder(questions);

        if (preRecorded)
        {
            StartCoroutine(InterviewerSpeaksPrerecorded());
        }
        else
        {
            StartCoroutine(InterviewerSpeaks());
        }
    }

    void ShuffleOrder(string[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;   
    }

    void emulateGreetingButtonClick()
    {
        // Emulate a button click
        greetingButton.onClick.Invoke();
    }

    void emulateInterviewQuestionButtonClick()
    {
        interviewQuestionButton.onClick.Invoke();
    }

    void emulateRecordButtonClick()
    {
        recordButton.onClick.Invoke();
    }

    IEnumerator InterviewerSpeaksPrerecorded()
    {
        AudioSource src;

        // Greeting made by interviewer
        inputField.text = "Hello! Welcome to your technical interview round.";
        yield return new WaitForSeconds(2);
        // attach audio clip to Avatar audio source and play it
        AvatarModel.GetComponent<AudioSource>().clip = intro[0];
        src = AvatarModel.GetComponent<AudioSource>();
        src.PlayOneShot(src.clip);
        yield return new WaitWhile (()=> src.isPlaying);

        // Wait till handshake is complete
        yield return StartCoroutine(WaitForHandshake());

        yield return new WaitForSeconds(1);
        // Continue greeting
        inputField.text = "This interview is to help us understand your skillset and domain knowledge, which will help us determine if you will be a good fit for the role of " + role + ". Let us get started!";
        // attach audio clip to Avatar audio source and play it
        AvatarModel.GetComponent<AudioSource>().clip = intro[1];
        src = AvatarModel.GetComponent<AudioSource>();
        src.PlayOneShot(src.clip);
        yield return new WaitWhile (()=> src.isPlaying);

        yield return new WaitForSeconds(1);
        // Interviewer asks first Intro question
        inputField.text = "To start off, can you give me a brief introduction about yourself?";
        GetComponent<DataStore>().SetQuestionsAskedElement(0, inputField.text.Trim());
        // attach audio clip to Avatar audio source and play it
        AvatarModel.GetComponent<AudioSource>().clip = intro[2];
        src = AvatarModel.GetComponent<AudioSource>();
        src.PlayOneShot(src.clip);
        AvatarModel.GetComponent<Animator>().SetInteger("animeState", 6);
        yield return new WaitWhile (()=> src.isPlaying);
        AvatarModel.GetComponent<Animator>().SetInteger("animeState", 1);

        // Record response to Intro question
        flag = 0;
        Invoke("emulateRecordButtonClick", 0.01f);
        startTime = Time.time;
        yield return StartCoroutine(StopRecordingResponse());
        flag = 0;
        Invoke("emulateRecordButtonClick", 0.01f);
        endTime = Time.time;

        // write to responses array
        GetComponent<DataStore>().SetResponsesElement(0, outputField.text.Trim());

        // write to timings array
        GetComponent<DataStore>().SetTimingsElement(0, (endTime - startTime));

        // Use Chat GPT to generate a feedback for the question and answer pair
        GetComponent<ChatGPTResponseScoring>().GetChatGPTResponse(inputField.text.Trim(), outputField.text.Trim(), 0);

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1);
            inputField.text = pre_recorded_questions[i]; 
            GetComponent<DataStore>().SetQuestionsAskedElement(i+1, inputField.text.Trim());

            // attach audio clip to Avatar audio source and play it
            AvatarModel.GetComponent<AudioSource>().clip = leadup[i];
            src = AvatarModel.GetComponent<AudioSource>();
            src.PlayOneShot(src.clip);
            AvatarModel.GetComponent<Animator>().SetInteger("animeState", 6);
            yield return new WaitWhile (()=> src.isPlaying);
            AvatarModel.GetComponent<Animator>().SetInteger("animeState", 1);
            AvatarModel.GetComponent<AudioSource>().clip = audio_questions[i];
            src = AvatarModel.GetComponent<AudioSource>();
            src.PlayOneShot(src.clip);
            AvatarModel.GetComponent<Animator>().SetInteger("animeState", 6);
            yield return new WaitWhile (()=> src.isPlaying);
            AvatarModel.GetComponent<Animator>().SetInteger("animeState", 1);

            flag = 0;
            Invoke("emulateRecordButtonClick", 0.01f);
            startTime = Time.time;
            yield return StartCoroutine(StopRecordingResponse());
            flag = 0;
            Invoke("emulateRecordButtonClick", 0.01f);
            endTime = Time.time;
            GetComponent<DataStore>().SetResponsesElement(i+1, outputField.text.Trim());

            GetComponent<DataStore>().SetTimingsElement(i+1, (endTime - startTime));

            GetComponent<ChatGPTResponseScoring>().GetChatGPTResponse(inputField.text.Trim(), outputField.text.Trim(), i+1);
        }

        yield return new WaitForSeconds(1);
        inputField.text = "Thank you, those are all the questions we had for today, your interview will now conclude. We appreciate you taking the time out to interview with us. We will provide feedback on your responses shortly.";
        AvatarModel.GetComponent<AudioSource>().clip = sendoff;
        src = AvatarModel.GetComponent<AudioSource>();
        src.PlayOneShot(src.clip);
        AvatarModel.GetComponent<Animator>().SetInteger("animeState", 6);
        yield return new WaitWhile (()=> src.isPlaying);
        AvatarModel.GetComponent<Animator>().SetInteger("animeState", 1);

        questions_asked = GetComponent<DataStore>().GetQuestionsAsked();
        responses = GetComponent<DataStore>().GetResponses();
        feedbacks = GetComponent<DataStore>().GetFeedbacks();
        timings = GetComponent<DataStore>().GetTimings();

        for (int i = 0; i < questions_asked.Length; i++)
        {
            string pref_var = "qa"+ i.ToString();
            PlayerPrefs.SetString(pref_var, questions_asked[i]);
        }

        for (int i = 0; i < responses.Length; i++)
        {
            string pref_var = "r"+ i.ToString();
            PlayerPrefs.SetString(pref_var, responses[i]);
        }
        
        for (int i = 0; i < feedbacks.Length; i++)
        {
            string pref_var = "f"+ i.ToString();
            PlayerPrefs.SetString(pref_var, feedbacks[i]);
        }
       
        for (int i = 0; i < timings.Length; i++)
        {
            string pref_var = "t"+ i.ToString();
            PlayerPrefs.SetFloat(pref_var, timings[i]);
        }
                
        // call the feedback scene
        if (GameObject.Find("ConfigFromMenu").GetComponent<GetConfigurationSettings>().GetGender() == "female")
        {
            SceneManager.LoadScene("NewFemaleFeedback");
        }
        else
        {
            SceneManager.LoadScene("NewMaleFeedback");
            // Debug.Log("Not yet");
        }
    }

    IEnumerator InterviewerSpeaks()
    {
        // Greeting made by interviewer
        inputField.text = "Hello! Welcome to your technical interview round.";
        Invoke("emulateGreetingButtonClick", 0.01f);
        yield return StartCoroutine(WaitForTTStoStart());
        yield return StartCoroutine(WaitForTTStoEnd());

        // Wait till handshake is complete
        yield return StartCoroutine(WaitForHandshake());

        // Continue greeting
        inputField.text = "This interview is to help us understand your skillset and domain knowledge, which will help us determine if you will be a good fit for the role of " + role + ". Let us get started!";
        Invoke("emulateGreetingButtonClick", 0.01f);
        yield return StartCoroutine(WaitForTTStoStart());
        yield return StartCoroutine(WaitForTTStoEnd());

        // Interviewer asks first Intro question
        inputField.text = "To start off, can you give me a brief introduction about yourself?";
        // write to questions_asked array
        GetComponent<DataStore>().SetQuestionsAskedElement(0, inputField.text.Trim());
        Invoke("emulateInterviewQuestionButtonClick", 0.01f);
        yield return StartCoroutine(WaitForTTStoStart());
        yield return StartCoroutine(WaitForTTStoEnd());

        // Record response to Intro question
        flag = 0;
        Invoke("emulateRecordButtonClick", 0.01f);
        startTime = Time.time;
        yield return StartCoroutine(StopRecordingResponse());
        flag = 0;
        Invoke("emulateRecordButtonClick", 0.01f);
        endTime = Time.time;

        // write to responses array
        GetComponent<DataStore>().SetResponsesElement(0, outputField.text.Trim());

        // write to timings array
        GetComponent<DataStore>().SetTimingsElement(0, (endTime - startTime));

        // Use Chat GPT to generate a feedback for the question and answer pair
        GetComponent<ChatGPTResponseScoring>().GetChatGPTResponse(inputField.text.Trim(), outputField.text.Trim(), 0);

        // Use for loop to ask 4 random questions from the text file
        for (int i = 0; i < 4; i++)
        {
            inputField.text = questionsLeadUp[i] + " " + questions[i]; 
            GetComponent<DataStore>().SetQuestionsAskedElement(i+1, inputField.text.Trim());

            Invoke("emulateInterviewQuestionButtonClick", 0.01f);
            yield return StartCoroutine(WaitForTTStoStart());
            yield return StartCoroutine(WaitForTTStoEnd());

            flag = 0;
            Invoke("emulateRecordButtonClick", 0.01f);
            startTime = Time.time;
            yield return StartCoroutine(StopRecordingResponse());
            flag = 0;
            Invoke("emulateRecordButtonClick", 0.01f);
            endTime = Time.time;
            GetComponent<DataStore>().SetResponsesElement(i+1, outputField.text.Trim());

            GetComponent<DataStore>().SetTimingsElement(i+1, (endTime - startTime));

            GetComponent<ChatGPTResponseScoring>().GetChatGPTResponse(inputField.text.Trim(), outputField.text.Trim(), i+1);
        }

        inputField.text = "Thank you, those are all the questions we had for today, your interview will now conclude. We appreciate you taking the time out to interview with us. We will provide feedback on your responses shortly.";
        Invoke("emulateGreetingButtonClick", 0.01f);
        yield return StartCoroutine(WaitForTTStoStart());
        yield return StartCoroutine(WaitForTTStoEnd());

        questions_asked = GetComponent<DataStore>().GetQuestionsAsked();
        responses = GetComponent<DataStore>().GetResponses();
        feedbacks = GetComponent<DataStore>().GetFeedbacks();
        timings = GetComponent<DataStore>().GetTimings();

        for (int i = 0; i < questions_asked.Length; i++)
        {
            string pref_var = "qa"+ i.ToString();
            PlayerPrefs.SetString(pref_var, questions_asked[i]);
        }

        for (int i = 0; i < responses.Length; i++)
        {
            string pref_var = "r"+ i.ToString();
            PlayerPrefs.SetString(pref_var, responses[i]);
        }
        
        for (int i = 0; i < feedbacks.Length; i++)
        {
            string pref_var = "f"+ i.ToString();
            PlayerPrefs.SetString(pref_var, feedbacks[i]);
        }
       
        for (int i = 0; i < timings.Length; i++)
        {
            string pref_var = "t"+ i.ToString();
            PlayerPrefs.SetFloat(pref_var, timings[i]);
        }
                
        // call the feedback scene
        if (GameObject.Find("ConfigFromMenu").GetComponent<GetConfigurationSettings>().GetGender() == "female")
        {
            SceneManager.LoadScene("NewFemaleFeedback");
        }
        else
        {
            SceneManager.LoadScene("NewMaleFeedback");
            // Debug.Log("Not yet");
        }
    }

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

    IEnumerator StopRecordingResponse()
    {   elapsedTime = 0f;
        Debug.Log("Waiting for response recording to stop...");
        yield return new WaitUntil(() => ((flag == 1) || elapsedTime >= 120f));
        Debug.Log("Response recording has ended #!");
    }

    IEnumerator WaitForHandshake()
    {
        Debug.Log("Waiting for Handshake to complete...");
        yield return new WaitUntil(() => hsFlag);
        Debug.Log("Handshake done");
    }

    public void SetStopRecordingFlag()
    {
        if(!_speaker.IsSpeaking)
        {
            flag = 1;
        }
    }

    // called when the game shuts down or switches to another Scene
    void OnDestroy()
    {

    }

    public void SetHandshakeFlag()
    {
        hsFlag = true;
    }
}
