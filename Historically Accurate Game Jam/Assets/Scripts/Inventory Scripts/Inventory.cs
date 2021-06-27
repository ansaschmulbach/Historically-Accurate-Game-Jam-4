using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public class Item
    {
        public string name;
        public string address;
        public Sprite sprite;
        public int quantity;

        #region Crafting Variables

        public bool heldInCrafting;

        #endregion

        public Item(string name, string address, Sprite sprite)
        {
            this.name = name;
            this.address = address;
            this.sprite = sprite;
            this.quantity = 1;
            heldInCrafting = false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Item)) return false;
            return Equals((Item) obj);
        }

        public bool Equals(Item other)
        {
            return other.name.Equals(this.name);
        }
        
    }

    public List<Item> items;

    public Inventory(Sprite emptySprite)
    {
        items = new List<Item>();
        items.Add(new Item("hand", "", emptySprite));
    }

    public Item AddItem(Collectable collectable)
    {
        Item item = items.Find(i => i.name.Equals(collectable.itemName));
        if (item == null)
        {
            item = new Item(collectable.itemName, 
                collectable.GetAddress(), 
                collectable.GetInventorySprite());
            items.Add(item);
        }
        else
        {
            item.quantity += 1;
        }
        return item;
    }

    public void RemoveItem(Item i)
    {
        items.Remove(i);
    }

    public GameObject DropItem(int index)
    {
        Item item = items[index];
        GameObject ret = Resources.Load(item.address) as GameObject;
        items.RemoveAt(index);
        return ret;
    }

    public Item Pop(int index)
    {
        Item item = items[index];
        items.RemoveAt(index);
        return item;
    }

    public Item Get(int index)
    {
        return items[index];
    }

    public bool IsEmpty()
    {
        return items.Count == 1;
    }

    public bool isHand(int index)
    {
        return index == 0;
    }
    
}
