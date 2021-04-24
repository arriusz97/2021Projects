using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CraftingRecipe : MonoBehaviour
{
    public ItemObject result;
    public Ingredient[] ingredients;
    public ActionController Player;

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ActionController>();
    }

    private bool CanCraft()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            bool containsCurrentIngredient = Player.inventory.IsItemInInventory(ingredient.item);

            if (!containsCurrentIngredient)
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveIngredientsFromInventory()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            Player.inventory.FindItemOnInventory(ingredient.item.data).RemoveItem();
        }
    }

    public void Crafting()
    {
        if (CanCraft())
        {
            RemoveIngredientsFromInventory();
            Player.inventory.AddItem(result.data, 1);
        }
        else
        {
            Debug.Log("You dont have enaugh ingredients to craft: " + result.data.Name);
        }
    }

    [System.Serializable]
    public class Ingredient
    {
        public ItemObject item;
        public int amount;
    }
}