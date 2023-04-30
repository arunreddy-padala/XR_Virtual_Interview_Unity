using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OpenAI
{
    public class ChatGPTForInterview : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI inputField;
        [SerializeField] private Button button;
        [SerializeField] private Button button2;
        // [SerializeField] private Text textArea;

        private OpenAIApi openai = new OpenAIApi("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");

        private string userInput;
        private string Instruction = "Act as an interviewer for technical role such as Software Engineer in an office room and reply to the questions.\nQ: ";

        private void Start()
        {
            button.onClick.AddListener(SendReply);
            button2.onClick.AddListener(SendReply2);
        }

        private async void SendReply()
        {
            userInput = "Greet someone who you are interviewing for a technical role in a professional and polite way";
            Instruction += $"{userInput}\nA: ";
            
            inputField.text = "...";
            // inputField.text = "";

            button.enabled = false;
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = Instruction,
                Model = "text-davinci-003",
                MaxTokens = 128
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                inputField.text = completionResponse.Choices[0].Text;
                Instruction += $"{completionResponse.Choices[0].Text}\nQ: ";
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }

        private async void SendReply2()
        {
            // userInput = inputField.text;
            //grab the text element of user's speech and add as userinput
            userInput = "Ask the interviewee questions related to computer science topics that can be answered in less than 40 characters";
            Instruction += $"{userInput}\nA: ";
            
            inputField.text = "...";
            // inputField.text = "";

            button.enabled = false;
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = Instruction,
                Model = "text-davinci-003",
                MaxTokens = 128
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                inputField.text = completionResponse.Choices[0].Text;
                Instruction += $"{completionResponse.Choices[0].Text}\nQ: ";
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
