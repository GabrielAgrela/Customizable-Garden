using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnToHit : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    public MenuContent menuContent;

    public void Start()
    {
        // Get a reference to the XRRayInteractor component attached to this game object
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    // Call this method to start the spawn after a delay
    public void StartSpawnWithDelay(GameObject itemPrefab)
    {
        print("ge1");
        StartCoroutine(SpawnAfterDelay(itemPrefab,3f)); // Change delay time here
        print("ge2");
    }

    // This is a coroutine that waits for a delay and then calls Spawn()
    private IEnumerator SpawnAfterDelay(GameObject itemPrefab,float delay)
    {
        yield return new WaitForSeconds(delay);
        Spawn(itemPrefab);
    }

    public void Spawn(GameObject itemPrefab)
    {
        // Check if the ray is hitting anything
        bool hitSomething = rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);
        // If the ray hit something, get the hit position and do something with it
        if (hitSomething)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                //print hit position
                
                print("fdsfd");
                GameObject item = Instantiate(itemPrefab, hit.point, Quaternion.identity);
                item.name = itemPrefab.name; 
                menuContent.spawnedItems.Add(item);
            }
        }
    }
}
