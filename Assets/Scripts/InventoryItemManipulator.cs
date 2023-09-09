using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemManipulator : MonoBehaviour
{
    public GameObject Player;
    public void IncreaseTreeSize()
    {
        transform.parent.transform.localScale += new Vector3(transform.parent.transform.localScale.x*.2f,transform.parent.transform.localScale.y*.2f,transform.parent.transform.localScale.z*.2f);
    }

    public void DecreaseTreeSize()
    {
       transform.parent.transform.localScale -= new Vector3(transform.parent.transform.localScale.x*.2f,transform.parent.transform.localScale.y*.2f,transform.parent.transform.localScale.z*.2f);
    }
    public void DeleteItem()
    {
        Destroy(transform.parent.transform.parent.gameObject);
    }

    public void RotateObject()
    {
        transform.parent.transform.Rotate(0, 10, 0,Space.World);
    }


    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Player.transform.position);
        //transform.LookAt(Player.transform);
    }
    public void Start() 
    {
        
       Player = GameObject.Find("XR Origin");
    }
}
