[System.Serializable]
public class DialogueLine
{
    public string id;
    public string speaker;
    public string text;
    public string next;
    public Choice[] choices;
    public string action;
}

[System.Serializable]
public class Choice
{
    public string text;
    public string next;
    public string action;
}
