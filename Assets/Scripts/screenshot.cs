using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Data.Common;

public class screenshot : MonoBehaviour
{

    public List<GameObject> uiObjects = new List<GameObject>();

    private void findUIs()
    {
        uiObjects.Clear();
        // Cache all UI objects on the "UI" layer during startup.
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.layer == LayerMask.NameToLayer("UI"))
            {
                uiObjects.Add(obj);
            }
        }
    }

    public string username;
    // Start is called before the first frame update
  public void TakeScreenshot()
    {
        string filepath = Application.persistentDataPath + "/MyGameSaveFolder/Screenshots/" + username;
        Directory.CreateDirectory(Path.GetDirectoryName(filepath));

        // Count the number of files in the folder filepath
        int count = Directory.GetFiles(filepath, "*", SearchOption.AllDirectories).Length;

        StartCoroutine(ScreenshotSequence(filepath + count + ".png"));
    }

    IEnumerator ScreenshotSequence(string path)
    {
        findUIs();
        // Hide UI
        SetUIActive(false);

        // Wait for a frame (this ensures UI is properly hidden)
        yield return new WaitForEndOfFrame();

        // Capture screenshot
        ScreenCapture.CaptureScreenshot(path);

        // Wait for another frame to ensure screenshot is taken without the UI
        yield return new WaitForEndOfFrame();

        // Show UI again
        SetUIActive(true);

        print(path);
    }

    void SetUIActive(bool isActive)
    {
        foreach (var obj in uiObjects)
        {
            obj.SetActive(isActive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
