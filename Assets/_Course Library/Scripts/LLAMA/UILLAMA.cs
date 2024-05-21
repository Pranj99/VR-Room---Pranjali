using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UILLAMA : MonoBehaviour
{
    ////Start is called before the first frame update
    //void Start()
    //{

    //}
    public TextMeshProUGUI output;
    private string outputHolder;
    public TextMeshProUGUI inputQuestion;
    private ConcurrentQueue<string> responseQueue = new ConcurrentQueue<string>();


    // Update is called once per frame
    private void Update()
    {
        // Process the queue and update the UI on the main thread
        while (responseQueue.TryDequeue(out string response))
        {
            output.text += response;
        }
    }


    public void OkayButton()
    {
        output.text = "";
        GenerateStreamingResponseAsyncAnythingLLMButton();
        //GenerateStreamingResponseButtonAsync();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RightHand Controller")
        {
            //GenerateStreamingResponseAsync();
            GenerateStreamingResponseAsyncAnythingLLM();

            ////Simple Object picking test
            //output.text = "this is a " + this.gameObject.name;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        output.text = "";
    }


    private async void GenerateStreamingResponseAsync()
    {
        string model = "TheBloke/Mistral-7B-Instruct-v0.1-GGUF/mistral-7b-instruct-v0.1.Q3_K_L.gguf";
        string prompt = "what is a " + this.gameObject.name;
        int maxTokens = -1;
        bool stream = true;

        await Task.Run(() =>
        {
            LlamaAPIVRroom.GetPromptResponse(model, prompt, maxTokens, stream, (completionResponse) =>
            {
                // Loop through the choices and enqueue the response text
                foreach (var choice in completionResponse.choices)
                {
                    responseQueue.Enqueue(choice.text);
                }
            });
        });
    }

    private async void GenerateStreamingResponseButtonAsync()
    {
        string model = "TheBloke/Mistral-7B-Instruct-v0.1-GGUF/mistral-7b-instruct-v0.1.Q3_K_L.gguf";
        string prompt = "what is a tree?";
        int maxTokens = -1;
        bool stream = true;

        await Task.Run(() =>
        {
            LlamaAPIVRroom.GetPromptResponse(model, prompt, maxTokens, stream, (completionResponse) =>
            {
                // Loop through the choices and enqueue the response text
                foreach (var choice in completionResponse.choices)
                {
                    responseQueue.Enqueue(choice.text);
                }
            });
        });
    }


    private async void GenerateStreamingResponseAsyncAnythingLLM()
    {
        string message = "What is a " + this.gameObject.name;
        string mode = "query";

        await Task.Run(() =>
        {
            LlamaAPIVRroom.GetAnythingLLMStreaming( message, mode,(completionResponse) =>
            {
                responseQueue.Enqueue(completionResponse.textResponse);
            
            });
        });
    }

    private async void GenerateStreamingResponseAsyncAnythingLLMButton()
    {
        string message = inputQuestion.text;
        string mode = "query";

        await Task.Run(() =>
        {
            LlamaAPIVRroom.GetAnythingLLMStreaming(message, mode, (completionResponse) =>
            {
                responseQueue.Enqueue(completionResponse.textResponse);

            });
        });
    }

    /// <summary>
    /// /////////////////////////////////////////////////NOT REQUIRED//////////////////////////////////////////////////////////
    /// </summary>
    /// 

    public void OnTriggerStay(Collider other)
    {
        //output.text = outputHolder;     
    }

    private void GenerateStreamingResponse()
    {
        string model = "TheBloke/Mistral-7B-Instruct-v0.1-GGUF/mistral-7b-instruct-v0.1.Q3_K_L.gguf";
        string prompt = "what is a " + this.gameObject.name;
        int maxTokens = -1;
        bool stream = true;

        LlamaAPIVRroom.GetPromptResponse(model, prompt, maxTokens, stream, (completionResponse) =>
        {
            // Loop through the choices and update output.text
            foreach (var choice in completionResponse.choices)
            {
               outputHolder += choice.text;
               
            }
        });
        
    }
    private void GenerateStreamingResponseButton()
    {
        string model = "TheBloke/Mistral-7B-Instruct-v0.1-GGUF/mistral-7b-instruct-v0.1.Q3_K_L.gguf";
        string prompt = "what is a tree?";
        int maxTokens = -1;
        bool stream = true;

        LlamaAPIVRroom.GetPromptResponse(model, prompt, maxTokens, stream, (completionResponse) =>
        {
            // Loop through the choices and update output.text
            foreach (var choice in completionResponse.choices)
            {
                output.text += choice.text;

            }
        });
       
    }

    private void GenerateNonStreamingResponse()
    {
        //Non streaming
        string model = "TheBloke/Mistral-7B-Instruct-v0.1-GGUF/mistral-7b-instruct-v0.1.Q3_K_L.gguf";
        
        string prompt = "what is a " + this.gameObject.name;

        int maxTokens = -1;
        bool stream = false;

        LlamaPromptResponse promptOutput = LlamaAPIVRroom.GetPromptResponseNonStreaming(model, prompt, maxTokens, stream);
        //output.text = promptOutput.choices[0].text;
        outputHolder = promptOutput.choices[0].text;
       
    }
}
