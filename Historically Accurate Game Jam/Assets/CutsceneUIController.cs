using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneUIController : MonoBehaviour
{
    public List<string> lines;
    [SerializeField] public Text UItext;
    [SerializeField] string nextScene;
    private int index;
    
    void Start()
    {
        index = 0;
        StartCoroutine(GoThroughLines());
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().frozen = true;
            player.GetComponent<PlayerCrafter>().frozen = true;
        }
    }

    IEnumerator GoThroughLines()
    {
        string line = lines[index];
        UItext.text = line;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        index++;
        if (index == lines.Count)
        {
            if (nextScene.Equals(""))
            {
                Unfreeze();
                yield break;
            }
            else
            {
                GameManager.instance.LoadScene(nextScene);
                yield break;
            }
        }
        
        yield return GoThroughLines();
    }

    void Unfreeze()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().frozen = false;
            player.GetComponent<PlayerCrafter>().frozen = false;
        }
        this.gameObject.SetActive(false);
    }
    
}
