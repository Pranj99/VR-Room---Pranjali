using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UILLAMA : MonoBehaviour
{
    ////Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public TextMeshProUGUI output;
    public string outputHolder;


    public void OkayButton()
    {
        output.text = "";
        StartCoroutine(GenerateStreamingResponseButton());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RightHand Controller")
        {

            //GenerateNonStreamingResponse();
            StartCoroutine(GenerateStreamingResponse());

            ////Simple Object picking test
            //output.text = "this is a " + this.gameObject.name;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        output.text = outputHolder;     
    }
    public void OnTriggerExit(Collider other)
    {
        output.text = "";
    }

    IEnumerator GenerateStreamingResponse()
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
        yield return null;
    }

    IEnumerator GenerateStreamingResponseButton()
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
        yield return null;
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
