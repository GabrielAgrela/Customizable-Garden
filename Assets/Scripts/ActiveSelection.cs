using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelection : MonoBehaviour
{
    public float timer = 5f;
    public float activeTimer = 2f; // Timer to keep track of how long it stays active
    public bool active = false; // Added a new boolean to keep track of active state
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void activate()
    {
        active = true;
        activeTimer = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (active) // If not hitting something and active, count down the active timer
        {
            activeTimer -= Time.deltaTime;
            if (transform.GetChild(0).transform.tag == "GardenUI" )
            {
                transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else if (transform.GetChild(0).transform.GetChild(0).transform.tag == "GardenUI")
            {
                transform.GetChild(0).transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            
            if (activeTimer <= 0)
            {
                activeTimer = 2f;
                active = false; // Set active to false after 2 seconds of not hitting
                if (transform.GetChild(0).transform.tag == "GardenUI" )
                {
                    transform.GetChild(0).transform.gameObject.SetActive(false);
                }
                else if (transform.GetChild(0).transform.GetChild(0).transform.tag == "GardenUI")
                {
                    transform.GetChild(0).transform.GetChild(0).transform.gameObject.SetActive(false);
                }
                
            }
        }
    }
}
