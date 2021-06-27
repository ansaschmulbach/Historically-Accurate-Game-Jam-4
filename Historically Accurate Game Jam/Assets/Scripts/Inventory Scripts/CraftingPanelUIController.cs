using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CraftingPanelUIController : MonoBehaviour
{

    private PlayerItemController playerItemController;
    [SerializeField] private IngredientUIObject outputContainer;
    private List<Inventory.Item> itemsBeingHeld;
    private CraftingRecipes craftingRecipes;
    private InventoryUIController inventoryUIController;
    private PlayerCrafter playerCrafter;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerItemController = player.GetComponent<PlayerItemController>();
        playerCrafter = player.GetComponent<PlayerCrafter>();
        craftingRecipes = FindObjectOfType<CraftingRecipes>();
        inventoryUIController = FindObjectOfType<InventoryUIController>();
        itemsBeingHeld = new List<Inventory.Item>();
        
    }

    public void PlaceItem(IngredientUIObject ingredientContainer)
    {
        if (playerItemController.HasHandEquipped()) return;
        int selectedIndex = playerItemController.selectedIndex;
        Inventory.Item item = playerItemController.inventory.Get(selectedIndex);
        if (ingredientContainer.IsHolding(item))
        {
            itemsBeingHeld.Remove(item);
            ingredientContainer.ReturnObjectToInv();
            return;
        } 
        // else if (!ingredientContainer.IsEmpty())
        // {
        //     itemsBeingHeld.Remove(ingredientContainer.GetItemHeld());
        //     ingredientContainer.ReturnObjectToInv();
        // }
        else if (item.heldInCrafting)
        {
            return;
        }
        ingredientContainer.SetItemBeingHeld(item);
        itemsBeingHeld.Add(item);
    }

    public void Craft()
    {
        CraftingRecipes.Recipe recipe = craftingRecipes.RecipeOutput(itemsBeingHeld);
        Collectable col = recipe.result;
        if (col == null) return;
        IngredientUIObject[] ingredientContainers = GetComponentsInChildren<IngredientUIObject>();
        foreach (IngredientUIObject ingredientContainer in ingredientContainers)
        {
            if (!ingredientContainer.IsEmpty())
            {
                ingredientContainer.ReturnObjectToInv();   
            }
        }
        foreach (Inventory.Item item in itemsBeingHeld)
        {
            playerItemController.inventory.RemoveItem(item);
            Debug.Log("item");
        }
        Inventory.Item added = playerItemController.inventory.AddItem(col);
        playerItemController.selectedIndex = 0;
        inventoryUIController.RefreshInventory();
        outputContainer.SetItemBeingHeld(added);
        playerCrafter.frozen = true;
        StartCoroutine(PlaySoundThenLoadScene("Crafting", recipe.nextSceneName));
    }
    
    public IEnumerator PlaySoundThenLoadScene(string soundName, string name)
    {
        yield return AudioManager.instance.WaitForPlay(soundName);
        GameManager.instance.LoadScene(name);
    }


}
