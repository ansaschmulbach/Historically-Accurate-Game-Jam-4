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
            Debug.Log("hi");
            AudioManager.instance.Play("Guard Scene");
        }
        SceneManager.LoadScene("Guard Scene");
    }

    public void LoadLyreScene()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("General Theme");
        }
        SceneManager.LoadScene("Lyre Scene");
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
            default:
                Debug.LogWarning("no scene named " + name);
                break;
        }
    }
    
}
