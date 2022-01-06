using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    void Start()
    {
        if (instance == null || instance == this)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadGuardScene()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("Guard Scene");
        }
        SceneManager.LoadScene("Guard Scene");
    }

    public void LoadLyreCutscene()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("General Theme");
        }

        SceneManager.LoadScene("Lyre Cutscene");
    }

    public void LoadLyreScene()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("General Theme");
        }
        SceneManager.LoadScene("Lyre Scene");
    }

    public void LoadDialogueScene()
    {
        SceneManager.LoadScene("Dialogue Scene");
    }

    public void LoadSandalsScene()
    {
        SceneManager.LoadScene("Sandals");
    }

    public void LoadGuardCutscene()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("Cows");
        }

        SceneManager.LoadScene("Guard Cutscene");

    }
    
    public IEnumerator WaitThenLoadScene(float seconds, string name)
    {
        yield return new WaitForSeconds(seconds);
        LoadScene(name);
    }

    public void LoadScene(string name)
    {
        switch (name)
        {
            case "Guard Scene":
                LoadGuardScene();
                break;
            case "Lyre Scene":
                LoadLyreScene();
                break;
            case "Dialogue Scene":
                LoadDialogueScene();
                break;
            case "Guard Cutscene":
                LoadGuardCutscene();
                break;
            default:
                Debug.LogWarning("no scene named " + name);
                break;
        }
    }
    
}
