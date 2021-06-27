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
    
}
