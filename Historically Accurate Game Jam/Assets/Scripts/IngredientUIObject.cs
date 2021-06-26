using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUIObject : MonoBehaviour
{
    private CraftingPanelUIController craftingUIController;
    [SerializeField] public Image ingredientImage;
    private Inventory.Item itemBeingHeld;

    void Start()
    {
        craftingUIController = GetComponentInParent<CraftingPanelUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        craftingUIController.PlaceItem(this);
    }

    public void SetItemBeingHeld(Inventory.Item item)
    {
        if (this.itemBeingHeld != null)
        {
            this.itemBeingHeld.heldInCrafting = false;
        }
        this.itemBeingHeld = item;
        this.itemBeingHeld.heldInCrafting = true;
        this.ingredientImage.sprite = item.sprite;
    }

    public bool IsEmpty()
    {
        return this.itemBeingHeld == null;
    }
    
    public bool IsHolding(Inventory.Item item)
    {
        if (this.itemBeingHeld == null)
        {
            return false;
        }
        return this.itemBeingHeld.Equals(item);
    }
    
    public void ReturnObjectToInv()
    {
        this.itemBeingHeld.heldInCrafting = false;
        this.itemBeingHeld = null;
        this.ingredientImage.sprite = null;
    }

    public Inventory.Item GetItemHeld()
    {
        return itemBeingHeld;
    }
    
}
