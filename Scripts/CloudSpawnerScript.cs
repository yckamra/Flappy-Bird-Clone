using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour
{
    public GameObject cloud;
    public float targetForTimer = 5;
    public float timer = 0;
    public bool spawnClouds = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnClouds)
        {
            if (targetForTimer <= timer)
            {
                spawnCloud();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            // do not spawn clouds
        }
    }

    private void spawnCloud()
    {
        GameObject aCloud = Instantiate(cloud, new Vector3(transform.position.x, transform.position.y, 9), transform.rotation);
        DontDestroyOnLoad(aCloud);
    }
}
