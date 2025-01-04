using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public float xPosition;
    public float movementSpeed = 0;
    public float deadZone = -11;
    public bool moveClouds = true;
    public CloudSpawnerScript theCloudSpawnerScript;
    // Start is called before the first frame update
    void Start()
    {
        moveClouds = true;
        GameObject newCloudSpawner = GameObject.FindGameObjectWithTag("Cloud Spawner");
        theCloudSpawnerScript = newCloudSpawner.GetComponent<CloudSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        xPosition = transform.position.x;

        if (moveClouds)
        {
            if (transform.position.x <= deadZone)
            {
                Destroy(gameObject);

            }
            else
            {
                transform.position += Vector3.left * movementSpeed * Time.deltaTime;

            }
        }
        else
        {
            // do nothing
        }
    }
}
