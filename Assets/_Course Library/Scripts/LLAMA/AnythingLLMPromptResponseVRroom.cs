
using System.Collections.Generic;
using static Unity.VisualScripting.Member;

[System.Serializable]


public class AnythingLLMPromptResponseVRroom
{
    //public string id;
    //public string type;
    //public bool close;
    //public object error;
    //public int chatId;
    //public string textResponse;
    //public List<Source> sources;

    //[System.Serializable]

    //public class Source
    //{
    //    public string id;
    //    public string url;
    //    public string title;
    //    public string docAuthor;
    //    public string description;
    //    public string docSource;
    //    public string chunkSource;
    //    public string published;
    //    public int wordCount;
    //    public int token_count_estimate;
    //    public string text;
    //    public double _distance;
    //    public double score;
    //}

    public string uuid;
    public List<Source> sources;
    public string type;
    public string textResponse;
    public bool close;
    public bool error;

    [System.Serializable]
    public class Source
    {
        public string id;
        public string url;
        public string title;
        public string docAuthor;
        public string description;
        public string docSource;
        public string chunkSource;
        public string published;
        public int wordCount;
        public int token_count_estimate;
        public string text;
        public double _distance;
        public double score;
    }

}
