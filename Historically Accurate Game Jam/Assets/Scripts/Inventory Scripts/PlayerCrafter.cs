using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafter : MonoBehaviour
{
    private RectTransform craftingPanelUI;
    private CraftingPanelUIController craftingUIController;
    private bool craftingEnabled;
    void Start()
    {
        craftingUIController = FindObjectOfType<CraftingPanelUIController>();
        craftingPanelUI = craftingUIController.GetComponent<RectTransform>();
        craftingEnabled = false;
        craftingPanelUI.gameObject.SetActive(craftingEnabled);
    }

    // Update is called once per frame
    void Update()
    {
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
