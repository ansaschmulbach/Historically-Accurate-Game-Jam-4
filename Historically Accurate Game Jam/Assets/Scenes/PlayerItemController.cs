using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.UIElements;
using UnityEngine;


//git comment

/**
 * This MonoBehaviour handles all interaction between the player and outside items,
 * ex. collectables, interactables, and their inventory
 */
public class PlayerItemController : MonoBehaviour
{

    #region Inventory Variables

    [SerializeField] private float collectRadius;
    [SerializeField] private Sprite emptySprite;
    [NonSerialized] public Inventory inventory;
    [NonSerialized] public int selectedIndex;
    public Collectable hoveredCollectable;

    #endregion

    #region Cached Variables

    [NonSerialized]
    public InventoryUIController invUIController;
    private PlayerMovement movementScript;

    #endregion

    #region Unity Instantiation Methods

    private void Awake()
    {
        inventory = new Inventory(emptySprite);
        selectedIndex = 0;
    }

    private void Start()
    {
        invUIController = FindObjectOfType<InventoryUIController>();
        movementScript = FindObjectOfType<PlayerMovement>();
    }


    #endregion

    #region Unity Update Methods

    void Update()
    {

        if (Input.GetKeyDown("e") && !inventory.IsEmpty())
        {
            DropItem(selectedIndex);
        }
        
        HoveredCollectableUpdate();

    }


    #endregion
    
    #region Inventory Methods

    void DropItem(int index)
    {
        if (inventory.isHand(index) || index >= inventory.items.Count) return;
        Vector3 unitFacing = movementScript.UnitVectorFacing();
        Vector3 feetCenter = movementScript.feetCenter();
        Vector3 instantiatePos = new Vector2(feetCenter.x, feetCenter.y) 
                                 + new Vector2(unitFacing.x, unitFacing.y);
        instantiatePos.z = FurnitureManager.instance.normalize(instantiatePos.y);
        Instantiate(inventory.DropItem(index), instantiatePos, Quaternion.identity);
        invUIController.RefreshInventory();
    }
    
    public void EquipItem(int index)
    {
        selectedIndex = index;
        Debug.Log(inventory.items[index].name);
    }

    #endregion

    #region Collection

    public void HoverItem(Collectable hoveredObj)
    {
        this.hoveredCollectable = hoveredObj;
        HoveredCollectableUpdate();
    }

    public void ClearHover()
    {
        this.hoveredCollectable = null;
        HoveredCollectableUpdate();
    }

    void HoveredCollectableUpdate()
    {
        if (hoveredCollectable == null)
        {
            return;
        }
        
        if (Vector2.SqrMagnitude(hoveredCollectable.transform.position - this.transform.position) <= collectRadius)
        {
            hoveredCollectable.hovered = true;
            hoveredCollectable.UpdateSpriteRendererHover();
        }
        else
        {
            hoveredCollectable.hovered = false;
            hoveredCollectable.UpdateSpriteRendererHover();
        }
    }
    
    public void CollectItem(Collectable selectedObj)
    {
        inventory.AddItem(selectedObj);
        Destroy(selectedObj.gameObject);
        invUIController.RefreshInventory();
    }

    #endregion
    
    #region General Utils

    bool HasHandEquipped()
    {
        return inventory.isHand(selectedIndex);
    }

    #endregion

}
