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

    private void Awake()
    {
        playerItemController = GameObject.FindWithTag("Player").GetComponent<PlayerItemController>();
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
        Collectable col = craftingRecipes.recipeOutput(itemsBeingHeld);
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
    }

}
