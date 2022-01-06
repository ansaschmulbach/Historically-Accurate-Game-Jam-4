using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIController : MonoBehaviour
{

    #region Inspector Variables

    [SerializeField] private Image grid;
    [SerializeField] private GameObject dialogueButtonPrefab;
    [SerializeField] private Text mainText;
    [SerializeField] private Text speakerText;

    #endregion

    #region Current Dialogue Prompt

    private DialoguePrompt currentPrompt;
    private int index;
    private List<GameObject> instantiatedButtons;
    public int selected = -1;
    private bool spacePressed;
    private bool completed;

    #endregion
    
    void Start()
    {
        index = 0;
        spacePressed = false;
        currentPrompt = Dialogue.instance.prompts[index];
        instantiatedButtons = new List<GameObject>();
        StartCoroutine(DisplayNextText());
    }

    private void Update()
    {
        if (completed)
        {
            
        }
    }

    IEnumerator DisplayNextText()
    {
        speakerText.text = currentPrompt.speaker;
        if (currentPrompt.options.Count == 0)
        {
            mainText.text = currentPrompt.prompt;
        }
        else
        {
            
            mainText.text = "";
            int i = 0;
            
            foreach (string option in currentPrompt.options)
            {
                GameObject button = Instantiate(dialogueButtonPrefab, grid.transform);
                DialogueButton buttonScript = button.GetComponent<DialogueButton>();
                buttonScript.textField.text = option;
                buttonScript.index = i;
                i++;
                instantiatedButtons.Add(button);
            }
            
            yield return new WaitUntil(() => selected >= 0 && selected < currentPrompt.options.Count);
            ClearButtons();
            mainText.text = currentPrompt.optionsDialogue[selected];
            
        }
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        bool completed = SelectOptions(selected);
        if (completed) yield break;
        selected = -1;
        yield return DisplayNextText();
        
    }

    bool SelectOptions(int index)
    {
        if (currentPrompt.options.Count == 0 || currentPrompt.correctIndex == index)
        {
            this.index += 1;
            if (this.index == Dialogue.instance.prompts.Count)
            {
                return true;
            }
            currentPrompt = Dialogue.instance.prompts[this.index];
        }
        else
        {
            currentPrompt = Dialogue.instance.failureMessage;
            this.index -= 1;   
        }

        return false;

    }

    void ClearButtons()
    {
        foreach (GameObject button in instantiatedButtons)
        {
            Destroy(button);
        }
    }

}
