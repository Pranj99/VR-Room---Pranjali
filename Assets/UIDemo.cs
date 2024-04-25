using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class UIDemo : MonoBehaviour
{
    public TextMeshProUGUI output;
    public TMP_InputField promptUser;

    //void Start()
    //{
    //    
    //}
    public void ButtonDemo()
    {
        string model = "TheBloke/Mistral-7B-Instruct-v0.1-GGUF/mistral-7b-instruct-v0.1.Q3_K_L.gguf";
        string prompt = promptUser.text;
        int maxTokens = -1;
        bool stream = true;

        ////Non streaming
        ////LlamaPromptResponse promptOutput = LlamaAPI.GetPromptResponse(model, prompt, maxTokens, stream);
        ////output.text = promptOutput.choices[0].text;

        ////Streaming
        ////LlamaAPI.GetPromptResponse(model, prompt, maxTokens, stream, UpdateOutputText);

        ////Streaming Async
        //clear the preious answer in output text box
        output.text = "";
        StartCoroutine(StreamPromptResponse(model, prompt, maxTokens, stream));
    }


    IEnumerator StreamPromptResponse(string model, string prompt, int maxTokens, bool stream)
    { 
        LlamaAPI.GetPromptResponse(model, prompt, maxTokens, stream, (completionResponse) =>
        {
            // Loop through the choices and update output.text
            foreach (var choice in completionResponse.choices)
            {  
                output.text += choice.text;
            }
        });
        yield return null;
    }

}
