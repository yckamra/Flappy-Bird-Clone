using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawnerScript : MonoBehaviour
{
    
    public GameObject building;
    public float targetForTimer = 5;
    public float timer = 0;
    public bool spawnBuildings = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnBuildings)
        {
            if (targetForTimer <= timer)
            {
                spawnBuilding();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            // do not spawn pipes
        }
    }
    private void spawnBuilding()
    {
        GameObject aBuilding = Instantiate(building, new Vector3(transform.position.x, transform.position.y, 8), transform.rotation);
        DontDestroyOnLoad(aBuilding);
    }
}
