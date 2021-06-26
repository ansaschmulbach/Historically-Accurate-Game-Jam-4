using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{

     #region Inspector Variables
     
     [Header("Optional")] 
     [SerializeField] public string itemName;
     [SerializeField] public Sprite inventorySprite;

     #endregion

     #region Public Variables

     [NonSerialized]
     public string address;

     #endregion

     #region Private Variables
     
     protected SpriteRenderer spriteRenderer;
     private PlayerItemController playerItemController;
     [SerializeField] public bool hovered;
     
     #endregion

     private void Start()
     {
          playerItemController = FindObjectOfType<PlayerItemController>();
          spriteRenderer = GetComponent<SpriteRenderer>();
          address = GetAddress();
          inventorySprite = inventorySprite == null ? this.GetComponent<SpriteRenderer>().sprite : inventorySprite;
          itemName = String.IsNullOrEmpty(itemName) ? this.gameObject.name : itemName;
     }

     #region Mouse Methods

     public void OnMouseDown()
     {
          if (hovered)
          {
               playerItemController.CollectItem(this);
          }
     }

     public void OnMouseEnter()
     {
          playerItemController.HoverItem(this);
          UpdateSpriteRendererHover();
     }

     public void OnMouseExit()
     {
          playerItemController.ClearHover();
          this.hovered = false;
          UpdateSpriteRendererHover();
     }

     public void UpdateSpriteRendererHover()
     {
          if (this.hovered)
          {
               this.spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f, 1);
          }
          else
          {
               this.spriteRenderer.color = Color.white;
          }
     }

     #endregion

     #region Virtual Methods
     
     public virtual Sprite GetInventorySprite()
     {
          if (inventorySprite == null)
          {
               inventorySprite = this.GetComponent<SpriteRenderer>().sprite;
          }

          return inventorySprite;
     }

     public virtual string GetAddress()
     {
          return "Collectables/" + gameObject.name.Replace("(Clone)", "").Trim();
     }

     #endregion
     

}
