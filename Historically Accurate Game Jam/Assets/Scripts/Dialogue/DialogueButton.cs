using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueButton : MonoBehaviour
{

    [SerializeField] public Text textField;
    public int index;
    private DialogueUIController dialogueController;

    void Start()
    {
        dialogueController = FindObjectOfType<DialogueUIController>();
    }
    
    public void Select()
    {
        dialogueController.selected = index;
    }
    
    
}
