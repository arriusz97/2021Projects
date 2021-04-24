using System;

[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public ItemType itemType;



    public Item()
    {
        Name = "";
        Id = -1;
    }

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        itemType = item.type;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].Min, item.data.buffs[i].Max)
            {
                stat = item.data.buffs[i].stat
            };
        }
    }
}