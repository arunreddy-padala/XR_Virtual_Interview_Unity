using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenAI;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class ChatGPTResponseScoring : MonoBehaviour
{
    // [SerializeField] private TextMeshProUGUI inputField;
    // [SerializeField] private Button button;
    // [SerializeField] private Button button2;
    // // [SerializeField] private Text textArea;

    private OpenAIApi openai = new OpenAIApi("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");

    private string userInput;
    private string Instruction = "You are an experienced software engineer conducting a technical interview.";
    // private string filePath = "./Assets/Scripts/feedback.txt";

    void Start()
    {
        // File.WriteAllText(filePath, string.Empty);

    }

    public async void GetChatGPTResponse(string question, string answer, int element)
    {
        userInput = " For the question '" + question + "' give feedback and rating for the answer '" + answer + "' on a scale from 1 to 10.";
        string requestString = Instruction + userInput;
        
        // Complete the instruction
        var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
        {
            Prompt = requestString,
            Model = "text-davinci-003",
            MaxTokens = 128
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            // write ChatGPT feedback into feedback.txt file
            // return completionResponse.Choices[0].Text;
            string feedbackString = completionResponse.Choices[0].Text.Trim();
            feedbackString = feedbackString.Replace("\n", " ");
            // using (StreamWriter writer = new StreamWriter(filePath, true))
            // {
            //     // write each line to the file
            //     writer.WriteLine(feedbackString);
            // }
            GetComponent<DataStore>().SetFeedbacksElement(element, feedbackString);
        }
        else
        {
            // using (StreamWriter writer = new StreamWriter(filePath, true))
            // {
            //     // write each line to the file
            //     writer.WriteLine("No text was generated from this prompt.");
            // }
            // return "No text was generated from this prompt.";
            GetComponent<DataStore>().SetFeedbacksElement(element, "No text was generated from this prompt.");
        }
    }

    // public async void GetOverallChatGPTResponse()
    // {
    //     userInput = " Please rate the performance in the interview by averaging out the ratings and feedback for each of the answers above, give only one number as the final average rating.";
    //     string requestString = Instruction + userInput;
        
    //     // Complete the instruction
    //     var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
    //     {
    //         Prompt = requestString,
    //         Model = "text-davinci-003",
    //         MaxTokens = 128
    //     });

    //     if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
    //     {
    //         // write ChatGPT feedback into feedback.txt file
    //         // return completionResponse.Choices[0].Text;
    //         string feedbackString = completionResponse.Choices[0].Text.Trim();
    //         using (StreamWriter writer = new StreamWriter(filePath, true))
    //         {
    //             // write each line to the file
    //             writer.WriteLine(feedbackString);
    //         }
    //     }
    //     else
    //     {
    //         using (StreamWriter writer = new StreamWriter(filePath, true))
    //         {
    //             // write each line to the file
    //             writer.WriteLine("No text was generated from this prompt.");
    //         }
    //         // return "No text was generated from this prompt.";
    //     }
    // }

    // private async void SendReply2()
    // {
    //     // userInput = inputField.text;
    //     //grab the text element of user's speech and add as userinput
    //     userInput = "Ask the interviewee questions related to computer science topics that can be answered in less than 40 characters";
    //     Instruction += $"{userInput}\nA: ";
        
    //     inputField.text = "...";
    //     // inputField.text = "";

    //     button.enabled = false;
    //     inputField.enabled = false;
        
    //     // Complete the instruction
    //     var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
    //     {
    //         Prompt = Instruction,
    //         Model = "text-davinci-003",
    //         MaxTokens = 128
    //     });

    //     if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
    //     {
    //         inputField.text = completionResponse.Choices[0].Text;
    //         Instruction += $"{completionResponse.Choices[0].Text}\nQ: ";
    //     }
    //     else
    //     {
    //         Debug.LogWarning("No text was generated from this prompt.");
    //     }

    //     button.enabled = true;
    //     inputField.enabled = true;
    // }
}

