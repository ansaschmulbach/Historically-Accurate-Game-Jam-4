using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{

    #region Inspector Variables

    [SerializeField] private Image spacer;
    [SerializeField] private Image grid;
    [SerializeField] private GameObject inventoryItemPrefab;

    #endregion

    #region Cached Variables

    private PlayerItemController playerItemC;
    private Inventory inventory;
    private List<GameObject> itemList;
    private bool open;

    #endregion

    #region Instantiation Method

    void Start()
    {
        playerItemC = FindObjectOfType<PlayerItemController>();
        inventory = playerItemC.inventory;
        itemList = new List<GameObject>();
        ClearInventory();
    }

    #endregion
    
    #region Clear and Fill Inventory Methods

    void FillInventory()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            Inventory.Item item = inventory.items[i];
            GameObject inventoryItem = CreateInventoryItemFromItem(item, i);
            itemList.Add(inventoryItem);
        }
    }

    void ClearInventory()
    {
        foreach (GameObject item in itemList)
        {
            Destroy(item);
        }
    }

    #endregion

    #region Utils

    public void RefreshInventory()
    {
        if (open)
        {
            ClearInventory();
            FillInventory();   
        }
    }
    
    GameObject CreateInventoryItemFromItem(Inventory.Item item, int index)
    {
        GameObject inventoryItem = Instantiate(inventoryItemPrefab, grid.transform);
        InventoryItem itemScript = inventoryItem.GetComponent<InventoryItem>();
        itemScript.index = index;
        itemScript.playerItemC = playerItemC;
        itemScript.ChangeSprite(item.sprite);
        return inventoryItem;
    }

    #endregion
    

}
