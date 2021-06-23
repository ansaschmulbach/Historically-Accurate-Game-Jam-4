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

        public Item(string name, string address, Sprite sprite)
        {
            this.name = name;
            this.address = address;
            this.sprite = sprite;
            this.quantity = 1;
        }
    }

    public List<Item> items;

    public Inventory(Sprite emptySprite)
    {
        items = new List<Item>();
        items.Add(new Item("hand", "", emptySprite));
    }

    public void AddItem(Collectable collectable)
    {
        Item item = items.Find(i => i.name.Equals(collectable.itemName));
        if (item == null)
        {
            item = new Item(collectable.itemName, 
                collectable.address, 
                collectable.GetInventorySprite());
            items.Add(item);
        }
        else
        {
            item.quantity += 1;
        }
        
    }

    public GameObject DropItem(int index)
    {
        Item item = items[index];
        GameObject ret = Resources.Load(item.address) as GameObject;
        items.RemoveAt(index);
        return ret;
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
