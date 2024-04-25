using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System.Net.Http;
using System;
using static LlamaPromptResponse;
using Unity.VisualScripting;

public static class LlamaAPI
{
    //Non streaming
    //public static LlamaPromptResponse GetPromptResponse(string model, string prompt, int maxTokens, bool stream)
    
    public static void GetPromptResponse(string model, string prompt, int maxTokens, bool stream, Action<LlamaPromptResponse> callback)
    {
        //Constructing JSON payload
        ApiJsonPayload requestPayload = new ApiJsonPayload
        {
            model = model,
            prompt = prompt,
            max_tokens = maxTokens,
            stream  = stream
        };

        //Serialize the payload to JSON
        string jsonPayload = JsonUtility.ToJson(requestPayload);

        //Creat HTTP POST request 
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:1234/v1/completions");
        request.Method = "POST";
        request.ContentType = "application/json";

        // Write the JSON payload to the request body
        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
        {
            writer.Write(jsonPayload);
        }

        ////Non streaming
        ////send request and get response
        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        ////read JSON response body
        //StreamReader reader = new StreamReader(response.GetResponseStream());
        //string json = reader.ReadToEnd();
        ////Deserialize JSON to LlamaPromptResponse object type
        //return JsonUtility.FromJson<LlamaPromptResponse>(json);


        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            // Read the response stream
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //to ignore the empty JSONS
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    if (line == "data: [DONE]")
                    {
                        //Debug.Log("Done parsing!");
                        break;
                    }

                    //Deserialize JSON to LlamaPromptResponse object type
                    LlamaPromptResponse completionResponse = JsonUtility.FromJson<LlamaPromptResponse>(line.Substring("data:".Length));
                    //Debug.Log(completionResponse.choices[0].text);
                    
                    callback.Invoke(completionResponse);

                }

            }
        }

    }
   
}
