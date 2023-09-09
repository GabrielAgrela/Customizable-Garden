using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemButtonGroup 
{
    // Define your variables here
    public string title;
    public GameObject itemPrefab;
    public Sprite icon;

    // You can use a constructor to set these values
    public ItemButtonGroup(string title, GameObject itemPrefab, Sprite icon)
    {
        this.title = title;
        this.itemPrefab = itemPrefab;
        this.icon = icon;
    }

    // You can also use a method to set these values
    public void SetValues(string newTitle, GameObject newItemPrefab, Sprite newIcon)
    {
        title = newTitle;
        itemPrefab = newItemPrefab;
        icon = newIcon;

    }
}
