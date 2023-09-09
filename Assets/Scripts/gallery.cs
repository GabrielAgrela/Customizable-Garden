using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class gallery : MonoBehaviour
{
    public string username; // Set this to your user's name
    public GameObject photoHolderPrefab; // Drag and drop your photoholder prefab here in the inspector
    public List<GameObject> photos = new List<GameObject>();

    private string screenshotsFolderPath;
    int i=0;
    public Transform photosT;

    void Start()
    {
        i=0;
        screenshotsFolderPath = Path.Combine(Application.persistentDataPath, "MyGameSaveFolder", "Screenshots", username);
        LoadPhotosFromPersistentDataPath();
    }

    void LoadPhotosFromPersistentDataPath()
    {
        // Ensure the folder exists
        if (Directory.Exists(screenshotsFolderPath))
        {
            string[] files = Directory.GetFiles(screenshotsFolderPath, "*.png");

            foreach (string filePath in files)
            {
                Sprite photo = LoadSprite(filePath);
                if (photo)
                {


                    GameObject photoInstance = Instantiate(photoHolderPrefab,photosT);
                    photoInstance.transform.position = photos[i].transform.position;
                    photoInstance.transform.rotation = photos[i].transform.rotation;
                    SpriteRenderer sr = photoInstance.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    if (sr)
                    {
                        sr.sprite = photo;
                    }
                    photos[i]=photoInstance;
                    i++;
                }
            }
        }
    }

    private Sprite LoadSprite(string filePath)
    {
        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, false); // creating a dummy 2x2 texture
            if (texture.LoadImage(fileData)) // Load the image file into the texture
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
        return null;
    }
}
