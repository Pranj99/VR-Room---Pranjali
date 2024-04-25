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
   

    public void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject.name == "RightHand Controller")
        {
            string model = "TheBloke/Mistral-7B-Instruct-v0.1-GGUF/mistral-7b-instruct-v0.1.Q3_K_L.gguf";
            string prompt = "what is a " + this.gameObject.name;
            int maxTokens = -1;
            bool stream = true;

            StartCoroutine(StreamPromptResponse(model, prompt, maxTokens, stream));
            //output.text = "this is a " + this.gameObject.name;
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        output.text = "";
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
