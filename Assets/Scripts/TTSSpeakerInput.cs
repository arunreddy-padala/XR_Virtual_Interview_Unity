/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using UnityEngine;
using UnityEngine.UI;
using Meta.WitAi.TTS.Utilities;
using System.Threading.Tasks;
using Meta.WitAi.TTS.Samples;
using TMPro;

public class TTSSpeakerInput : MonoBehaviour
{
    [SerializeField] private Text _title;
    // [SerializeField] private InputField _input;
    public TextMeshProUGUI _input;
    [SerializeField] private TTSSpeaker _speaker;
    [SerializeField] private Button _stopButton;
    [SerializeField] private Button _speakButton;
    [SerializeField] private Button _speakButton2;
    [SerializeField] private Button _speakQueuedButton;

    [SerializeField] private string[] _queuedText;

    // States
    private bool _loading;
    private bool _speaking;

    // Add delegates
    private void OnEnable()
    {
        RefreshButtons();
        _stopButton.onClick.AddListener(StopClick);
        _speakButton.onClick.AddListener(SpeakClick);
        _speakButton2.onClick.AddListener(SpeakClick2);
        _speakQueuedButton.onClick.AddListener(SpeakQueuedClick);
    }
    // Stop click
    private void StopClick() => _speaker.Stop();
    // Speak phrase click
    private async void SpeakClick() { 
        
        await Task.Delay(3500);
        _speaker.Speak(_input.text);

    }

    private async void SpeakClick2() { 
        
        await Task.Delay(3500);
        _speaker.Speak(_input.text);

    }
    // Speak queued phrase click
    private void SpeakQueuedClick()
    {
        foreach (var text in _queuedText)
        {
            _speaker.SpeakQueued(text);
        }
        _speaker.SpeakQueued(_input.text);
    }
    // Remove delegates
    private void OnDisable()
    {
        _stopButton.onClick.RemoveListener(StopClick);
        _speakButton.onClick.RemoveListener(SpeakClick);
        _speakQueuedButton.onClick.RemoveListener(SpeakQueuedClick);
    }

    // Preset text fields
    private void Update()
    {
        // On preset voice id update
        if (!string.Equals(_title.text, _speaker.presetVoiceID))
        {
            _title.text = _speaker.presetVoiceID;
            // _input.placeholder.GetComponent<Text>().text = $"Interact with Button to start";
        }
        // On state changes
        if (_loading != _speaker.IsLoading)
        {
            _loading = _speaker.IsLoading;
            RefreshButtons();
        }
        if (_speaking != _speaker.IsSpeaking)
        {
            _speaking = _speaker.IsSpeaking;
            RefreshButtons();
        }
    }
    // Refresh interactable based on states
    private void RefreshButtons()
    {
        _stopButton.interactable = _loading || _speaking;
    }
}
