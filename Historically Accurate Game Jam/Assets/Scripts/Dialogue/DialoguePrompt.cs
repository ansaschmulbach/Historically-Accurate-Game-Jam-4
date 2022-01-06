using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialoguePrompt
{
    
    public string speaker;
    public string prompt;
    public int correctIndex;
    public float secondsToWait;
    public List<string> options;
    public List<string> optionsDialogue;

}
