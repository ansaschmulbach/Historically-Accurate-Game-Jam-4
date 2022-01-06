using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    public static Dialogue instance;
    [SerializeField] public List<DialoguePrompt> prompts;
    [SerializeField] public DialoguePrompt failureMessage;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}
