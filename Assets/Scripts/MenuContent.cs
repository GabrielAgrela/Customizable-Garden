using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine.Networking;
using System.IO;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public Vector3Serializable position;
    public QuaternionSerializable rotation;
    public Vector3Serializable size;
}

[System.Serializable]
public class Vector3Serializable
{
    public float x;
    public float y;
    public float z;

    public Vector3Serializable(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public class QuaternionSerializable
{
    public float x;
    public float y;
    public float z;
    public float w;

    public QuaternionSerializable(Quaternion q)
    {
        x = q.x;
        y = q.y;
        z = q.z;
        w = q.w;
    }

    public Quaternion ToQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
}
[System.Serializable]
public class ItemDataList
{
    public List<ItemData> items = new List<ItemData>();
}

public class MenuContent : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ItemGroup> myCustomClasses = new List<ItemGroup>();
    public List<GameObject> spawnedItems = new List<GameObject>();
    public List<ItemData> itemDataList = new List<ItemData>();
    public SpawnToHit spawnToHit;
    public GameObject coco;
    public GameObject Content;
    public ChestScript CS;
    public string username;

    void Start()
    {
        CS = GameObject.Find("Chest").GetComponent<ChestScript>();
        LoadItemsFromJSON();
        /*GameObject batata = Instantiate(batataPrefab, coco.transform.position, Quaternion.identity, coco.transform);
        batata.name = "batata";*/

        foreach (var itemGroup in myCustomClasses)
        {
            int i=-1400;
            GameObject itemGroupCreated = Instantiate(itemGroup.groupPrefab, Content.transform.position, Quaternion.identity, Content.transform);
            itemGroupCreated.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemGroup.title;
            foreach (var itemButtonGroup in itemGroup.groupButtons)
            {
                GameObject itemButtonGroupCreated = Instantiate(itemGroup.groupButtonPrefab, itemGroupCreated.transform.position, Quaternion.identity, itemGroupCreated.transform.GetChild(1).transform.GetChild(0).transform);
                itemButtonGroupCreated.transform.localPosition = new Vector3(itemGroupCreated.transform.localPosition.x + i, itemGroupCreated.transform.localPosition.y, itemGroupCreated.transform.localPosition.z);
                itemButtonGroupCreated.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemButtonGroup.title;
                itemButtonGroupCreated.transform.GetChild(0).GetComponent<Image>().sprite = itemButtonGroup.icon;
                itemButtonGroupCreated.transform.GetComponent<Button>().onClick.AddListener(() => SpawnItem(itemButtonGroup.itemPrefab));
                i+=270;
            }
            
        }

    }

    public void SpawnItem(GameObject itemPrefab)
    {
        /*GameObject item = Instantiate(itemPrefab, Content.transform.position, Quaternion.identity);
        item.name = itemPrefab.name;
        spawnedItems.Add(item);*/
        spawnToHit.StartSpawnWithDelay(itemPrefab);
        
    }

    public void SaveItemsToJSON()
    {
        itemDataList.Clear();
        foreach (var item in spawnedItems)
        {
            ItemData itemData = new ItemData
            {
                itemName = item.name,
                position = new Vector3Serializable(item.transform.GetChild(0).transform.position),
                rotation = new QuaternionSerializable(item.transform.GetChild(0).transform.rotation),
                size = new Vector3Serializable(item.transform.GetChild(0).transform.localScale)
            };

            itemDataList.Add(itemData);
        } 
        try
        {
            foreach (var item2 in CS.chestPositionsCurr)
            {
                ItemData itemData = new ItemData
                {
                    itemName = item2.name,
                    position = new Vector3Serializable(item2.transform.transform.position),
                    rotation = new QuaternionSerializable(item2.transform.transform.rotation),
                    size = new Vector3Serializable(item2.transform.transform.localScale)
                };

                itemDataList.Add(itemData);
            } 
        }
        catch (System.Exception)
        {

        }
        
        print("hey");
        ItemDataList listWrapper = new ItemDataList();
        listWrapper.items = itemDataList;
        string jsonData = JsonUtility.ToJson(listWrapper, true);
        File.WriteAllText(Application.persistentDataPath + "/itemData.json", jsonData);
    }

    public void LoadItemsFromJSON()
    {
        int chestI = 0;
        string path = Application.persistentDataPath + "/itemData.json";
        if (File.Exists(path))
        {
            // Read the json from the file into a string
            string jsonData = File.ReadAllText(path);
            // Deserialize the json data into an object
            ItemDataList loadedData = JsonUtility.FromJson<ItemDataList>(jsonData);
            
            // Iterate through the data, spawning each item
            foreach (ItemData itemData in loadedData.items)
            {
                // You'll need some way to get the correct prefab for each item based on its name
                GameObject itemPrefab = GetPrefabByName(itemData.itemName);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity);
                    

                    if (itemData.itemName.Contains("Message"))
                    {
                        item.name = itemData.itemName;
                        item.transform.transform.position = itemData.position.ToVector3();
                        item.transform.transform.rotation = itemData.rotation.ToQuaternion();
                        CS.chestPositionsCurr[chestI]=item;

                        try
                        {
                            string messageContent = GetMessageContentById(chestI);
                            item.GetComponent<MessageScript>().changeMessageContent(messageContent,chestI); 
                            item.GetComponent<MessageScript>().id=chestI;
                        }
                        catch (System.Exception)
                        {
                            string folderPath = Path.Combine(Application.persistentDataPath, "MyGameSaveFolder", username);
                            string fileName = Path.Combine(folderPath, chestI.ToString() + ".wav");
                            item.GetComponent<AudioMessage>().id=chestI;
                            StartCoroutine(GetAudioClip2(fileName,item));
                        }

                        

                        chestI++;
                    }
                    else
                    {
                        item.name = itemData.itemName;
                        item.transform.GetChild(0).transform.position = itemData.position.ToVector3();
                        item.transform.GetChild(0).transform.localScale = itemData.size.ToVector3();
                        item.transform.GetChild(0).transform.rotation = itemData.rotation.ToQuaternion();
                    }
                }
                
            }
        }
        else
        {
            Debug.Log("No save file found at " + path);
        }
    }

    public IEnumerator GetAudioClip2(string fullPath, GameObject item)
    {
        print(fullPath);
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                item.GetComponent<AudioSource>().clip = myClip;
                //audioSource.Play();
            }
        }
    }

    private string GetMessageContentById(int id)
    {
        string path = Path.Combine(Application.persistentDataPath, id + ".json");

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);

            MessageScript.MessageData data = JsonUtility.FromJson<MessageScript.MessageData>(jsonString);
            return data.content;
        }

        Debug.LogError("File for ID " + id + " does not exist!");
        return string.Empty;
    }

    private GameObject GetPrefabByName(string name)
{
    // Load the prefab from the Resources folder
    GameObject prefab = Resources.Load<GameObject>("Prefabs/" + name);
    if (prefab == null)
    {
        Debug.LogError("Failed to load prefab with name " + name);
        prefab = Resources.Load<GameObject>("Prefabs/" + name.Replace("(Clone)", ""));
    }
    return prefab;
}


}
