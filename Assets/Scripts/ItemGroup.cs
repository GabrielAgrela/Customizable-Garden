using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemGroup 
{
    // Define your variables here
    public string title;
    public ItemButtonGroup[] groupButtons;
    public GameObject groupPrefab;
    public GameObject groupButtonPrefab;
    

    // You can use a constructor to set these values
    public ItemGroup(string title, ItemButtonGroup[] groupButtons, GameObject groupPrefab, GameObject groupButtonPrefab)
    {
        this.title = title;
        this.groupPrefab = groupPrefab;
        this.groupButtonPrefab = groupButtonPrefab;
        this.groupButtons = groupButtons;
    }

    // You can also use a method to set these values
    public void SetValues(string newTitle, ItemButtonGroup[] newGroupButtons, GameObject newGroupPrefab, GameObject newGroupButtonPrefab)
    {
        title = newTitle;
        groupButtons = newGroupButtons;
        groupButtonPrefab = newGroupButtonPrefab;
        groupPrefab = newGroupPrefab;

    }
}
