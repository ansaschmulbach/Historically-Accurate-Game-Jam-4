using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScriptManager : MonoBehaviour
{

    [NonSerialized] private PlayerCrafter playerCrafter;
    
    void Awake()
    {
        playerCrafter = GetComponent<PlayerCrafter>();
        SceneManager.sceneLoaded += TogglePlayerCrafter;
    }

    void TogglePlayerCrafter(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name.Equals("Lyre Scene") || scene.name.Equals("Sandals"))
        {
            playerCrafter.frozen = false;
        }
        else if (playerCrafter != null)
        {
            playerCrafter.frozen = true;
        }
    }
    
    
}
