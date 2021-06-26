using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour
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
        itemImage.color = new Color(0.8f, 0.8f, 0.8f, 1);
        playerItemC.EquipItem(this.index);
    }

    public void Deselect()
    {
        itemImage.color = Color.white;
    }

    public void ChangeSprite(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
    

}
