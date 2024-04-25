using System.Collections.Generic;

[System.Serializable]
public class LlamaPromptResponse
{
    
    public string id;
    public string @object;
    public int created;
    public string model;
    public List<Choice> choices;
    // public Usage usage;

    [System.Serializable]
    public class Choice
    {
        public int index;
        public string text;
        public object logprobs;
        public object finish_reason;
    }

    //[System.Serializable]
    //public class Usage
    //{
    //    public int prompt_tokens;
    //    public int completion_tokens;
    //    public int total_tokens;
    //}


}
