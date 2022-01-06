using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipes : MonoBehaviour
{

    [SerializeField] public List<Recipe> craftingRecipes;
    
    [Serializable]
    public class Recipe
    {
        [SerializeField] private List<Collectable> recipeCollectables;
        [SerializeField] public Collectable result;
        [SerializeField] public string nextSceneName;
        [NonSerialized] private List<Inventory.Item> recipeItems;

        public void InstantiateRecipe() {
            recipeItems = new List<Inventory.Item>();
            foreach (Collectable c in recipeCollectables)
            {
                recipeItems.Add(new Inventory.Item(c.itemName,
                    c.address,
                    c.GetInventorySprite()
                    ));
            }
        }

        public bool MatchesItemList(List<Inventory.Item> items)
        {
            Debug.Log(items.Count + ", " + recipeItems.Count);
            if (items.Count != recipeItems.Count) return false;
            foreach (Inventory.Item item in items)
            {
                if (!recipeItems.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
        
    }

    void Start()
    {
        foreach (Recipe r in craftingRecipes)
        {
            r.InstantiateRecipe();
        }
    }

    public Recipe RecipeOutput(List<Inventory.Item> items)
    {
        foreach (Recipe r in craftingRecipes)
        {
            if (r.MatchesItemList(items))
            {
                return r;
            }
        }
        return null;
    }
    
    
}
