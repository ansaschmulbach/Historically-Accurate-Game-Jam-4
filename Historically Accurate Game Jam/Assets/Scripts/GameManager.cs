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
        SceneManager.LoadScene("Guard Scene");
    }

    public void LoadLyreScene()
    {
        SceneManager.LoadScene("Lyre Scene");
    }
    
}
