using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCrafter : MonoBehaviour
{
    private RectTransform craftingPanelUI;
    private CraftingPanelUIController craftingUIController;
    private bool craftingEnabled;
    public bool frozen;

    void Start()
    {
        craftingUIController = FindObjectOfType<CraftingPanelUIController>();
        craftingEnabled = false;
        craftingPanelUI = craftingUIController.GetComponent<RectTransform>();
        craftingPanelUI.gameObject.SetActive(craftingEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen) return;
        if (Input.GetKeyDown("f"))
        {
            ToggleCrafting();
        }
    }

    void ToggleCrafting()
    {
        craftingEnabled = !craftingEnabled;
        craftingPanelUI.gameObject.SetActive(craftingEnabled);
    }

}
