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
        if (scene.name == "Lyre Scene")
        {
            playerCrafter.enabled = true;
        }
        else if (playerCrafter != null)
        {
            playerCrafter.enabled = false;
        }
    }
    
    
}
