using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public bool stackable;
    public ItemType type;
    [TextArea(15, 20)] 
    public string description;
    public Item data = new Item();
    [Serializable]
    public class Recipe
    {
        public ItemObject result;
        public int resultamount = 1;
        public Ingredient[] ingredients;
    }
    public Recipe RecipeData;

    private ActionController Player;

    //제작 레시피라면 플레이어 인벤토리 참조를 위해 플레이어와 연결
    private void Start()
    {
        if (type == ItemType.Recipe)
        {
            Player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ActionController>();
            Debug.Log("Play Connected");
        }        
    }
    //플레이어 인벤토리에 제작에 필요한 재료가 있는지 확인
    private bool CanCraft()
    {
        foreach (Ingredient ingredient in RecipeData.ingredients)
        {
            bool containsCurrentIngredient = Player.inventory.IsItemInInventory(ingredient.item);

            if (!containsCurrentIngredient)
            {
                return false;
            }
        }
        return true;
    }
    //플레이어 인벤토리의 재료 아이템을 제거
    private void RemoveIngredientsFromInventory()
    {
        foreach (Ingredient ingredient in RecipeData.ingredients)
        {
            Player.inventory.FindItemOnInventory(ingredient.item.data).AddAmount(-ingredient.amount);
        }
    }
    //제작이 가능하면 결과물을 플레이어 인벤토리에 추가해준다
    public void Crafting()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ActionController>();
        if (CanCraft())
        {
            RemoveIngredientsFromInventory();
            Player.inventory.AddItem(RecipeData.result.data, RecipeData.resultamount);
        }
        else
        {
            Debug.Log("You dont have enaugh ingredients to craft: " + RecipeData.result.data.Name);
        }
    }
    
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    [System.Serializable]
    public class Ingredient
    {
        public ItemObject item;
        public int amount;
    }
}