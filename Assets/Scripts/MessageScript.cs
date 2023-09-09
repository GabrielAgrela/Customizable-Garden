using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class MessageScript : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public int id=0;
    public int idTotal=0;
    public Animator animator;

    [System.Serializable]
    public class MessageData
    {
        public int name;
        public string content;
    }
    
    public void deleteMessage()
    
    {

        try
        {
            GameObject.Find("Chest").GetComponent<ChestScript>().chestPositionsCurr[id]=null;
            Destroy(gameObject);
        }
        catch (System.Exception)
        {
            
            print("gallery");
        }

    }

    public void changeMessageContent(string messageContent, int id)
    {
        this.id = id;
        textField.text = messageContent;

        MessageData data = new MessageData
        {
            name = this.id,
            content = messageContent
        };

        string jsonString = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, id + ".json");
        File.WriteAllText(path, jsonString);
    }

    public void Grabbed()
    {
         
        animator.SetBool("Open",true);
        
    }

    public void Released()
    { 
        animator.SetBool("Open",false);
    }
}
