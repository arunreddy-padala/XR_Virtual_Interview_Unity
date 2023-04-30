//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections;
using Microsoft.CognitiveServices.Speech.Audio;
using System.IO;
using TMPro;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
#if PLATFORM_IOS
using UnityEngine.iOS;
using System.Collections;
#endif

public class AzureSpeechToText : MonoBehaviour
{
    private bool micPermissionGranted = false;
    public TextMeshProUGUI outputText;
    public Button recoButton;
    SpeechRecognizer recognizer;
    SpeechConfig config;
    AudioConfig audioInput;
    PushAudioInputStream pushStream;
    
    // string subscriptionKey = "2e476b38c84742f49c29ff29d467e5ec";
    string subscriptionKey = "00e1393baca54947ac7768307df8d42e";
    string region = "eastus";
    string language = "en-US";
    string transcript = "";

    private object threadLocker = new object();
    private bool recognitionStarted = false;
    private string message;
    int lastSample = 0;
    AudioSource audioSource;

#if PLATFORM_ANDROID || PLATFORM_IOS
    // Required to manifest microphone permission, cf.
    // https://docs.unity3d.com/Manual/android-manifest.html
    private Microphone mic;
#endif

    private byte[] ConvertAudioClipDataToInt16ByteArray(float[] data)
    {
        MemoryStream dataStream = new MemoryStream();
        int x = sizeof(Int16);
        Int16 maxValue = Int16.MaxValue;
        int i = 0;
        while (i < data.Length)
        {
            dataStream.Write(BitConverter.GetBytes(Convert.ToInt16(data[i] * maxValue)), 0, x);
            ++i;
        }
        byte[] bytes = dataStream.ToArray();
        dataStream.Dispose();
        return bytes;
    }

    private void RecognizingHandler(object sender, SpeechRecognitionEventArgs e)
    {
        lock (threadLocker)
        {
            message = e.Result.Text;
            Debug.Log("RecognizingHandler: " + message);
        }
    }

    private void RecognizedHandler(object sender, SpeechRecognitionEventArgs e)
    {
        lock (threadLocker)
        {
            message = e.Result.Text;
            Debug.Log("RecognizedHandler: " + message);
        }
    }

    private void CanceledHandler(object sender, SpeechRecognitionCanceledEventArgs e)
    {
        lock (threadLocker)
        {
            message = e.ErrorDetails.ToString();
            Debug.Log("CanceledHandler: " + message);
        }
    }

    public async void ButtonClick()
    {
        if (recognitionStarted)
        {
            await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(true);

            if (Microphone.IsRecording(Microphone.devices[0]))
            {
                Debug.Log("Microphone.End: " + Microphone.devices[0]);
                Microphone.End(null);
                lastSample = 0;
            }

            lock (threadLocker)
            {
                recognitionStarted = false;
                Debug.Log("RecognitionStarted: " + recognitionStarted.ToString());
            }
        }
        else
        {
            if (!Microphone.IsRecording(Microphone.devices[0]))
            {
                Debug.Log("Microphone.Start: " + Microphone.devices[0]);
                audioSource.clip = Microphone.Start(Microphone.devices[0], true, 200, 16000);
                Debug.Log("audioSource.clip channels: " + audioSource.clip.channels);
                Debug.Log("audioSource.clip frequency: " + audioSource.clip.frequency);
            }

            await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
            lock (threadLocker)
            {
                recognitionStarted = true;
                Debug.Log("RecognitionStarted: " + recognitionStarted.ToString());
            }
        }
    }

    void Start()
    {
        if (outputText.text == null)
        {
            UnityEngine.Debug.LogError("outputText property is null! Assign a UI Text element to it.");
        }
        else if (recoButton == null)
        {
            message = "recoButton property is null! Assign a UI Button to it.";
            UnityEngine.Debug.LogError(message);
        }
        else
        {
            // Continue with normal initialization, Text and Button objects are present.
#if PLATFORM_ANDROID
            // Request to use the microphone, cf.
            // https://docs.unity3d.com/Manual/android-RequestingPermissions.html
            message = "Waiting for mic permission";
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
#elif PLATFORM_IOS
            if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
            {
                Application.RequestUserAuthorization(UserAuthorization.Microphone);
            }
#else
            micPermissionGranted = true;
            message = "Interviewee: Transcription";
#endif
            config = SpeechConfig.FromSubscription(subscriptionKey, region);
            config.SetProperty(PropertyId.Speech_SegmentationSilenceTimeoutMs, "3000");
            config.SetProperty(PropertyId.SpeechServiceConnection_InitialSilenceTimeoutMs, "3000");
            pushStream = AudioInputStream.CreatePushStream();
            audioInput = AudioConfig.FromStreamInput(pushStream);
            recognizer = new SpeechRecognizer(config, audioInput);
            recognizer.Recognizing += RecognizingHandler;
            recognizer.Recognized += RecognizedHandler;
            recognizer.Canceled += CanceledHandler;

            recoButton.onClick.AddListener(ButtonClick);
            foreach (var device in Microphone.devices)
            {
                Debug.Log("DeviceName: " + device);                
            }
            audioSource = gameObject.GetComponent<AudioSource>();
        }
    }

    void Disable()
    {
        recognizer.Recognizing -= RecognizingHandler;
        recognizer.Recognized -= RecognizedHandler;
        recognizer.Canceled -= CanceledHandler;
        pushStream.Close();
        recognizer.Dispose();
    }

    void FixedUpdate()
    {
#if PLATFORM_ANDROID
        if (!micPermissionGranted && Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            micPermissionGranted = true;
            message = "Interviewee: Transcription";
        }
#elif PLATFORM_IOS
        if (!micPermissionGranted && Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            micPermissionGranted = true;
            message = "Interviewee: Transcription";
        }
#endif
        lock (threadLocker)
        {
            if (recoButton != null)
            {
                recoButton.interactable = micPermissionGranted;
            }
            if (outputText.text != null)
            {
                outputText.text = message;
                transcript += message;
            }
        }

        if (Microphone.IsRecording(Microphone.devices[0]) && recognitionStarted == true)
        {
            // GameObject.Find("RecordingOrNotText").GetComponentInChildren<TextMeshProUGUI>().text = "RECORDING";
            GameObject.Find("MyButton").GetComponentInChildren<TextMeshProUGUI>().text = "Recording...";
            Button b = GameObject.Find("MyButton").GetComponentInChildren<Button>();
            ColorBlock cb = b.colors;
            cb.normalColor = new Color(1f, 0f, 0f, 1f);
            b.colors = cb;
            int pos = Microphone.GetPosition(Microphone.devices[0]);
            int diff = pos - lastSample;

            if (diff > 0)
            {
                float[] samples = new float[diff * audioSource.clip.channels];
                audioSource.clip.GetData(samples, lastSample);
                byte[] ba = ConvertAudioClipDataToInt16ByteArray(samples);
                if (ba.Length != 0)
                {
                    Debug.Log("pushStream.Write pos:" + Microphone.GetPosition(Microphone.devices[0]).ToString() + " length: " + ba.Length.ToString());
                    pushStream.Write(ba);
                }
            }
            lastSample = pos;
        }
        else if (!Microphone.IsRecording(Microphone.devices[0]) && recognitionStarted == false)
        {
            // GameObject.Find("RecordingOrNotText").GetComponentInChildren<TextMeshProUGUI>().text = "NOT RECORDING";
            Button b = GameObject.Find("MyButton").GetComponentInChildren<Button>();
            GameObject.Find("MyButton").GetComponentInChildren<TextMeshProUGUI>().text = "Not recording";
            ColorBlock cb = b.colors;
            cb.normalColor = new Color(31.0f/255.0f, 32.0f/255.0f, 33.0f/255.0f, 255f/255.0f);
            b.colors = cb;
        }
    }
}