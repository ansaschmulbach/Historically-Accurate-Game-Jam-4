using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{

    #region Inspector Variables
    
    [SerializeField] private Image itemImage;

    #endregion

    #region Cached Variables

    #endregion

    #region Public Variables

    [NonSerialized] public int index;
    [NonSerialized] public PlayerItemController playerItemC;

    #endregion
    

    //When button is pressed, this is called
    public void Select()
    {
        Debug.Log("hi");
        playerItemC.EquipItem(this.index);
    }

    public void ChangeSprite(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
    

}
